import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SharedService } from 'src/app/shared/services/shared.service.';


@Injectable({ providedIn: 'root' })

export class AuthenticationService {

  apiBaseUrl:string;
  constructor(protected http: HttpClient ,private sharedService: SharedService) {
    this.apiBaseUrl = sharedService.ApiBaseUrl;
  }
  login(user:any){
    //return this.http.post(this.apiBaseUrl+"", JSON.stringify(user));
    return this.http.post(this.apiBaseUrl+"Account/signin" , user);
  }

  logout(user:any){
    //return this.http.post(this.apiBaseUrl+"", JSON.stringify(user));
   localStorage.clear();    
    return this.http.get(this.apiBaseUrl+"");
  }
  updateUser(user :any){
    //return this.http.post(this.apiBaseUrl+"", JSON.stringify(user));
    return this.http.post(this.apiBaseUrl+"Account/updateUser" , user);
  }

}