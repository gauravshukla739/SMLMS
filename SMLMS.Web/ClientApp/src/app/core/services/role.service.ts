import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SharedService } from 'src/app/shared/services/shared.service.';


@Injectable({ providedIn: 'root' })

export class RoleService {

  apiBaseUrl: string;
  constructor(protected http: HttpClient, private sharedService: SharedService) {
    this.apiBaseUrl = sharedService.ApiBaseUrl;
  }
  all() {
    return this.http.get(this.apiBaseUrl + "/Role");
  }
  post(data: any) {
    return this.http.post(this.apiBaseUrl + "/Role", data);
  }
  permission(data: any) {
    return this.http.post(this.apiBaseUrl + "/Role/Permission", data);
  }

  getpermission() {
    return this.http.get(this.apiBaseUrl + "/Role/Permission");
  }
}
