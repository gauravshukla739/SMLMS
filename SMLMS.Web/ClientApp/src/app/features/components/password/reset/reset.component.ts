import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared/services/shared.service.';
import { Router, ActivatedRoute } from '@angular/router';
import { PasswordService } from 'src/app/core/services/password.service';

@Component({
  selector: 'app-reset',
  templateUrl: './reset.component.html',
  styleUrls: ['./reset.component.css']
})
export class ResetPasswordComponent implements OnInit {

  constructor(private passwordService: PasswordService, private sharedService: SharedService, private route: ActivatedRoute) { }
  queryFields: any;
  ngOnInit() {
    debugger;
    this.route.queryParamMap
      .subscribe(params => {
        this.queryFields = { ...params } ;
        this.pwd.emailId = this.queryFields.params.email;
        this.pwd.token = this.queryFields.params.token;
      });
  }

  pwd: any = {};

  onSubmit(formValid: any) {
    debugger;
    this.sharedService.startLoading();
    if (this.pwd.Password == this.pwd.ConfirmPassword) {
      var response = this.passwordService.reset(this.pwd).subscribe((data: any) => {
        console.log(data);
        if (data.isSuccess) {
          this.sharedService.showPopup(data.message);

        } else {
          this.sharedService.showPopup(data.message);

        }
      });
      response.add(() => {
        this.sharedService.stopLoading();
      })
    } else {
      this.sharedService.showPopup("Password & Confirm Password should be same");
    }
  }


}
