import { PageContainer } from 'components/PageContainer';
import { ForgotPassword } from 'components/user/ForgotPassword';
import { Login } from 'components/user/Login';
import { useRouter } from 'next/router';
import { useCallback } from 'react';

import { CardContent } from '@mui/material';
import Card from '@mui/material/Card';

const ForgotPasswordPage = () => {
  const router = useRouter();
  const onSubmit = useCallback(async (email: string) => {
    const result = await fetch(`${process.env.NEXT_PUBLIC_HOST}/user/forgotPassword`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        email,
      }),
    });
    if(result.ok) {
      router.push("/");
    }
  }, [router]);

  return (
    <PageContainer>
      <Card>
        <CardContent>
          <ForgotPassword onSubmit={onSubmit}/>
        </CardContent>
      </Card>
    </PageContainer>
  );
};

export default ForgotPasswordPage;
