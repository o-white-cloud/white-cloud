export interface ClientInvite {
    id: number;
    therapistUserId : string;
    sentDate: string;
    email: string;
    acceptedDate?: string;
}