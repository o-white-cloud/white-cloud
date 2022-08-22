import { useUser } from 'components/hooks';
import { PageContainer } from 'components/PageContainer';
import { ClientInvites, ClientList } from 'components/therapist';
import { Roles } from 'models/User';

import { Grid, Typography } from '@mui/material';

const TherapistPage = () => {
  const [user] = useUser();
  if (!user.authenticated || !user.roles.includes(Roles.Therapist)) {
    return <p>Unauthorized</p>;
  }

  return (
    <PageContainer>
      <Grid container spacing={2}>
        <Grid item xs={4}>
          <ClientInvites therapistUserId={user.id} />
        </Grid>

        <Grid item xs={8}>
          <ClientList />
        </Grid>
      </Grid>
    </PageContainer>
  );
};

export default TherapistPage;
