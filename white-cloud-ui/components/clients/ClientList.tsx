import { Client } from 'models';
import { useCallback, useEffect, useState } from 'react';

import Paper from '@mui/material/Paper';
import Stack from '@mui/material/Stack';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Typography from '@mui/material/Typography';

export interface ClientListProps {}

export const ClientList: React.FC<ClientListProps> = (props) => {
  const [clients, setClients] = useState<Client[]>([]);
  useEffect(() => {
    fetch(`${process.env.NEXT_PUBLIC_HOST}/therapist/clients`)
      .then((r) => r.json())
      .then((data) => setClients(data));
  }, []);

  return (
    <div>
      <TableContainer component={Paper}>
      <Typography sx={{marginTop:2, marginLeft: 2}} variant='h5'>Clienti</Typography>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Nume</TableCell>
              <TableCell>Email</TableCell>
              <TableCell>Data client</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {clients.map((row) => (
              <TableRow
                hover
                key={row.id}
                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
              >
                <TableCell component="th" scope="row">
                  {`${row.firstName} ${row.lastName}`}
                </TableCell>
                <TableCell>{row.email}</TableCell>
                <TableCell>{new Date(row.clientDate).toLocaleString()}</TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </div>
  );
};
