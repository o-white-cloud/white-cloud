import { useCallback } from 'react';

import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Stack from '@mui/material/Stack';

export interface OpenIdLoginProps {
  onOidcLogin: (provider: string) => void;
}

enum Idps {
  Google = 'google',
  Facebook = 'facebook',
}

export const useOpenIdConnect = useCallback(async (provider: string) => {
  const url = await (
    await fetch(
      `${process.env.NEXT_PUBLIC_HOST}/authentication/oidc/authUrl/${provider}`
    )
  ).text();
  window.location.assign(url);
}, []);

export const OpenIdLogin: React.FC<OpenIdLoginProps> = (props) => {
  const { onOidcLogin } = props;
  return (
    <Box
      sx={{
        marginTop: 24,
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
      }}
    >
      <Stack>
        <Button onClick={() => onOidcLogin(Idps.Google)}>Google</Button>
      </Stack>
    </Box>
  );
};
