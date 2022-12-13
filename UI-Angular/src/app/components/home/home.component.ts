import { Component, OnInit } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { Photo } from 'src/app/models/photo';
import { ModalsService } from 'src/app/services/modals.service';
import { PhotoService } from 'src/app/services/photo.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  loading = false;
  photos$: Observable<Photo[]> | undefined
  constructor(public photoService: PhotoService, public modalsService:ModalsService){

  }
  ngOnInit(): void {
    this.loading = true;
    this.photos$ = this.photoService.getAll().pipe(
      tap(() => this.loading = false)
    );

    
  }


  

}
