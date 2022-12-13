import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, tap, throwError } from 'rxjs';
import { Photo } from '../models/photo';
import { ErrorService } from './error.service';

@Injectable({
  providedIn: 'root'
})
export class PhotoService {

  photoURL: string = 'http://localhost:47392/api/photo';

  constructor(private http: HttpClient, private errorService: ErrorService) {
    
   }

   photos: Photo[] = [];
   photo: Photo | undefined;

   getAll(): Observable<Photo[]>{
    return this.http.get<Photo[]>(`${this.photoURL}s`).pipe(
      tap(photos => this.photos = photos),
      catchError(this.errorHandler.bind(this))
    )
   }

   getById(id: number):Observable<Photo>{
    return this.http.get<Photo>(`${this.photoURL}/${id}`).pipe(
      tap(photo => this.photo = photo),
      catchError(this.errorHandler.bind(this))
    )
   }

   createPhoto(photo: Photo): Observable<Object>{
    return this.http.post(`${this.photoURL}`, photo, { responseType: 'text' });
   }

   private errorHandler(error: HttpErrorResponse){
    this.errorService.handle(error.message);
    return throwError(() => error.message)
  } 

  deletePhoto(id: number): Observable<Object>{
    return this.http.delete(`${this.photoURL}/${id}`);
  }


}
