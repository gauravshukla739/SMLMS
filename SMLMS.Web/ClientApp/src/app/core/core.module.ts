import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RequestInterceptor } from './intercepter/http-interceptor';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AuthenticationService } from './services/authentication.service';
import { UserService } from './services/user.service';




@NgModule({
  declarations: [
    
  ],
  imports: [
    BrowserModule  ,
    HttpClientModule 
  ],
  providers: [
    AuthenticationService,UserService,
    { provide: HTTP_INTERCEPTORS, useClass: RequestInterceptor, multi: true }
    
    ],
  //bootstrap: []
})
export class CoreModule { }
