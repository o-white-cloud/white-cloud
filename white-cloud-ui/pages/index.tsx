import Hero from 'components/Hero';
import TestList from 'components/TestList';
import { TestModel } from 'models/TestModel';
import { GetStaticProps } from 'next';

import Container from '@mui/material/Container';
import Typography from '@mui/material/Typography';

interface HomepageProps {
  tests: TestModel[];
}

const Homepage: React.FC<HomepageProps> = (props) => {
  const { tests } = props;
  return (
    <>
      <Hero
        imgSrc="/images/ravi-roshan-_hand-unsplash.jpg"
        imgAlt="clouds"
        title="Teste psihologice"
        subtitle="Descopera testele noastre miau miau"
      />
      <Container
        maxWidth="lg"
        sx={{
          height: '100vh',
          width: `100vw`,
          overflow: `hidden`,
          maxWidth: `100%`,
        }}
      >
        <TestList testItems={tests} />
      </Container>
    </>
  );
};

export const getStaticProps: GetStaticProps = async (context) => {
  const res = await fetch('http://localhost:5187/tests');
  const tests: TestModel[] = await res.json();
  
  return { props: { tests } };
};

export default Homepage;