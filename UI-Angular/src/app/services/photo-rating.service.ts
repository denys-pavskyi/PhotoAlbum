import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { PhotoRating } from '../models/photoRating';
import { ErrorService } from './error.service';

@Injectable({
  providedIn: 'root'
})
export class PhotoRatingService {

  photoRatingURL: string = 'http://localhost:47392/api/photoRating';

  constructor(private http: HttpClient, private errorService: ErrorService) {
    
   }

   getById(id: number):Observable<PhotoRating>{
    return this.http.get<PhotoRating>(`${this.photoRatingURL}/${id}`).pipe(
      catchError(this.errorHandler.bind(this))
    )
   }

   getByUserIdAndPhotoId(userId: number, photoId: number):Observable<PhotoRating>{
    return this.http.get<PhotoRating>(`${this.photoRatingURL}/hasRated/${userId}/${photoId}`);
   }

   updatePhotoRating(id: number, photoRating: PhotoRating){
    return this.http.put<PhotoRating>(`${this.photoRatingURL}/${id}`, photoRating).pipe(
      catchError(this.errorHandler.bind(this))
    )
   }

   createPhotoRating(photoRating: PhotoRating): Observable<Object>{
    return this.http.post(`${this.photoRatingURL}`, photoRating, { responseType: 'text' }).pipe(
      catchError(this.errorHandler.bind(this))
    );
   }

   private errorHandler(error: HttpErrorResponse){
    this.errorService.handle(error.message);
    return throwError(() => error.message)
  } 

}
