export interface Client {
    id: number;
    therapistId: number;
    userId: string;
    clientDate: string;
    email: string;
    firstName: string;
    lastName: string;
    age: number;
    gender: Gender;
    ocupation: string;
}

export enum Gender {
    Female = 1,
    Male = 2,
    Other = 3
}