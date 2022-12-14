import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { Album } from 'src/app/models/album';
import { Photo } from 'src/app/models/photo';
import { AlbumService } from 'src/app/services/album.service';
import { ModalsService } from 'src/app/services/modals.service';

@Component({
  selector: 'app-album',
  templateUrl: './album.component.html',
  styleUrls: ['./album.component.css']
})
export class AlbumComponent implements OnInit {

  album$: Observable<Album> | undefined;
  photos$: Observable<Photo[]> | undefined;
  constructor(private route: ActivatedRoute, 
    private albumService: AlbumService,
    public modalsService: ModalsService){

  }

  ngOnInit(): void {
    this.route.params.subscribe(params =>{
      this.album$ = this.albumService.getAlbumById(params['id']);
      this.photos$ =  this.albumService.getPhotosByAlbumId(params['id']);
    });

    

  }

}
