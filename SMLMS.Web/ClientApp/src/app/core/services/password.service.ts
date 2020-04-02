import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SharedService } from 'src/app/shared/services/shared.service.';


@Injectable({ providedIn: 'root' })

export class PasswordService {

  apiBaseUrl:string;
  constructor(protected http: HttpClient ,private sharedService: SharedService) {
    this.apiBaseUrl = sharedService.ApiBaseUrl;
  }
  forgot(emailId: string) {
    return this.http.post(this.apiBaseUrl + "/Password/Forgot", emailId);
  }

}
