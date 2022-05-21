import { ForgotPassword } from 'components/user/ForgotPassword';
import { Login } from 'components/user/Login';
import { useCallback } from 'react';

import { CardContent } from '@mui/material';
import Card from '@mui/material/Card';
import Container from '@mui/material/Container';

const ForgotPasswordPage = () => {
  const onSubmit = useCallback(async (email: string) => {
    await fetch(`${process.env.NEXT_PUBLIC_HOST}/user/forgotPassword`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        email,
      }),
    });
  }, []);

  return (
    <Container component="main" maxWidth="md">
      <Card>
        <CardContent>
          <ForgotPassword onSubmit={onSubmit}/>
        </CardContent>
      </Card>
    </Container>
  );
};

export default ForgotPasswordPage;
