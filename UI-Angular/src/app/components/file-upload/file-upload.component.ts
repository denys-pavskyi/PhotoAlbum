import { Component, ElementRef, HostListener, Input, OnInit } from '@angular/core';
import { NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css'],
  providers:[{
      provide: NG_VALUE_ACCESSOR,
      useExisting: FileUploadComponent,
      multi: true
    }
  ]
  
})
export class FileUploadComponent implements OnInit {
  
  @Input() progress = null;
  onChange!: Function;
  public file: File|null = null;

  @HostListener('change', ['$event.target.files']) emitFiles(event: FileList){
    const file = event && event.item(0);
    this.onChange(file);
    this.file = file;
  }
  
  constructor(private host: ElementRef<HTMLInputElement>){

  }
  
  ngOnInit(): void {
    
  }

  writeValue( value: null){
    this.host.nativeElement.value='';
    this.file = null;
  }

  registerOnChange(fn: Function){
    this.onChange = fn;
  }

  registerOnTouched(fn: Function){

  }

}
