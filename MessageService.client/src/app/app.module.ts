//import { HttpClientModule } from '@angular/common/http';
//import { NgModule } from '@angular/core';
//import { BrowserModule } from '@angular/platform-browser';

//import { AppRoutingModule } from './app-routing.module';
//import { AppComponent } from './app.component';

//@NgModule({
//  declarations: [
//    AppComponent
//  ],
//  imports: [
//    BrowserModule, HttpClientModule,
//    AppRoutingModule
//  ],
//  providers: [],
//  bootstrap: [AppComponent]
//})
//export class AppModule { }
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
/*import { NavMenuComponent } from './nav-menu/nav-menu.component';*/
import { AboutComponent } from './home/home.component';
import { PlantDataComponent } from './plant-list/plant-list.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    AboutComponent,
    //GamesDataComponent,
    //GameDataComponent,
    PlantDataComponent

  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: 'app-nav-menu', component: NavMenuComponent },
      { path: 'about', component: AboutComponent },
      { path: 'plant-list', component: PlantDataComponent },
      /*{ path: '', component: PlantDataComponent, pathMatch: 'full' },*/
      { path: '', component: AboutComponent, pathMatch: 'full' },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
