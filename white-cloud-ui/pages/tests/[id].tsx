import TestRunner from 'components/test-runner/TestRunner';
import parse from 'html-react-parser';
import { TestModel } from 'models';
import { GetStaticPaths, GetStaticProps } from 'next';
import React, { useCallback } from 'react';

import { Box, Container, Typography } from '@mui/material';

interface TestProps {
  testItem: TestModel;
}

const Test: React.FC<TestProps> = (props) => {
  const { testItem } = props;
  const onTestSubmit = useCallback((answers: { [qId: number]: string }) => {},
  []);
  return (
    <Container>
      <Typography variant="h2">{testItem.name}</Typography>
      <Typography variant="body1">{parse(testItem.description)}</Typography>
      <TestRunner testItem={testItem} onSubmit={onTestSubmit} />
    </Container>
  );
};

export const getStaticPaths: GetStaticPaths = async () => {
  // Call an external API endpoint to get posts
  const res = await fetch('http://localhost:5187/tests');
  const tests: TestModel[] = await res.json();

  // Get the paths we want to pre-render based on posts
  const paths = tests.map((t) => ({
    params: { id: t.id.toString() },
  }));

  // We'll pre-render only these paths at build time.
  // { fallback: false } means other routes should 404.
  return { paths, fallback: false };
};

export const getStaticProps: GetStaticProps = async (context) => {
  if (!context.params) {
    return { props: {} };
  }

  const id = Number(context.params['id']);
  const res = await fetch('http://localhost:5187/tests');
  const tests: TestModel[] = await res.json();

  return { props: { testItem: tests.find((t) => t.id === id) } };
};

export default Test;
