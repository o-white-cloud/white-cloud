import { PageContainer } from 'components/PageContainer';
import { Login, useLogin } from 'components/user/Login';

import { CardContent } from '@mui/material';
import Card from '@mui/material/Card';
import Container from '@mui/material/Container';

const LoginPage = () => {
  const [onLogin, loginError, loading] = useLogin(`${process.env.NEXT_PUBLIC_HOST}/authentication/login`, '/user');
  
  return (
    <PageContainer maxWidth="sm">
      <Card>
        <CardContent>
          <Login
            onLogin={onLogin}
            error={loginError}
            loading={loading}
            forgotPasswordUrl="/login/forgotPassword"
            registerUrl="/login/register"
          />
        </CardContent>
      </Card>
    </PageContainer>
  );
};

export default LoginPage;
