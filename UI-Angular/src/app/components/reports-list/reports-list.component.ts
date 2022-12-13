import { Component, OnInit } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { Report } from 'src/app/models/report';
import { ReportService } from 'src/app/services/report.service';

@Component({
  selector: 'app-reports-list',
  templateUrl: './reports-list.component.html',
  styleUrls: ['./reports-list.component.css']
})
export class ReportsListComponent implements OnInit {

  loading = false;
  reports$: Observable<Report[]> | undefined
  reportsOnReview$: Observable<Report[]> | undefined
  reportsCompleted$: Observable<Report[]> | undefined

  constructor(public reportService: ReportService){

  }

  ngOnInit(): void {
    this.loading = true;
    this.reportsOnReview$ = this.reportService.getAllOnReview().pipe(
      tap(() => this.loading = false)
    );
    
    this.reportsCompleted$ = this.reportService.getAllCompleted().pipe(
      tap(() => this.loading = false)
    );
    
  }

}
