import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { ProfileComponent } from './components/profile/profile.component';
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

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    ProfileComponent,
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
    ReportPhotoComponent
  ],
  imports: [
    BrowserModule, HttpClientModule, 
    ReactiveFormsModule, FormsModule, BrowserAnimationsModule,
    CommonModule,

    RouterModule.forRoot([
      {path: '', redirectTo: '/home', pathMatch: 'full'},
      {path: 'home', component: HomeComponent},
      {path: 'login', component: LoginComponent},
      {path: 'profile', component: ProfileComponent},
      {path: 'upload-photo', component: UploadPhotoComponent},
      {path: 'reports-list', component: ReportsListComponent},
      {path: 'albums-list', component: AlbumsListComponent}
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
