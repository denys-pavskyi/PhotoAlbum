import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AccountService } from '../services/account.service';
import { ErrorService } from '../services/error.service';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {

  constructor(private router: Router,
    public errorService: ErrorService,
    private accountService: AccountService){

  }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    
    if(this.accountService.isAdmin){
      return true;
    }else{
      this.router.navigate(['/home']);
      this.errorService.error$.next("You don't have permission for this")
      return false;
    }
    
      
  }
  
}
