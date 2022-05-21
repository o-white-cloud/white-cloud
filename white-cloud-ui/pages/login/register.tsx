import { Login } from 'components/user/Login';
import { OpenIdLogin } from 'components/user/OpenIdLogin';
import { Register, RegisterFormData } from 'components/user/Register';
import { useCallback, useState } from 'react';

import { Typography } from '@mui/material';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import Container from '@mui/material/Container';
import Grid from '@mui/material/Grid';

const RegisterPage = () => {
  const [registerComplete, setRegisterComplete] = useState(false);

  const onRegister = useCallback(async (data: RegisterFormData) => {
    const response = await fetch(`${process.env.NEXT_PUBLIC_HOST}/user/register`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(data),
    });
    if(response.ok) {
      setRegisterComplete(true);
    }
  }, []);

  return (
    <Container component="main" maxWidth="xs">
      <Card>
        <CardContent>
          {!registerComplete && <Register signInUrl="/login" onRegister={onRegister} />}
          {registerComplete && (<>
            <Typography variant='h4'>Bine ati venit in White Cloud!</Typography>
            <Typography variant='body1'>Un email de confirmare a fost trimis catre adresa dumneavoastra! Pentru a va putea autentifica in platforma noastra, este nevoie pentru confirmarea adresei de email.</Typography>
          </>)}
        </CardContent>
      </Card>
    </Container>
  );
};

export default RegisterPage;
