import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, tap, throwError } from 'rxjs';
import { Report } from '../models/report';
import { ErrorService } from './error.service';

@Injectable({
  providedIn: 'root'
})



export class ReportService {
  reportURL: string = 'http://localhost:47392/api/photo';

  constructor(private http: HttpClient, private errorService: ErrorService) {
   }

   reports: Report[] = [];

   getAll(): Observable<Report[]>{
    return this.http.get<Report[]>(`${this.reportURL}s`).pipe(
      tap(reports => this.reports = reports),
      catchError(this.errorHandler.bind(this))
    )
   }

   private errorHandler(error: HttpErrorResponse){
    this.errorService.handle(error.message);
    return throwError(() => error.message)
  } 
}
