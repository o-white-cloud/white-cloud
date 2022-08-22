import { ClientTestRequest, TestModel } from 'models';
import { useSnackbar } from 'notistack';
import React, { useCallback, useEffect, useState } from 'react';

import { Delete } from '@mui/icons-material';
import PlusIcon from '@mui/icons-material/AddCircle';
import Button from '@mui/material/Button';
import IconButton from '@mui/material/IconButton';
import Paper from '@mui/material/Paper';
import Stack from '@mui/material/Stack';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Tooltip from '@mui/material/Tooltip';
import Typography from '@mui/material/Typography';

import { TestSelectionDialog } from './TestSelectionDialog';

export interface ClientTestRequestsProps {
  clientId: number;
}

export const ClientTestRequests: React.FC<ClientTestRequestsProps> = (
  props
) => {
  const { clientId } = props;
  const [selectTestDialogOpen, setSelectTestDialogOpen] = useState(false);
  const [requests, setRequests] = useState<ClientTestRequest[]>([]);
  const { enqueueSnackbar } = useSnackbar();

  useEffect(() => {
    fetch(
      `${process.env.NEXT_PUBLIC_HOST}/therapist/testRequests?clientId=${clientId}`
    )
      .then((r) => r.json())
      .then((data) => setRequests(data));
  }, []);

  const onSendTests = useCallback(
    (selectedTests: TestModel[]) => {
      fetch(`${process.env.NEXT_PUBLIC_HOST}/therapist/testRequest`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          clientId,
          testIds: selectedTests.map((t) => t.id),
        }),
      })
        .then((r) => r.json())
        .then((data) => {
          setRequests([...requests, ...data]);
          enqueueSnackbar(
            selectedTests.length > 1
              ? 'Teste trimise cu succes!'
              : 'Test trimis cu succes!',
            { variant: 'success' }
          );
        });
      setSelectTestDialogOpen(false);
    },
    [clientId, requests, enqueueSnackbar]
  );

  const onRequestCancel = useCallback<React.MouseEventHandler>((e) => {
    const id = Number(e.currentTarget.getAttribute('data-id'));
    if (!id) {
      return;
    }

    fetch(`${process.env.NEXT_PUBLIC_HOST}/therapist/testRequest/${id}`, {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json',
      },
    }).then((r) => {
      if(r.status === 409) {
        enqueueSnackbar('Cererea de test a fost indeplinita si nu poate fi stearsa!', {
          variant: 'error',
        });  
        return;
      }
      const index = requests.findIndex(r => r.id === id);
      const newRequests = [...requests];
      newRequests.splice(index, 1);
      setRequests(newRequests);
      enqueueSnackbar('Cererea de test a fost revocata.', {
        variant: 'success',
      });
    });
  }, [requests]);

  return (
    <div>
      <TableContainer component={Paper}>
        <Stack
          direction="row"
          sx={{ marginTop: 2, marginLeft: 2, marginRight: 2 }}
        >
          <Typography variant="h5" sx={{ flexGrow: 1 }}>
            Teste trimise
          </Typography>
          <Button
            variant="contained"
            startIcon={<PlusIcon />}
            onClick={() => setSelectTestDialogOpen(true)}
          >
            Trimite
          </Button>
        </Stack>
        <Table size="small">
          <TableHead>
            <TableRow>
              <TableCell>Test</TableCell>
              <TableCell>Data</TableCell>
              <TableCell width={100} />
            </TableRow>
          </TableHead>
          <TableBody>
            {requests.map((row) => (
              <TableRow
                hover
                key={row.id}
                sx={{
                  '&:last-child td, &:last-child th': { border: 0 },
                  '&:hover button': { visibility: 'visible' },
                }}
              >
                <TableCell component="th" scope="row">
                  {row.testName}
                </TableCell>
                <TableCell>
                  {new Date(row.sentDate).toLocaleString()}
                </TableCell>
                <TableCell>
                  <Tooltip title="Anuleaza cererea">
                    <IconButton
                      sx={{ visibility: 'hidden'}}
                      data-id={row.id}
                      onClick={onRequestCancel}
                    >
                      <Delete />
                    </IconButton>
                  </Tooltip>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
      <TestSelectionDialog
        open={selectTestDialogOpen}
        onCancel={() => setSelectTestDialogOpen(false)}
        onSelected={onSendTests}
      />
    </div>
  );
};
