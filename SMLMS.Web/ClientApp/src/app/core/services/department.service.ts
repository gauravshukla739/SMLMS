import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SharedService } from 'src/app/shared/services/shared.service.';


@Injectable({ providedIn: 'root' })

export class DepartmentService {

  apiBaseUrl:string;
  constructor(protected http: HttpClient ,private sharedService: SharedService) {
    this.apiBaseUrl = sharedService.ApiBaseUrl;
  }
  all() {
    return this.http.get(this.apiBaseUrl + "/Department");
  }

  post(data) {
    return this.http.post(this.apiBaseUrl + "/Department/CreateOrUpdate" ,data);
  }

  delete(id) {
    return this.http.post(this.apiBaseUrl + "/Department/delete/" + id, null);
  }
  

}
