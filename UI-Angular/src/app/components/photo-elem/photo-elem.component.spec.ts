import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhotoElemComponent } from './photo-elem.component';

describe('PhotoElemComponent', () => {
  let component: PhotoElemComponent;
  let fixture: ComponentFixture<PhotoElemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PhotoElemComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PhotoElemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
