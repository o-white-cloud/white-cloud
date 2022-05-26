import { PageContainer } from 'components/PageContainer';
import { Login } from 'components/user/Login';
import { OpenIdLogin } from 'components/user/OpenIdLogin';
import { Register, RegisterFormData } from 'components/user/Register';
import { useRouter } from 'next/router';
import { useCallback, useEffect, useState } from 'react';

import { Typography } from '@mui/material';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import Container from '@mui/material/Container';
import Grid from '@mui/material/Grid';

const InvitePage = () => {
  const [email, setEmail] = useState('');
  const [token, setToken] = useState('');
  const [therapistUserId, setTherapistUserId] = useState('');
  const router = useRouter();
  useEffect(() => {
    setToken((location.search.match(/token=([^&]+)/) || [])[1]);
    setEmail((location.search.match(/email=([^&]+)/) || [])[1]);
    setTherapistUserId((location.search.match(/therapistUserId=([^&]+)/) || [])[1]);  
  })
  
  const onRegister = useCallback(async (data: RegisterFormData) => {
    const response = await fetch(`${process.env.NEXT_PUBLIC_HOST}/user/registerInvite`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        inviteToken: token,
        therapistUserId,
        email,
        password: data.password,
        firstName: data.firstName,
        lastName: data.lastName
      }),
    });
    if(response.ok) {
      router.push('/login');
    }
  }, [email, token, therapistUserId]);

  return (
    <PageContainer>
      <Card>
        <CardContent>
          <Register signInUrl="/login" onRegister={onRegister} email={email} inviteById={therapistUserId}/>
        </CardContent>
      </Card>
    </PageContainer>
  );
};

export default InvitePage;
