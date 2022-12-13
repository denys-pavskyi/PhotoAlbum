export class LoginResponse{
    username: string;
    role: string
    id: number;
    token: string;

    constructor(username: string, role:string, id: number, token:string){
        this.username = username;
        this.role = role;
        this.id = id;
        this.token = token;
    }
}