import { TestModel } from 'models';
import React from 'react';

import Grid from '@mui/material/Grid';

import TestCard from './TestCard';

interface TestListProps {
  testItems: TestModel[];
}

const TestList: React.FC<TestListProps> = (props) => {
  return (
    <Grid container spacing={3}>
      {props.testItems.map((t) => (
        <Grid item xs={4} key={t.id}>
          <TestCard test={t} />
        </Grid>
      ))}
    </Grid>
  );
};

export default TestList;
