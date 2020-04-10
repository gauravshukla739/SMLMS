import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared/services/shared.service.';
import { Router } from '@angular/router';
import { PasswordService } from 'src/app/core/services/password.service';
@Component({
  selector: 'app-change',
  templateUrl: './change.component.html'
})

export class ChangePasswordComponent implements OnInit {

  constructor(private passwordService: PasswordService, private sharedService: SharedService, private router: Router) { }

  ngOnInit() {
  }

  pwd: any = {};
  invalidpwd: boolean = false;
  onSubmit(formValid: any) {
    this.sharedService.startLoading();
    if (this.pwd.NewPassword == this.pwd.ConfirmPassword) {
      this.invalidpwd = false;
      var response = this.passwordService.change(this.pwd).subscribe((data: any) => {
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
      this.invalidpwd = true;
    }
  }


}
