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
        this.router.navigate(['/dashboard']);
        this.sharedService = new SharedService();
        this.sharedService.showPopup("Successfully login");
        localStorage.setItem("user-token", data.data.token);
        this.sharedService.setUser(data.data.user);
        this.sharedService.setPermission(data.data.permission);
        this.sharedService.accessToken = data.data.token;
        
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
