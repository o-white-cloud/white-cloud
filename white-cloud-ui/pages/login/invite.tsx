import { PageContainer } from 'components/PageContainer';
import { Register, RegisterFormData } from 'components/user/Register';
import { useRouter } from 'next/router';
import { useSnackbar } from 'notistack';
import { useCallback, useEffect, useState } from 'react';

import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';

const InvitePage = () => {
  const [email, setEmail] = useState('');
  const [token, setToken] = useState('');
  const [loading, setLoading] = useState(false);
  const [therapistUserId, setTherapistUserId] = useState('');
  const router = useRouter();
  const { enqueueSnackbar } = useSnackbar();
  
  useEffect(() => {
    setToken((location.search.match(/token=([^&]+)/) || [])[1]);
    setEmail((location.search.match(/email=([^&]+)/) || [])[1]);
    setTherapistUserId(
      (location.search.match(/therapistUserId=([^&]+)/) || [])[1]
    );
  }, []);

  const onRegister = useCallback(
    async (data: RegisterFormData) => {
      setLoading(true);
      try {
        const response = await fetch(
          `${process.env.NEXT_PUBLIC_HOST}/user/registerInvite`,
          {
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
              lastName: data.lastName,
              age: data.age,
              gender: data.gender,
              ocupation: data.ocupation,
            }),
          }
        );
        if (response.ok) {
          router.push('/login');
        }
      } catch (e) {
      } finally {
        setLoading(false);
      }
    },
    [email, token, therapistUserId, router]
  );

  return (
    <PageContainer maxWidth="sm">
      <Card>
        <CardContent>
          <Register
            signInUrl="/login"
            loading={loading}
            onRegister={onRegister}
            email={email}
            inviteMode
          />
        </CardContent>
      </Card>
    </PageContainer>
  );
};

export default InvitePage;
