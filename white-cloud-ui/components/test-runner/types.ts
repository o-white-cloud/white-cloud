export interface QuestionsData {
  [qId: number]: string;
}

export interface TestFormData {
  email: string;
  questions: QuestionsData;
}
