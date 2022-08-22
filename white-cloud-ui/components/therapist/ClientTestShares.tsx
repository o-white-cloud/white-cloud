import { ClientTestRequest, ClientTestSubmissionShare, TestModel } from 'models';
import { useSnackbar } from 'notistack';
import React, { useCallback, useEffect, useState } from 'react';

import { Delete } from '@mui/icons-material';
import ArrowRightIcon from '@mui/icons-material/ArrowRight';
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

export interface ClientTestSharesProps {
  clientId: number;
}

export const ClientTestShares: React.FC<ClientTestSharesProps> = (
  props
) => {
  const { clientId } = props;
  const [testShares, setTestShares] = useState<ClientTestSubmissionShare[]>([]);
  // const { enqueueSnackbar } = useSnackbar();

  useEffect(() => {
    fetch(
      `${process.env.NEXT_PUBLIC_HOST}/therapist/testShares?clientId=${clientId}`
    )
      .then((r) => r.json())
      .then((data) => setTestShares(data));
  }, []);

  
  return (
    <div>
      <TableContainer component={Paper}>
          <Typography variant="h5" sx={{ margin: 2 }}>
            Teste efectuate
          </Typography>
        <Table size="small">
          <TableHead>
            <TableRow>
              <TableCell>Test</TableCell>
              <TableCell>Data</TableCell>
              <TableCell>Rezultat</TableCell>
              <TableCell width={100} />
            </TableRow>
          </TableHead>
          <TableBody>
            {testShares.map((row) => (
              <TableRow
                hover
                key={row.id}
                sx={{
                  '&:last-child td, &:last-child th': { border: 0 },
                  '&:hover button': { visibility: 'visible' },
                }}
              >
                <TableCell component="th" scope="row">
                  {row.testSubmissionTestName}
                </TableCell>
                <TableCell>
                  {new Date(row.shareDate).toLocaleString()}
                </TableCell>
                    <TableCell>
                      {JSON.stringify(row.testSubmissionResultData)}
                    </TableCell>
                <TableCell>
                  <Tooltip title="Anuleaza cererea">
                    <IconButton
                      sx={{ visibility: 'hidden'}}
                      data-id={row.id}
                      onClick={() => {}}
                    >
                      <ArrowRightIcon />
                    </IconButton>
                  </Tooltip>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </div>
  );
};
