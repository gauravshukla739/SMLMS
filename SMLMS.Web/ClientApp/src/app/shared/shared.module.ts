import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { SharedService } from './services/shared.service.';
import { PaginationComponent } from './components/pagination/pagination.component';
import { ConfirmDialogComponent } from './components/confirm-dialog/confirm-dialog.component';
import { ConfirmDialogService } from './services/confirm-dialog-service.service';
import { CommonModule } from '@angular/common';



@NgModule({
  declarations: [

    PaginationComponent,
    ConfirmDialogComponent],
  imports: [
   // BrowserModule,
    CommonModule 
  ],
  exports: [
    CommonModule ,
    PaginationComponent,
    ConfirmDialogComponent
  ],
  providers: [SharedService, ConfirmDialogService],

  //bootstrap: []
})
export class SharedModule { }
