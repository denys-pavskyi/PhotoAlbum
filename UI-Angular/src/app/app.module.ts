import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { CreateAlbumComponent } from './components/create-album/create-album.component';
import { AlbumsListComponent } from './components/albums-list/albums-list.component';
import { AlbumComponent } from './components/album/album.component';
import { ReportsListComponent } from './components/reports-list/reports-list.component';
import { ReportComponent } from './components/report/report.component';
import { PhotoElemComponent } from './components/photo-elem/photo-elem.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { UploadPhotoComponent } from './components/upload-photo/upload-photo.component';
import { PhotoComponent } from './components/photo/photo.component';
import { PhotoPreviewComponent } from './components/photo-preview/photo-preview.component';
import { ModalComponent } from './components/modal/modal.component';
import { AddToAlbumComponent } from './components/add-to-album/add-to-album.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations'
import { CommonModule } from '@angular/common';
import { FileUploadComponent } from './components/file-upload/file-upload.component';
import { AlbumElemComponent } from './components/album-elem/album-elem.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { ReportElemComponent } from './components/report-elem/report-elem.component';
import { ReportPhotoComponent } from './components/report-photo/report-photo.component';
import { GlobalErrorComponent } from './components/global-error/global-error.component';
import { AuthGuard } from './guards/auth.guard';
import { AdminGuard } from './guards/admin.guard';
import { StarRatingComponent } from './components/star-rating/star-rating.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    CreateAlbumComponent,
    AlbumsListComponent,
    AlbumComponent,
    ReportsListComponent,
    ReportComponent,
    PhotoElemComponent,
    NavMenuComponent,
    PhotoComponent,
    PhotoPreviewComponent,
    ModalComponent,
    AddToAlbumComponent,
    UploadPhotoComponent,
    FileUploadComponent,
    AlbumElemComponent,
    RegistrationComponent,
    ReportElemComponent,
    ReportPhotoComponent,
    GlobalErrorComponent,
    StarRatingComponent
  ],
  imports: [
    BrowserModule, HttpClientModule, 
    ReactiveFormsModule, FormsModule, BrowserAnimationsModule,
    CommonModule,

    RouterModule.forRoot([
      {path: '', redirectTo: '/home', pathMatch: 'full'},
      {path: 'home', component: HomeComponent},
      {path: 'login', component: LoginComponent},
      {path: 'upload-photo', component: UploadPhotoComponent, canActivate: [AuthGuard]},
      {path: 'reports-list', component: ReportsListComponent, canActivate: [AuthGuard,AdminGuard]},
      {path: 'albums-list', component: AlbumsListComponent, canActivate: [AuthGuard]},
      {path: 'registration', component: RegistrationComponent},
      {path: 'album/:id', component: AlbumComponent, canActivate: [AuthGuard]},
      {path: 'photo/:id', component: PhotoComponent, canActivate: [AuthGuard]},
      { path: "**",redirectTo:"/home"}
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
