import { Component, Input, OnInit } from '@angular/core';
import { ModalsService } from 'src/app/services/modals.service';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.css']
})
export class ModalComponent implements OnInit {
  @Input() title!:string 
  
  constructor(public modalsService: ModalsService){

  }
  ngOnInit(): void {
    
  }
}
