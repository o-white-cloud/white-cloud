import { TestModel } from 'models/TestModel';
import React, { useCallback } from 'react';
import { SubmitHandler, useForm } from 'react-hook-form';

import Button from '@mui/material/Button';

import TestQuestion from './TestQuestion';

interface TestRunnerProps {
  testItem: TestModel;
  onSubmit: (answers: {[qId: number]: string}) => void;
}

const TestRunner: React.FC<TestRunnerProps> = (props) => {
  const { testItem, onSubmit } = props;

  const onSubmitInternal = useCallback<SubmitHandler<{[qId: number]: string}>>(data => {
    console.log(data);
    if(onSubmit) {
        onSubmit(data);
    }
  }, [onSubmit]);

  const { control, handleSubmit } = useForm({
    defaultValues: testItem.questions.reduce<{ [qId: number]: string }>(
      (o, q) => {
        o[q.id] = '';
        return o;
      },
      {}
    ),
  });
  return (
    <div>
      <form onSubmit={handleSubmit(onSubmitInternal)}>
        {testItem.questions.map((q) => (
          <TestQuestion key={q.id} question={q} formControl={control} />
        ))}

        <Button type="submit">Submit</Button>
      </form>
    </div>
  );
};

export default TestRunner;
