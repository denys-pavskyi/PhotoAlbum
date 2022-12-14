import { Component, OnInit } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { Album } from 'src/app/models/album';
import { AlbumService } from 'src/app/services/album.service';
import { ModalsService } from 'src/app/services/modals.service';

@Component({
  selector: 'app-albums-list',
  templateUrl: './albums-list.component.html',
  styleUrls: ['./albums-list.component.css']
})
export class AlbumsListComponent implements OnInit {


  loading = false;
  albums$: Observable<Album[]> | undefined
  constructor(public albumService: AlbumService, public modalsService:ModalsService){

  }
  ngOnInit(): void {
    this.loading = true;
    this.albums$ = this.albumService.getAllByUserId(Number(window.localStorage.getItem('ID'))).pipe(
      tap(() => this.loading = false)
    );

    
  }

}
