import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ModalsService {

  isReporting$ = new BehaviorSubject<boolean>(false)
  isAddingToAlbum$ = new BehaviorSubject<boolean>(false)
  isCreatingAlbum$ = new BehaviorSubject<boolean>(false);

  openReport(){
    this.isReporting$.next(true);
  }

  openAlbum(){
    this.isAddingToAlbum$.next(true);
  }

  openCreateAlbum(){
    this.isCreatingAlbum$.next(true);
  }

  close(){
    this.isReporting$.next(false);
    this.isAddingToAlbum$.next(false);
    this.isCreatingAlbum$.next(false);
  }
  constructor() { }
}
