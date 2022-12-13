import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, tap, throwError } from 'rxjs';
import { Report, ReportStatus } from '../models/report';
import { ErrorService } from './error.service';

@Injectable({
  providedIn: 'root'
})



export class ReportService {
  reportURL: string = 'http://localhost:47392/api/report';

  constructor(private http: HttpClient, private errorService: ErrorService) {

   }

   reports: Report[] = [];
   reportsOnReview: Report[] = [];
   reportsCompleted: Report[] = [];
   report!: Report;

   getAll(): Observable<Report[]>{
    return this.http.get<Report[]>(`${this.reportURL}s`).pipe(
      tap(reports => this.reports = reports),
      catchError(this.errorHandler.bind(this))
    )
   }

   getAllOnReview(): Observable<Report[]>{
    return this.http.get<Report[]>(`${this.reportURL}s/onReview`).pipe(
      tap(reportsOnReview => this.reportsOnReview = reportsOnReview),
      catchError(this.errorHandler.bind(this))
    )
   }

   getAllCompleted(): Observable<Report[]>{
    return this.http.get<Report[]>(`${this.reportURL}s/completed`).pipe(
      tap(reportsCompleted => this.reportsCompleted = reportsCompleted),
      catchError(this.errorHandler.bind(this))
    )
    
   }


   getById(id: number): Observable<Report>{
    return this.http.get<Report>(`${this.reportURL}/${id}`).pipe(
      tap(report => this.report = report),
      catchError(this.errorHandler.bind(this))
    )
   }

   updateReport(id: number, report: Report){
    return this.http.put<Report>(`${this.reportURL}/${id}`, report).pipe(
      catchError(this.errorHandler.bind(this))
    )
   }


   private errorHandler(error: HttpErrorResponse){
    this.errorService.handle(error.message);
    return throwError(() => error.message)
  } 
}
