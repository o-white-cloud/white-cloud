import { TestModel } from 'models/TestModel';
import React, { useCallback, useEffect } from 'react';
import { Controller, Ref, SubmitHandler, useForm } from 'react-hook-form';

import { Stack, TextField } from '@mui/material';
import Button from '@mui/material/Button';
import { styled } from '@mui/system';
import { unstable_useForkRef } from '@mui/utils';

import TestQuestion from './TestQuestion';
import { QuestionsData, TestFormData } from './types';

interface TestRunnerProps {
  testItem: TestModel;
  onSubmit: (email: string, answers: { [qId: number]: string }) => void;
}

const RootDiv = styled('div')(({ theme }) => ({
  marginBottom: theme.spacing(6),
}));

const TestRunner: React.FC<TestRunnerProps> = (props) => {
  const { testItem, onSubmit } = props;

  const onSubmitInternal = useCallback<SubmitHandler<TestFormData>>(
    (data) => {
      if (onSubmit) {
        onSubmit(data.email, data.questions);
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
      email: '',
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
      } else if (errors.email) {
        elementName = 'email';
      }

      if (elementName) {
        let firstErrorElement = document.getElementsByName(elementName)[0];
        firstErrorElement?.scrollIntoView({
          behavior: `smooth`,
          block: 'center',
        });
      }
    }
  }, [errors.email, errors.questions]);

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

        <Stack direction="row" spacing={2}>
          <Controller
            name="email"
            control={control}
            defaultValue=""
            rules={{ required: { value: true, message: 'Email is required' } }}
            render={({ field }) => (
              <TextField
                label="Email"
                sx={{ minWidth: 400 }}
                {...field}
                helperText={errors.email?.message}
                error={errors.email !== undefined}
              />
            )}
          />
          <Button variant="contained" type="submit">
            Submit
          </Button>
        </Stack>
      </form>
    </RootDiv>
  );
};

export default TestRunner;
