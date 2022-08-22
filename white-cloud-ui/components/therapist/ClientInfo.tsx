import { Client, Gender } from 'models';
import React, { useEffect, useState } from 'react';

import { CircularProgress, Typography } from '@mui/material';
import Stack from '@mui/material/Stack';
import TextField from '@mui/material/TextField';

export interface ClientInfoProps {
  clientId: number;
}

const ClientField: React.FC<{ label: string; value: string }> = (props) => (
  <TextField
    sx={{ marginTop: 2 }}
    label={props.label}
    value={props.value}
    variant="standard"
    InputProps={{ readOnly: true, disableUnderline: true }}
  />
);

export const ClientInfo: React.FC<ClientInfoProps> = (props) => {
  const { clientId } = props;
  const [client, setClient] = useState<Client | null>(null);
  useEffect(() => {
    fetch(`${process.env.NEXT_PUBLIC_HOST}/therapist/client/${clientId}`)
      .then((r) => r.json())
      .then((data) => setClient(data));
  }, [clientId]);

  if (!client) {
    return <CircularProgress />;
  }

  return (
    <Stack>
      <Typography variant="h4">{`${client.userFirstName} ${client.userLastName}`}</Typography>
      <ClientField label="Varsta" value={client.age.toString()} />
      <ClientField
        label="Sex"
        value={client.gender === Gender.Male ? 'Masculin' : 'Feminin'}
      />
      <ClientField label="Ocupatia" value={client.ocupation} />
      <ClientField label="Email" value={client.userEmail} />
      <ClientField
        label="Data"
        value={new Date(client.clientDate).toLocaleDateString()}
      />
    </Stack>
  );
};
