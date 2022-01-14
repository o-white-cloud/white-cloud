import { ChoiceQuestion, Question } from 'models/TestModel';
import React from 'react';
import { Control, Controller } from 'react-hook-form';

import {
    Checkbox, FormControlLabel, FormGroup, Radio, RadioGroup, Typography
} from '@mui/material';

interface TestQuestionProps<QT extends Question = Question> {
  question: QT;
  formControl: Control<
    {
      [qId: number]: string;
    },
    object
  >;
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
      name={question.id.toString()}
      control={props.formControl}
      render={({ field }) => (
        <RadioGroup row {...field}>
          {question.answers.map((a) => (
            <FormControlLabel
              value={a.id.toString()}
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
        />
      ) : (
        <SingleChoiceAnswers
          question={question}
          formControl={props.formControl}
        />
      );
    default:
      return <div>unknown question type</div>;
  }
};

const TestQuestion: React.FC<TestQuestionProps> = (props) => {
  const { question } = props;

  return (
    <div>
      <Typography variant="h5">{question.title}</Typography>
      <TestQuestionAnswers
        question={question}
        formControl={props.formControl}
      />
    </div>
  );
};

export default TestQuestion;