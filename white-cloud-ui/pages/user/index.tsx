import { ClientInvites, ClientList } from 'components/clients';
import { useUser } from 'components/hooks';
import { PageContainer } from 'components/PageContainer';
import { Roles } from 'models/User';

import { Grid, Typography } from '@mui/material';

const UserPage = () => {
  const [user] = useUser();
  if (!user.authenticated) {
    return <p>Unauthorized</p>;
  }

  return (
    <PageContainer>
      {user.roles.includes(Roles.Therapist) && (
        <Grid container spacing={2}>
          <Grid item xs={4}>
            <ClientInvites therapistUserId={user.id} />
          </Grid>

          <Grid item xs={8}>
            <ClientList />
          </Grid>
        </Grid>
      )}
    </PageContainer>
  );
};

export default UserPage;
