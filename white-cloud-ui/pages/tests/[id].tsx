import { PageContainer } from 'components/PageContainer';
import TestRunner from 'components/test-runner/TestRunner';
import parse from 'html-react-parser';
import { TestModel } from 'models';
import { GetStaticPaths, GetStaticProps } from 'next';
import React, { useCallback } from 'react';

import { Box, Container, TextField, Typography } from '@mui/material';

interface TestProps {
  testItem: TestModel;
}

const Test: React.FC<TestProps> = (props) => {
  console.log(process.env.NEXT_PUBLIC_HOST);
  const { testItem } = props;
  const onTestSubmit = useCallback(
    (answers: { [qId: number]: string }) => {
      fetch(`${process.env.NEXT_PUBLIC_HOST}/tests`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          testId: testItem.id,
          answers,
        }),
      })
        .then((r) => r.json())
        .then((x) => console.log(JSON.stringify(x)));
    },
    [testItem]
  );
  return (
    <PageContainer sx={{overflow: 'auto'}}>
      <Typography variant="h2">{testItem.name}</Typography>
      <Box sx={{ typography: 'body1' }}>{parse(testItem.description)}</Box>
      <TestRunner testItem={testItem} onSubmit={onTestSubmit} />
    </PageContainer>
  );
};

export const getStaticPaths: GetStaticPaths = async () => {
  // Call an external API endpoint to get posts
  const res = await fetch(`${process.env.BUILD_HOST}/tests`);
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
  const res = await fetch(`${process.env.BUILD_HOST}/tests`);
  const tests: TestModel[] = await res.json();

  return { props: { testItem: tests.find((t) => t.id === id) } };
};

export default Test;
