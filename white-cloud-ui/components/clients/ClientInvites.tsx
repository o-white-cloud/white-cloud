import { ClientInvite } from 'models';
import { useCallback, useEffect, useState } from 'react';
import { Controller, SubmitHandler, useForm } from 'react-hook-form';

import Badge from '@mui/material/Badge';
import Button from '@mui/material/Button';
import Paper from '@mui/material/Paper';
import Stack from '@mui/material/Stack';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import TextField from '@mui/material/TextField';
import Typography from '@mui/material/Typography';

export interface ClientInvitesProps {
  therapistUserId: string;
}

export const ClientInvites: React.FC<ClientInvitesProps> = (props) => {
  const { therapistUserId } = props;
  const [invites, setInvites] = useState<ClientInvite[]>([]);
  useEffect(() => {
    fetch(`${process.env.NEXT_PUBLIC_HOST}/therapist/invites`)
      .then((r) => r.json())
      .then((data) => setInvites(data));
  }, [therapistUserId]);

  const onNewInvite = useCallback(
    (invite: ClientInvite) => {
      setInvites([invite, ...invites]);
    },
    [invites]
  );

  return (
    <div>
      <TableContainer component={Paper}>
        <Typography sx={{marginTop:2, marginLeft: 2}} variant='h5'>Invitatii active</Typography>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Email</TableCell>
              <TableCell>Data</TableCell>
              <TableCell></TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {invites.map((row) => (
              <TableRow
                key={row.id}
                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
              >
                <TableCell component="th" scope="row">
                  {row.email}
                </TableCell>
                <TableCell>{new Date(row.sentDate).toLocaleString()}</TableCell>
                <TableCell>
                  <Button>Trimite din nou</Button>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
      <SendInvite onNewInvite={onNewInvite} />
    </div>
  );
};

const SendInvite: React.FC<{ onNewInvite: (invite: ClientInvite) => void }> = (
  props
) => {
  const { onNewInvite } = props;
  const onSubmitInternal = useCallback<SubmitHandler<{ email: string }>>(
    (data) => {
      fetch(`${process.env.NEXT_PUBLIC_HOST}/therapist/invite`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(data.email),
      })
        .then((r) => r.json())
        .then((data) => onNewInvite(data));
    },
    [onNewInvite]
  );

  const {
    control,
    handleSubmit,
    formState: { errors, isSubmitting },
  } = useForm<{ email: string }>({
    defaultValues: {
      email: '',
    },
  });

  return (
    <form onSubmit={handleSubmit(onSubmitInternal)}>
      <Stack direction="row" spacing={2} sx={{ marginTop: 2 }}>
        <Controller
          name="email"
          control={control}
          defaultValue=""
          rules={{ required: { value: true, message: 'Email is required' } }}
          render={({ field }) => (
            <TextField
              label="Email"
              sx={{ minWidth: 300 }}
              {...field}
              helperText={errors.email?.message}
              error={errors.email !== undefined}
            />
          )}
        />
        <Button variant="contained" type="submit">
          Trimite invitatie
        </Button>
      </Stack>
    </form>
  );
};
