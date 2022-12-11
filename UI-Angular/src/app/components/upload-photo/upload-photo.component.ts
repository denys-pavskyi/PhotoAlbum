import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ValidationErrors, Validators} from '@angular/forms';

@Component({
  selector: 'app-upload-photo',
  templateUrl: './upload-photo.component.html',
  styleUrls: ['./upload-photo.component.css']
  
})
export class UploadPhotoComponent implements OnInit {
  
  form: FormGroup
  constructor(){
    this.form = new FormGroup({
      'title': new FormControl('', [Validators.required, Validators.minLength(3)]),
      'description': new FormControl('', [Validators.required, Validators.minLength(3)]),
      'tags': new FormControl(''),
      'image': new FormControl('',[Validators.required])
    });
  }

  ngOnInit(): void {
    
  }

  uploadPhoto(): void{

  }

  requiredFileType(type: string): ValidationErrors| null{
    return null;
  }

}
