import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs';


@Injectable({ providedIn: 'root' })
export class SharedService {
  ApiBaseUrl : string ="http://localhost:57608/";

  constructor(protected http: HttpClient) {
    this.accessToken = localStorage.getItem("user-token");
    this.user = localStorage.getItem("user") != null ? JSON.parse(localStorage.getItem("user")): {};
  }
 
  user ={Name:"" ,Photo_URL:"",Email:"",Id: ""}
  currentUserId: any;
  currentMember: any;
  currentProjectId: any;
  accessToken = "";
  getData() {
    let r  = Request;
    //return this.http.send(r);
  }
  setUser(user: any){
    this.user = user;
    localStorage.setItem("user" , JSON.stringify(user));
  }

  private _loading: boolean = false;
  loadingStatus: any = new Subject();

  get loading():boolean {
    return this._loading;
  }

  set loading(value) {
    this._loading = value;
    this.loadingStatus.next(value);
  }

  startLoading() {
    this.loading = true;
  }

  stopLoading() {
    this.loading = false;
  }

  private _showPopup: boolean = false;
  popupMsg : any;
  popupStatus: any = new Subject();

  get showpopup():boolean {
    return this._showPopup;
  }

  set showpopup(value) {
    this._showPopup = value;
    this.popupStatus.next(this.popupMsg);
  }

  showPopup( msg :any) {
    this.popupMsg = msg;
    this.showpopup = true;
  }

}
