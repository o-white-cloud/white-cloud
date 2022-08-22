import { TherapistInfo, TherapistTestRequests } from 'components/client';
import { useUser } from 'components/hooks';
import { PageContainer } from 'components/PageContainer';
import { Roles } from 'models/User';
import { useRouter } from 'next/router';
import { useCallback } from 'react';

import { Grid, Typography } from '@mui/material';

const UserPage = () => {
  const [user] = useUser();
  const router = useRouter();

  const onTestStart = useCallback((testId: number, requestId: number) => {
    router.push(`/tests/${testId}?request=${requestId}`)
  }, [router]);

  if (!user.authenticated) {
    return <p>Unauthorized</p>;
  }


  return (
    <PageContainer>
      <Grid container spacing={2}>
        <Grid item xs={4}>
          <TherapistInfo/>
        </Grid>

        <Grid item xs={8}>
          <TherapistTestRequests startTest={onTestStart}/>
        </Grid>
      </Grid>
    </PageContainer>
  );
};

export default UserPage;
