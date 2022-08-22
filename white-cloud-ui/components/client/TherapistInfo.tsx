import { ClientTherapist } from 'models';
import React, { useEffect, useState } from 'react';

import { Button, CircularProgress, Typography } from '@mui/material';
import Stack from '@mui/material/Stack';
import TextField from '@mui/material/TextField';

export interface TherapistInfoProps {}

const ClientField: React.FC<{ label: string; value: string }> = (props) => (
  <TextField
    sx={{ marginTop: 2 }}
    label={props.label}
    value={props.value}
    variant="standard"
    InputProps={{ readOnly: true, disableUnderline: true }}
  />
);

export const TherapistInfo: React.FC<TherapistInfoProps> = (props) => {
  const [therapist, setTherapist] = useState<ClientTherapist | null>(null);
  const [loadingTherapistInfo, setLoadingTherapistInfo] = useState(true);
  useEffect(() => {
    fetch(`${process.env.NEXT_PUBLIC_HOST}/client/therapist`)
      .then((r) => r.json())
      .then((data) => {
        setTherapist(data);
        setLoadingTherapistInfo(false);
      })
      .catch((e) => setLoadingTherapistInfo(false));
  }, []);

  if (loadingTherapistInfo) {
    return <CircularProgress />;
  }

  if (!therapist) {
    return (
      <div>
        <Typography>
          Nu aveti niciun psiholog asociat. Doriti un psiholog?
        </Typography>
        <Button>Vreau psiholog</Button>
      </div>
    );
  }
  
  return (
    <Stack>
      <Typography variant="h4">{`${therapist.firstName} ${therapist.lastName}`}</Typography>
      <ClientField label="Email" value={therapist.email} />
      <ClientField
        label="Data"
        value={new Date(therapist.therapistDate).toLocaleDateString()}
      />
    </Stack>
  );
};
