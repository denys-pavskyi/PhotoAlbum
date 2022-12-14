import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { Photo } from 'src/app/models/photo';
import { PhotoService } from 'src/app/services/photo.service';

@Component({
  selector: 'app-photo',
  templateUrl: './photo.component.html',
  styleUrls: ['./photo.component.css']
})
export class PhotoComponent implements OnInit {
 

  photo$: Observable<Photo> | undefined;
  constructor(private route: ActivatedRoute, 
    private photoService: PhotoService){

  }

  ngOnInit(): void {
    this.route.params.subscribe(params =>{
      this.photo$ =  this.photoService.getById(params['id']);
    });
  }
}
