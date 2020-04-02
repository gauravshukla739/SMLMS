import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { SharedService } from 'src/app/shared/services/shared.service.';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private authService: AuthenticationService, private sharedService: SharedService, private router: Router) { }

  ngOnInit() {
  }

  user: any = {};

  onSubmit(formValid: any) {
    this.sharedService.startLoading();
    var response = this.authService.login(this.user).subscribe((data: any) => {
      console.log(data);
      if (data.IsSuccess) {
        this.sharedService.showPopup("Successfully login");
        localStorage.setItem("user-token" ,  data.Data.Token);
        this.sharedService.accessToken = data.Data.Token;
        this.sharedService.setUser(data.Data.User);
        this.router.navigate(['/permissions']);
      } else {
        this.sharedService.showPopup(data.Message);
        //this.sharedService.showPopup("Login failed , Invalid user");
      }
    });
    response.add(() => {
       this.sharedService.stopLoading(); 
      })
  }




}
