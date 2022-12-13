import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Album } from 'src/app/models/album';
import { Photo } from 'src/app/models/photo';
import { AlbumService } from 'src/app/services/album.service';


@Component({
  selector: 'app-album-elem',
  templateUrl: './album-elem.component.html',
  styleUrls: ['./album-elem.component.css']
})
export class AlbumElemComponent implements OnInit {
  @Input()album!: Album;
  
  photo$!: Observable<Photo>
  
  constructor(private albumService: AlbumService){

  }
  ngOnInit(): void {
    if(this.album.numberOfPictures>0){
      this.photo$ = this.albumService.getPreviewPhotoByAlbumId(this.album.id);
    }
   
  }
  


  
}
