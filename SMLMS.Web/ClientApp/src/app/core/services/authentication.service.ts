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
    return this.http.post(this.apiBaseUrl +"/Account/Login" , user);
  }

  register(user: any) {
    return this.http.post(this.apiBaseUrl + "/Account/Register", user);
  }

  update(user: any) {
    return this.http.post(this.apiBaseUrl + "/Account/Update", user);
  }

  detail(id: any) {
    return this.http.get(this.apiBaseUrl + "/Account/Detail?id="+ id);
  }

  logout(){
   localStorage.clear();    
    return this.http.get(this.apiBaseUrl+"/Account/Logout");
  }
  updateUser(user :any){
    //return this.http.post(this.apiBaseUrl+"", JSON.stringify(user));
    return this.http.post(this.apiBaseUrl+"Account/updateUser" , user);
  }

}
