import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RequestInterceptor } from './intercepter/http-interceptor';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AuthenticationService } from './services/authentication.service';
import { UserService } from './services/user.service';
import { PasswordService } from './services/password.service';
import { DepartmentService } from './services/department.service';
import { RoleService } from './services/role.service';
import { PasswordValidator } from './directives/passwordvalidate.directive';




@NgModule({
  declarations: [
    PasswordValidator
  ],
  imports: [
    BrowserModule  ,
    HttpClientModule 
  ],
  providers: [
    AuthenticationService, UserService, PasswordService, DepartmentService, RoleService,
    { provide: HTTP_INTERCEPTORS, useClass: RequestInterceptor, multi: true }
    
    ],
  //bootstrap: []
})
export class CoreModule { }
