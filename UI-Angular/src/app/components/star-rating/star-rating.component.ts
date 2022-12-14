import { Component, Input, OnInit } from '@angular/core';
import { first } from 'rxjs';
import { PhotoRating } from 'src/app/models/photoRating';
import { ErrorService } from 'src/app/services/error.service';
import { PhotoRatingService } from 'src/app/services/photo-rating.service';

@Component({
  selector: 'app-star-rating',
  templateUrl: './star-rating.component.html',
  styleUrls: ['./star-rating.component.css']
})
export class StarRatingComponent implements OnInit {
  @Input()photoId!: number;
  
  rate: number;
  constructor(private photoRatingService: PhotoRatingService, public errorService: ErrorService){
    this.rate = 0;
  }



  ratePhoto(grade: number){
    let hasRated = false;
    let photoRating!: PhotoRating;
    const userId = Number(window.localStorage.getItem('ID'));
    this.photoRatingService.getByUserIdAndPhotoId(userId, this.photoId)
    .subscribe(
       x => {
        hasRated = x!=null?true:false
       }
       
    )

    console.log('hasRated = '+ hasRated);

    // if(hasRated){
    //   let photoRatingPost!: PhotoRating;
    //   this.photoRatingService.getById(photoRating.id)
    //   .subscribe(
    //     x=>photoRatingPost = x
    //   );
    //   this.photoRatingService.updatePhotoRating(photoRating.id, photoRatingPost)
    //   .subscribe(
    //     ()=>first()
    //   );
    //   return true;
    // }else{
    //   const photoId = this.photoId;
    //   const ratingDate = new Date();
    //   const photoRating = new PhotoRating(photoId, userId, grade, ratingDate);
    //   this.photoRatingService.createPhotoRating(photoRating).subscribe(
    //     ()=>first()
    //   );
    //   return true;
    // }


  }

  
  
  ngOnInit(): void {
    
  }
  
}
