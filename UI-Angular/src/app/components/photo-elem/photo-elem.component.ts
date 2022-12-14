import { Component, Input, OnInit } from '@angular/core';
import { Photo } from 'src/app/models/photo';
import { AccountService } from 'src/app/services/account.service';
import { ModalsService } from 'src/app/services/modals.service';

@Component({
  selector: 'app-photo-elem',
  templateUrl: './photo-elem.component.html',
  styleUrls: ['./photo-elem.component.css']
})
export class PhotoElemComponent implements OnInit {
  @Input()photo!: Photo;
  @Input()modalsService!: ModalsService;

  constructor(public accountService: AccountService){

  }
  ngOnInit(): void {
    
  }
}
