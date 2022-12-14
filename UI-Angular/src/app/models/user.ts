import { last } from "rxjs";

export class User{
    id!: number;
    username: string;
    password: string;
    emailAddress: string;
    firstName: string;
    lastName: string;
    birthDate: Date;
    registrationDate: Date;
    role: Role;
    userStatus: UserStatus;
    photoRatingIds: Array<Number>;
    photoIds: Array<Number>;
    albumIds: Array<Number>;
    reportIds: Array<Number>;

    constructor(username: string, password: string, emailAddress: string,
         firstName: string, lastName: string, birthDate: Date, 
         registrationDate: Date, role: Role, userStatus: UserStatus, 
         photoRatingIds: Array<Number>, photoIds: Array<Number>, albumIds: Array<Number>, reportIds: Array<Number>){
            this.username = username;
            this.password = password;
            this.emailAddress = emailAddress;
            this.firstName = firstName;
            this.lastName = lastName;
            this.birthDate = birthDate;
            this.registrationDate = registrationDate;
            this.role = role;
            this.userStatus = userStatus;
            this.photoRatingIds = photoRatingIds;
            this.photoIds = photoIds;
            this.albumIds = albumIds;
            this.reportIds = reportIds;
         }

}

export enum Role{
    Admin,
    User
}
export enum UserStatus{
    Active,
    Banned
}