import { useUser } from 'components/hooks';
import { PageContainer } from 'components/PageContainer';
import { ClientInfo, ClientTestRequests, ClientTestShares } from 'components/therapist';
import { Roles } from 'models/User';
import { useRouter } from 'next/router';

import { Grid, Typography } from '@mui/material';

const ClientPage = () => {
  const [user] = useUser();
  const router = useRouter();
  const { clientId } = router.query;

  const id = Number(clientId);

  if (!user.authenticated || !user.roles.includes(Roles.Therapist)) {
    return <p>Unauthorized</p>;
  }

  return (
    <PageContainer>
      {user.roles.includes(Roles.Therapist) && (
        <Grid container spacing={2}>
          <Grid item xs={4}>
            <ClientInfo clientId={id} />
          </Grid>
          <Grid item xs={8}>
            <ClientTestRequests clientId={id} />
            <br/>
            <ClientTestShares clientId={id}/>
          </Grid>
        </Grid>
      )}
    </PageContainer>
  );
};

export default ClientPage;
