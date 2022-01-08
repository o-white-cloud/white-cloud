import Hero from 'components/Hero';
import TestList from 'components/TestList';
import { TestModel } from 'models/TestModel';

import Container from '@mui/material/Container';
import Typography from '@mui/material/Typography';

const tests: TestModel[] = [
  {
    id: 1,
    icon: 'multipleInt',
    name: 'Inteligente multiple',
    excerpt: 'Chiar crezi ca ai mai multe?',
    description: 'Much description, such lorem ipsum',
    questions: [
      {
        id: 1,
        title: "Te simti singur noaptea in padure?",
        type: "choice",
        answers: [
          {
            id: 1,
            name: "Da"
          },
          {
            id: 2,
            name: "Nu"
          }
        ]
      },
      {
        id: 2,
        type: "choice",
        title: "Vara iti este cateodata cald?",
        answers: [
          {
            id: 1,
            name: "Da"
          },
          {
            id: 2,
            name: "Nu"
          }
        ]
      }
    ]
  }
];

const Homepage = () => {
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

export default Homepage;
