import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Role, User, UserStatus } from 'src/app/models/user';
import { AccountService } from 'src/app/services/account.service';
import { ErrorService } from 'src/app/services/error.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  form:FormGroup;
  isLogged = false;

  constructor(private accountService: AccountService, private router: Router,
    private errorService: ErrorService){
    this.form = new FormGroup({
      'username': new FormControl('', [Validators.required, Validators.minLength(4), Validators.pattern('[_&$A-Za-z0-9]+')]),
      'password': new FormControl('', [Validators.required, Validators.minLength(8), Validators.pattern('[&@_$A-Za-z0-9]+')]),
      'firstName': new FormControl('', [Validators.required, Validators.minLength(1), Validators.pattern('[A-Za-z]+')]),
      'lastName': new FormControl('', [Validators.required, Validators.minLength(1), Validators.pattern('[A-Za-z]+')]),
      'birthDate': new FormControl(null, [Validators.required]),
      'emailAddress': new FormControl('', [Validators.email])
    });
  }
  
  ngOnInit(): void {
    
  }

  register(){
    const username = this.form.get('username')?.value;
    const password = this.form.get('password')?.value;
    const firstName = this.form.get('firstName')?.value;
    const lastName = this.form.get('lastName')?.value;
    const birthDate = this.form.get('birthDate')?.value;
    const emailAddress = this.form.get('emailAddress')?.value;

    const role = Role.User;
    const registrationDate = new Date();
    const userStatus = UserStatus.Active;

    const user = new User(username, password, emailAddress, firstName, lastName, 
      birthDate, registrationDate, role, userStatus, [], [], [], []);

    this.accountService.register(user).subscribe({
      next: (() => {
        this.router.navigate(['/login'])
      })
    });

  }

}
