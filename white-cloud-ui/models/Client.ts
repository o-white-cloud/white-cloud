export interface Client {
    id: number;
    therapistId: number;
    userId: string;
    clientDate: string;
    userEmail: string;
    userFirstName: string;
    userLastName: string;
    age: number;
    gender: Gender;
    ocupation: string;
}

export enum Gender {
    Female = "Female",
    Male = "Male",
    Other = "Other"
}