import { PageContainer } from 'components/PageContainer';

import Button from '@mui/material/Button';

const EmailConfirmed = () => {
  return (
    <PageContainer>
      Email-ul a fost confirmat!
      Acceseaza-ti pagina ta apasand acest buton {<Button>Pagina mea</Button>}
    </PageContainer>
  );
};

export default EmailConfirmed;