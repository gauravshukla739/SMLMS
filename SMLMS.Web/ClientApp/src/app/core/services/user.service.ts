import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { SharedService } from 'src/app/shared/services/shared.service.';


@Injectable({ providedIn: 'root' })

export class UserService {
  apiBaseUrl = "";

  constructor(protected http: HttpClient ,private sharedService: SharedService) {
    this.apiBaseUrl = sharedService.ApiBaseUrl;
  }

  getAll() {
    return this.http.get(this.apiBaseUrl + `/User/getAll`);
  }

  delete(id) {
    return this.http.post(this.apiBaseUrl + `/User/delete?userId=${id}`, null);
  }

  getRoles(){
    return this.http.get(this.apiBaseUrl+`/Role/getAllRoles`);
  }

  getModules(){
    return this.http.get(this.apiBaseUrl+"/Role/getAllModule");
  }
  getPermissions(){
    return this.http.get(this.apiBaseUrl+"/Role/getAllRolePermissions");
  }

  import(user: any) {
    return this.http.post(this.apiBaseUrl + "/user/import", user);
  }

  promote(user: any) {
    return this.http.post(this.apiBaseUrl + "/user/promote", user);
  }


}
