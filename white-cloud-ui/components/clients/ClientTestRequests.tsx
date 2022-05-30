import { ClientTestRequest } from 'models';
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

export interface ClientTestRequestsProps {
  clientId: number;
}

export const ClientTestRequests: React.FC<ClientTestRequestsProps> = (
  props
) => {
  const { clientId } = props;
  const [requests, setRequests] = useState<ClientTestRequest[]>([]);
  useEffect(() => {
    fetch(
      `${process.env.NEXT_PUBLIC_HOST}/therapist/testRequests?clientId=${clientId}`
    )
      .then((r) => r.json())
      .then((data) => setRequests(data));
  }, []);

  return (
    <div>
      <TableContainer component={Paper}>
        <Typography sx={{ marginTop: 2, marginLeft: 2 }} variant="h5">
          Teste trimise
        </Typography>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Test</TableCell>
              <TableCell>Data</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {requests.map((row) => (
              <TableRow
                hover
                key={row.id}
                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
              >
                <TableCell component="th" scope="row">
                  {row.testName}
                </TableCell>
                <TableCell>
                  {new Date(row.requestDate).toLocaleString()}
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </div>
  );
};
