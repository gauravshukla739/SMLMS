import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { SharedService } from 'src/app/shared/services/shared.service.';


@Injectable({ providedIn: 'root' })

export class UserService {
  apiBaseUrl = "";

  constructor(protected http: HttpClient ,private sharedService: SharedService) {
    this.apiBaseUrl = sharedService.ApiBaseUrl;
  }
  getRoles(){
    return this.http.get(this.apiBaseUrl+`/api/Role/getAllRoles`);
  }

  getModules(){
    return this.http.get(this.apiBaseUrl+"/api/Role/getAllModule");
  }
  getPermissions(){
    return this.http.get(this.apiBaseUrl+"/api/Role/getAllRolePermissions");
  }


}