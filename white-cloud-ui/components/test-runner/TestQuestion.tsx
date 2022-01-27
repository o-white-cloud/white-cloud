import { ChoiceQuestion, Question } from 'models/TestModel';
import React from 'react';
import { Control, Controller, FieldError } from 'react-hook-form';

import {
    Checkbox, FormControl, FormControlLabel, FormGroup, FormHelperText, Paper, Radio, RadioGroup,
    styled, Typography
} from '@mui/material';

import { TestFormData } from './types';

interface TestQuestionProps<QT extends Question = Question> {
  question: QT;
  formControl: Control<TestFormData, object>;
  error: FieldError | undefined;
}

const MultipleChoiceAnswers: React.FC<TestQuestionProps<ChoiceQuestion>> = (
  props
) => {
  const { question } = props;
  return (
    <FormGroup row>
      {question.answers.map((a) => (
        <FormControlLabel control={<Checkbox />} label={a.name} key={a.id} />
      ))}
    </FormGroup>
  );
};

const SingleChoiceAnswers: React.FC<TestQuestionProps<ChoiceQuestion>> = (
  props
) => {
  const { question } = props;
  return (
    <Controller
      name={`questions.${question.id}`}
      control={props.formControl}
      rules={{
        required: {
          value: true,
          message: 'Every question needs to be answered',
        },
      }}
      render={({ field }) => (
        <RadioGroup row {...field}>
          {question.answers.map((a) => (
            <FormControlLabel
              value={a.value.toString()}
              control={<Radio />}
              label={a.name}
              key={a.id}
            />
          ))}
        </RadioGroup>
      )}
    />
  );
};

const TestQuestionAnswers: React.FC<TestQuestionProps> = (props) => {
  const { question } = props;
  switch (question.type) {
    case 'choice':
      return question.multiple ? (
        <MultipleChoiceAnswers
          question={question}
          formControl={props.formControl}
          error={props.error}
        />
      ) : (
        <SingleChoiceAnswers
          question={question}
          error={props.error}
          formControl={props.formControl}
        />
      );
    default:
      return <div>unknown question type</div>;
  }
};

const QuestionPaper = styled(Paper)(({ theme }) => ({
  marginBottom: theme.spacing(4),
  padding: theme.spacing(2),
}));

const TestQuestion: React.FC<TestQuestionProps> = (props) => {
  const { question } = props;

  return (
    <QuestionPaper>
      <FormControl error={props.error !== undefined}>
        <Typography variant="h5">
          {question.id}. {question.title}
        </Typography>
        <TestQuestionAnswers
          error={props.error}
          question={question}
          formControl={props.formControl}
        />
        <FormHelperText>{props.error?.message}</FormHelperText>
      </FormControl>
    </QuestionPaper>
  );
};

export default TestQuestion;
