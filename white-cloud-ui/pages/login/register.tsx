import { PageContainer } from 'components/PageContainer';
import { Login } from 'components/user/Login';
import { OpenIdLogin } from 'components/user/OpenIdLogin';
import { Register, RegisterFormData } from 'components/user/Register';
import { useCallback, useState } from 'react';

import { Typography } from '@mui/material';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';

const RegisterPage = () => {
  const [registerComplete, setRegisterComplete] = useState(false);

  const onRegister = useCallback(async (data: RegisterFormData) => {
    debugger;
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
    <PageContainer maxWidth="sm">
      <Card>
        <CardContent>
          {!registerComplete && <Register signInUrl="/login" onRegister={onRegister} />}
          {registerComplete && (<>
            <Typography variant='h4'>Bine ati venit in White Cloud!</Typography>
            <Typography variant='body1'>Un email de confirmare a fost trimis catre adresa dumneavoastra! Pentru a va putea autentifica in platforma noastra, este nevoie pentru confirmarea adresei de email.</Typography>
          </>)}
        </CardContent>
      </Card>
    </PageContainer>
  );
};

export default RegisterPage;
