export interface TestModel {
  id: number;
  icon: string;
  name: string;
  excerpt: string;
  description: string;

  questions: Question[];
}

export type QuestionType = 'choice' | 'input' | 'range';

export type Question = ChoiceQuestion | InputQuestion | RangeQuestion;

interface QuestionBase {
  id: number;
  title: string;
}

export interface ChoiceQuestion extends QuestionBase {
  type: 'choice';
  multiple?: boolean;
  answers: {
    id: number;
    name: string;
    value: number;
  }[];
}

export interface InputQuestion extends QuestionBase {
  type: 'input';
}

export interface RangeQuestion extends QuestionBase {
  type: 'range';
  min: number;
  max: number;
}
