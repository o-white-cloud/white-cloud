import { ClientTestRequest } from 'models';
import { TestModel } from 'models/TestModel';
import React, { useCallback, useEffect } from 'react';
import { Controller, Ref, SubmitHandler, useForm } from 'react-hook-form';

import { Alert, Stack, TextField } from '@mui/material';
import Button from '@mui/material/Button';
import { styled } from '@mui/system';
import { unstable_useForkRef } from '@mui/utils';

import TestQuestion from './TestQuestion';
import { QuestionsData, TestFormData } from './types';

interface TestRunnerProps {
  testItem: TestModel;
  onSubmit: (answers: { [qId: number]: string }) => void;
  testRequest: ClientTestRequest | null;
}

const RootDiv = styled('div')(({ theme }) => ({
  marginBottom: theme.spacing(6),
}));

const TestRunner: React.FC<TestRunnerProps> = (props) => {
  const { testItem, onSubmit, testRequest } = props;

  const onSubmitInternal = useCallback<SubmitHandler<TestFormData>>(
    (data) => {
      if (onSubmit) {
        onSubmit(data.questions);
      }
    },
    [onSubmit]
  );

  const {
    control,
    handleSubmit,
    formState: { errors, isSubmitting },
  } = useForm<TestFormData>({
    defaultValues: {
      questions: testItem.questions.reduce<QuestionsData>((o, q) => {
        o[q.id] = '';
        return o;
      }, {}),
    },
  });

  useEffect(() => {
    if (errors) {
      let elementName: string = '';
      if (errors.questions && Object.values(errors.questions).length > 0) {
        const questionId = Object.keys(errors.questions)[0];
        elementName = `questions.${questionId}`;
      }

      if (elementName) {
        let firstErrorElement = document.getElementsByName(elementName)[0];
        firstErrorElement?.scrollIntoView({
          behavior: `smooth`,
          block: 'center',
        });
      }
    }
  }, [errors, errors.questions]);

  return (
    <RootDiv>
      <form onSubmit={handleSubmit(onSubmitInternal)}>
        {testItem.questions.map((q) => (
          <TestQuestion
            key={q.id}
            question={q}
            formControl={control}
            error={
              errors && errors.questions ? errors.questions[q.id] : undefined
            }
          />
        ))}

        <Stack direction='row'>
          <Button variant="contained" type="submit">
            Submit
          </Button>
          {testRequest && (
            <Alert severity="info" sx={{marginLeft: 2}}>
              Rezultatele vor fi automat trimise catre{' '}
              {testRequest.therapistUserFirstName}{' '}
              {testRequest.therapistUserLastName}.
            </Alert>
          )}
        </Stack>
      </form>
    </RootDiv>
  );
};

export default TestRunner;
