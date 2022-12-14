import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap, catchError, throwError } from 'rxjs';
import { Album } from '../models/album';
import { Photo } from '../models/photo';
import { ErrorService } from './error.service';

@Injectable({
  providedIn: 'root'
})
export class AlbumService {

  albumURL: string = 'http://localhost:47392/api/album';

  constructor(private http: HttpClient, private errorService: ErrorService) {
    
  }

  albums: Album[] = []

  getAllByUserId(userId: number): Observable<Album[]>{
   return this.http.get<Album[]>(`${this.albumURL}/user/${userId}`).pipe(
     tap(albums => this.albums = albums),
     catchError(this.errorHandler.bind(this))
   )
  }
  getAlbumById(id: number): Observable<Album>{
    return this.http.get<Album>(`${this.albumURL}/${id}`).pipe(
      catchError(this.errorHandler.bind(this))
    )
  }

  createAlbum(album: Album): Observable<Object>{
   return this.http.post(`${this.albumURL}`, album, { responseType: 'text' }).pipe(
    catchError(this.errorHandler.bind(this))
    );
  }

  getPreviewPhotoByAlbumId(albumId: number): Observable<Photo>{
    return this.http.get<Photo>(`${this.albumURL}/${albumId}/preview`).pipe(
      catchError(this.errorHandler.bind(this))
    )
  }

  getPhotosByAlbumId(albumId: number): Observable<Photo[]>{
    return this.http.get<Photo[]>(`${this.albumURL}/${albumId}/photos`).pipe(
      catchError(this.errorHandler.bind(this))
    )
  }
  private errorHandler(error: HttpErrorResponse){
   this.errorService.handle(error.message);
   return throwError(() => error.message)
 } 
}
