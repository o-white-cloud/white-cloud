import { TestModel } from 'models';
import { ChangeEvent, MouseEventHandler, useCallback, useEffect, useState } from 'react';

import { Button, DialogActions, DialogTitle, FormControlLabel } from '@mui/material';
import Checkbox from '@mui/material/Checkbox';
import CircularProgress from '@mui/material/CircularProgress';
import Dialog from '@mui/material/Dialog';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Typography from '@mui/material/Typography';

export interface TestSelectionDialogProps {
  onSelected: (selectedTests: TestModel[]) => void;
  onCancel: () => void;
  defaultTests?: TestModel[];
  open: boolean;
}

export const TestSelectionDialog: React.FC<TestSelectionDialogProps> = (
  props
) => {
  const { onSelected, onCancel, defaultTests, open } = props;
  const [tests, setTests] = useState<TestModel[]>(defaultTests ?? []);
  const [selected, setSelected] = useState<readonly number[]>([]);
  const [loadingTests, setLoadingTests] = useState(true);
  // const [sendEmail, setSendEmail] = useState(false);

  useEffect(() => {
    if (!open) return;

    if (!tests || tests.length == 0) {
      fetch(`${process.env.NEXT_PUBLIC_HOST}/tests`)
        .then((r) => r.json())
        .then((data) => {
          setTests(data);
          setSelected([]);
          setLoadingTests(false);
        });
    } else {
      setSelected([]);
      setLoadingTests(false);
    }
  }, [tests, open]);

  // const onSendEmailChanged = useCallback(
  //   (event: ChangeEvent<HTMLInputElement>, checked: boolean) => {
  //     setSendEmail(checked);
  //   },
  //   [setSendEmail]
  // );

  const onRowSelectionChange = useCallback<
    MouseEventHandler<HTMLTableRowElement>
  >(
    (event) => {
      const id = Number(event.currentTarget.getAttribute('data-id'));
      if (Number.isNaN(id)) {
        return;
      }

      const selectedIndex = selected.indexOf(id);
      let newSelected: readonly number[] = [];

      if (selectedIndex === -1) {
        newSelected = newSelected.concat(selected, id);
      } else if (selectedIndex === 0) {
        newSelected = newSelected.concat(selected.slice(1));
      } else if (selectedIndex === selected.length - 1) {
        newSelected = newSelected.concat(selected.slice(0, -1));
      } else if (selectedIndex > 0) {
        newSelected = newSelected.concat(
          selected.slice(0, selectedIndex),
          selected.slice(selectedIndex + 1)
        );
      }

      setSelected(newSelected);
    },
    [selected]
  );

  const onSelectAllClick = useCallback(
    (event: React.ChangeEvent<HTMLInputElement>) => {
      if (event.target.checked) {
        const newSelecteds = tests.map((t) => t.id);
        setSelected(newSelecteds);
        return;
      }
      setSelected([]);
    },
    [tests]
  );

  const onSelectClick = useCallback(() => {
    if (!onSelected || selected.length == 0) {
      return;
    }
    const selectedTests = selected.reduce<TestModel[]>((a, id) => {
      const t = tests.find((t) => t.id === id);
      if (t) {
        a.push(t);
      }
      return a;
    }, []);
    onSelected(selectedTests);
  }, [onSelected, selected, tests]);
  return (
    <Dialog open={open} keepMounted={false}>
      <DialogTitle>Selecteaza teste pentru a trimite</DialogTitle>
      <TableContainer>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>
                <Checkbox
                  indeterminate={
                    (selected.length > 0 && selected.length !== tests.length) ??
                    false
                  }
                  checked={
                    (tests.length > 0 && selected.length === tests.length) ??
                    false
                  }
                  onChange={onSelectAllClick}
                />
              </TableCell>
              <TableCell>Test</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {loadingTests ? (
              <CircularProgress />
            ) : (
              tests.map((row) => {
                const isSelected = selected.indexOf(row.id) !== -1;
                return (
                  <TableRow
                    hover
                    key={row.id}
                    data-id={row.id}
                    onClick={onRowSelectionChange}
                    selected={isSelected}
                    sx={{ cursor: 'pointer' }}
                  >
                    <TableCell component="th" scope="row">
                      <Checkbox checked={isSelected} />
                    </TableCell>
                    <TableCell>
                      <Typography variant="h6">{row.name}</Typography>
                      <Typography variant="caption">{row.excerpt}</Typography>
                    </TableCell>
                  </TableRow>
                );
              })
            )}
          </TableBody>
        </Table>
      </TableContainer>
      <DialogActions>
        {/* <FormControlLabel
          sx={{ flexGrow: 1, justifyContent: 'left', marginLeft: 1 }}
          control={
            <Checkbox checked={sendEmail} onChange={onSendEmailChanged} />
          }
          label="Trimite email"
        /> */}
        <Button onClick={onCancel}>Cancel</Button>
        <Button onClick={onSelectClick}>Trimite</Button>
      </DialogActions>
    </Dialog>
  );
};
