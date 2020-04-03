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
      debugger;
      if (data.isSuccess) {
        this.sharedService.showPopup("Successfully login");
        localStorage.setItem("user-token" , data.data.token);
        this.sharedService.accessToken = data.data.token;
        this.sharedService.setUser(data.data.user);
        this.router.navigate(['/password/change']);
      } else {
        this.sharedService.showPopup(data.message);
        //this.sharedService.showPopup("Login failed , Invalid user");
      }
    });
    response.add(() => {
       this.sharedService.stopLoading(); 
      })
  }




}
