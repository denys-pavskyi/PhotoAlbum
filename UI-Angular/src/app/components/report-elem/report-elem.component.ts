import { Component, Input } from '@angular/core';
import { Observable } from 'rxjs';
import { Photo } from 'src/app/models/photo';
import { Report } from 'src/app/models/report';
import { PhotoService } from 'src/app/services/photo.service';
import { ReportService } from 'src/app/services/report.service';

@Component({
  selector: 'app-report-elem',
  templateUrl: './report-elem.component.html',
  styleUrls: ['./report-elem.component.css']
})
export class ReportElemComponent {
  @Input()report!: Report;

  photo$!: Observable<Photo>
  
  constructor(private photoService: PhotoService, 
    private reportService: ReportService){

  }
  ngOnInit(): void {
    this.photo$ = this.photoService.getById(this.report.photoId);
  }
  
  statusToString(status: number){
    if(status == 0){
      return "Approved";
    }
    if(status == 1){
      return "Declined";
    }else{
      return "OnReview";
    }
    
  }

  approve(id: number){
    this.report.status = 0;
    this.reportService.updateReport(id, this.report).subscribe();
  }

  decline(id: number){
    this.report.status = 1;
    this.reportService.updateReport(id, this.report).subscribe();
  }
}
