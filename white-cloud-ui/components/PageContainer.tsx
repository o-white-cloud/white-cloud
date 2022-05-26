import { Container, ContainerProps } from '@mui/material';

export const PageContainer = (props: ContainerProps) => {
  return (
    <Container
      component="main"
      maxWidth="xl"
      sx={{
        padding: 4,
        height: '100vh',
        width: `100vw`,
        overflow: `hidden`,
        maxWidth: `100%`,
      }}
      {...props}
    >
      {props.children}
    </Container>
  );
};
