import Button from '@mui/material/Button';
import Container from '@mui/material/Container';

const EmailConfirmed = () => {
  return (
    <Container component="main" maxWidth="md">
      Email-ul a fost confirmat!
      Acceseaza-ti pagina ta apasand acest buton {<Button>Pagina mea</Button>}
    </Container>
  );
};

export default EmailConfirmed;