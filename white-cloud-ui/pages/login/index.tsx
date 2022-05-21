import { Login, useLogin } from 'components/user/Login';
import { useCallback } from 'react';

import { CardContent } from '@mui/material';
import Card from '@mui/material/Card';
import Container from '@mui/material/Container';

const LoginPage = () => {
  const [onLogin, loginError] = useLogin(`${process.env.NEXT_PUBLIC_HOST}/authentication/login`, '/');
  
  return (
    <Container component="main" maxWidth="md">
      <Card>
        <CardContent>
          <Login
            onLogin={onLogin}
            error={loginError}
            forgotPasswordUrl="/login/forgotPassword"
            registerUrl="/login/register"
          />
        </CardContent>
      </Card>
    </Container>
  );
};

export default LoginPage;
