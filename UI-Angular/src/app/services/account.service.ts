import { HttpClient, HttpErrorResponse, HttpParams  } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { LoginRequest } from '../models/loginReqest';
import { LoginResponse } from '../models/loginResponse';
import { User } from '../models/user';
import { ErrorService } from './error.service';

@Injectable({
  providedIn: 'root'
})
export class AccountService implements OnInit {

  
  loginURL: string = 'http://localhost:47392/api/login';
  registrationURL: string ='http://localhost:47392/api/user';

  constructor(private http: HttpClient, private errorService: ErrorService) {
    
   }
  ngOnInit(): void {
  }

  login(loginRequest: LoginRequest):Observable<LoginResponse>{
    return this.http.post<LoginResponse>(`${this.loginURL}`, loginRequest ).pipe(
      catchError(this.errorHandler.bind(this))
    );
  }

  register(user: User):Observable<Object>{
    return this.http.post<User>(`${this.registrationURL}`, user).pipe(
      catchError(this.errorHandler.bind(this))
    );
  }
  
  get isLogged(): boolean{
    if(window.localStorage.getItem("Token")){
      return true;
    }else{
      return false;
    }
  }

  get isAdmin():boolean{
    let role = window.localStorage.getItem('Role');
    if(role){
      if(role=="Admin"){
        return true;
      }else{
        return false;
      }
    }else{
      return false;
    }
  }

  get userId(): number{
    if(window.localStorage.getItem('ID')){
      return Number(window.localStorage.getItem('ID'));
    }else{
      return -1;
    }
  }

  saveSession(loginResponse: LoginResponse) {
    window.localStorage.setItem('Username', loginResponse.username);
    window.localStorage.setItem('Role', loginResponse.role);
    window.localStorage.setItem('ID', loginResponse.id.toString());
    window.localStorage.setItem('Token', loginResponse.token);
  }

  

  logout() {
    window.localStorage.clear();
  }

  private errorHandler(error: HttpErrorResponse){
    this.errorService.handle(error.message);
    return throwError(() => error.message)
  } 
}
