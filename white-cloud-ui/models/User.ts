export interface User {
    authenticated: boolean;
    id: string;
    email: string;
    firstName: string;
    lastName: string;
    roles: string[]
}

export enum Roles {
    Therapist = "Therapist",
    Admin = "Admin"
}