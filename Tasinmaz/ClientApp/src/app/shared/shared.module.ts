import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CollumOneComponent } from './layouts/collum-one/collum-one.component';
import { HeaderComponent } from './components/header/header.component';
import { RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import { AlertModule } from '@full-fledged/alerts';



@NgModule({
  declarations: [CollumOneComponent, HeaderComponent],
  imports: [
    CommonModule,
    RouterModule,
    BrowserAnimationsModule,
    BrowserModule,
    AlertModule.forRoot({maxMessages: 5, timeout: 5000})
  ],
  exports: [
    CollumOneComponent
  ]
})
export class SharedModule { }
