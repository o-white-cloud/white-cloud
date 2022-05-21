import { ResetPassword } from 'components/user/ResetPassword';
import { useCallback, useEffect, useState } from 'react';

import { CardContent } from '@mui/material';
import Card from '@mui/material/Card';
import Container from '@mui/material/Container';

const ForgotPasswordPage = () => {
  const [errors, setErrors] = useState<string[]>([]);

  const onSubmit = useCallback(
    async (password: string, confirmPassword: string) => {
      try {
        const token = (location.search.match(/token=([^&]+)/) || [])[1];
        const email = (location.search.match(/email=([^&]+)/) || [])[1];
        if(!token || !email)
        {
          setErrors(["Invalid email or token"]);
          return;
        }
        
        const response = await fetch(
          `${process.env.NEXT_PUBLIC_HOST}/user/resetPassword`,
          {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json',
            },
            body: JSON.stringify({
              email,
              token,
              password,
              confirmPassword,
            }),
          }
        );
        if (response.ok) {
          window.location.assign('/login');
        } else {
          var respErrors = await response.json();
          setErrors(respErrors);
        }
      } catch (err) {
        console.log(JSON.stringify(err));
      }
    },
    []
  );

  return (
    <Container component="main" maxWidth="md">
      <Card>
        <CardContent>
          <ResetPassword onSubmit={onSubmit} />
          {errors.length > 0 && errors.map((e) => <p>{e}</p>)}
        </CardContent>
      </Card>
    </Container>
  );
};

export default ForgotPasswordPage;
