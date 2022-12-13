import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ValidationErrors, Validators} from '@angular/forms';
import { first } from 'rxjs';
import { Photo } from 'src/app/models/photo';
import { PhotoService } from 'src/app/services/photo.service';
import { ValidateImageType } from 'src/app/shared/custom_validators';

@Component({
  selector: 'app-upload-photo',
  templateUrl: './upload-photo.component.html',
  styleUrls: ['./upload-photo.component.css']
  
})
export class UploadPhotoComponent implements OnInit {
  
  form: FormGroup
  constructor(private photoService: PhotoService){
    this.form = new FormGroup({
      'title': new FormControl('', [Validators.required, Validators.minLength(3), Validators.pattern('[-.,!? A-Za-z0-9]+')]),
      'description': new FormControl('', [Validators.required, Validators.minLength(3), Validators.pattern('[-.,!? A-Za-z0-9]+')]),
      'tags': new FormControl('', Validators.pattern('[_ A-Za-z]*')),
      'image': new FormControl(null,[Validators.required, ValidateImageType(['png','jpg'])])
    });
  }

  ngOnInit(): void {
    
  }

  uploadPhoto(): void{
    const title = this.form.get('title')?.value;
    const desctiption = this.form.get('description')?.value;
    const image = this.form.get('image')?.value;
    const date = new Date();
    const photo = new Photo(image.name, title, desctiption,date, 1, 0, [],[],[],"johny1");

    this.photoService.createPhoto(photo).pipe(
      first()
    ).subscribe();

    const tags_array:Array<string> = this.form.get('tags')?.value.split(" ");

  }

 
}
