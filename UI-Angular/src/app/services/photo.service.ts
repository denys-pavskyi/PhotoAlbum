import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, tap, throwError } from 'rxjs';
import { Photo } from '../models/photo';
import { ErrorService } from './error.service';

@Injectable({
  providedIn: 'root'
})
export class PhotoService {

  constructor(private http: HttpClient, private errorService: ErrorService) {
    
   }

   photos: Photo[] = []

   getAll(): Observable<Photo[]>{
    return this.http.get<Photo[]>('http://localhost:47392/api/Photo').pipe(
      tap(photos => this.photos = photos),
      catchError(this.errorHandler.bind(this))
    )
   }


   private errorHandler(error: HttpErrorResponse){
    this.errorService.handle(error.message);
    return throwError(() => error.message)
  } 



}
