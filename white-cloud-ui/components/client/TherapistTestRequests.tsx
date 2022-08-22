import { ClientTestRequest } from 'models';
import { useRouter } from 'next/router';
import { useCallback, useEffect, useState } from 'react';

import PlayIcon from '@mui/icons-material/PlayArrow';
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

export interface TherapistTestRequestsProps {
  startTest: (testId: number, requestId: number) => void;
}

export const TherapistTestRequests: React.FC<TherapistTestRequestsProps> = (
  props
) => {
  const { startTest } = props;
  const [requests, setRequests] = useState<ClientTestRequest[]>([]);
  useEffect(() => {
    fetch(`${process.env.NEXT_PUBLIC_HOST}/client/testRequests`)
      .then((r) => r.json())
      .then((data) => setRequests(data));
  }, []);

  const onStartTest = useCallback<React.MouseEventHandler<HTMLButtonElement>>(
    (e) => {
      var id = Number(e.currentTarget.getAttribute('data-id'));
      if (id === Number.NaN) {
        return;
      }
      const req = requests.find(t => t.id === id);
      if(!req){
        return;
      }
      startTest(req.testId, req.id);
    },
    [requests, startTest]
  );

  return (
    <div>
      <TableContainer component={Paper}>
        <Stack
          direction="row"
          sx={{ marginTop: 2, marginLeft: 2, marginRight: 2 }}
        >
          <Typography variant="h5" sx={{ flexGrow: 1 }}>
            Teste asignate
          </Typography>
        </Stack>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Test</TableCell>
              <TableCell width={200}>Data</TableCell>
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
                <TableCell>{new Date(row.sentDate).toLocaleString()}</TableCell>
                <TableCell>
                    <Button variant="contained"
                      sx={{ visibility: 'hidden' }}
                      data-id={row.id}
                      onClick={onStartTest}
                      startIcon={<PlayIcon />}
                    >
                      Start
                    </Button>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </div>
  );
};
