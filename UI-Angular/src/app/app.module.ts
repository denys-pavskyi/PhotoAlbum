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
import { PhotoComponent } from './components/photo/photo.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { UploadPhotoComponent } from './components/upload-photo/upload-photo.component';

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
    PhotoComponent,
    NavMenuComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,

    RouterModule.forRoot([
      {path: '', redirectTo: '/home', pathMatch: 'full'},
      {path: 'home', component: HomeComponent},
      {path: 'login', component: LoginComponent},
      {path: 'profile', component: ProfileComponent},
      {path: 'upload-photo', component: UploadPhotoComponent}

    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
