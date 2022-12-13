import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportElemComponent } from './report-elem.component';

describe('ReportElemComponent', () => {
  let component: ReportElemComponent;
  let fixture: ComponentFixture<ReportElemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReportElemComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ReportElemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
