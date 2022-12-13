import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs';
import { Album } from 'src/app/models/album';
import { AlbumService } from 'src/app/services/album.service';

@Component({
  selector: 'app-create-album',
  templateUrl: './create-album.component.html',
  styleUrls: ['./create-album.component.css']
})
export class CreateAlbumComponent implements OnInit {
  
  form:FormGroup
  

  constructor(private albumService: AlbumService){
    this.form = new FormGroup({
      'title': new FormControl('', [Validators.required, Validators.minLength(3), Validators.pattern('[-.,!? A-Za-z0-9]+')]),
      'description': new FormControl('', [Validators.required, Validators.minLength(3), Validators.pattern('[-.,!? A-Za-z0-9]+')]),
      
    });
  }
  ngOnInit(): void {
    
  }

  createAlbum(){
    const title = this.form.get('title')?.value;
    const description = this.form.get('description')?.value;
    const numberOfPictures = 0;
    const userId = 2;
    const creationDate = new Date();

    let album = new Album(title, description, numberOfPictures, userId, creationDate,[]);
    this.albumService.createAlbum(album).pipe(
      first()
    ).subscribe(response =>
      console.log(response)
      );
      this.albumService.albums.push(album);
      
  }
}
