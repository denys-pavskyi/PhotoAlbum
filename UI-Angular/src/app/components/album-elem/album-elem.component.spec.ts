import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AlbumElemComponent } from './album-elem.component';

describe('AlbumElemComponent', () => {
  let component: AlbumElemComponent;
  let fixture: ComponentFixture<AlbumElemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AlbumElemComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AlbumElemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
