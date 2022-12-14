import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginRequest } from 'src/app/models/loginReqest';
import { LoginResponse } from 'src/app/models/loginResponse';
import { AccountService } from 'src/app/services/account.service';
import { ErrorService } from 'src/app/services/error.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  form:FormGroup;
  loginResponse: LoginResponse | undefined
  isLogged = false;

  constructor(private accountService: AccountService, private router: Router,
    private errorService: ErrorService){
    this.form = new FormGroup({
      'username': new FormControl('', [Validators.required, Validators.minLength(4), Validators.pattern('[_&$A-Za-z0-9]+')]),
      'password': new FormControl('', [Validators.required, Validators.minLength(8), Validators.pattern('[&@_$A-Za-z0-9]+')]),
      
    });
  }
  
  ngOnInit(): void {
    
  }
  
  login(){
    const username = this.form.get('username')?.value;
    const password = this.form.get('password')?.value;
    let loginReqest = new LoginRequest(username, password);
    this.accountService.login(loginReqest).subscribe({
      next: (data => {
        console.log(data);
        this.accountService.saveSession(data);
        this.errorService.clear();
        this.router.navigate(['/home'])
      })
    });

    
  }
}
