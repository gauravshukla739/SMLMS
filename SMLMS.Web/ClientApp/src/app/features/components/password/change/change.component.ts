import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared/services/shared.service.';
import { Router } from '@angular/router';
import { PasswordService } from 'src/app/core/services/password.service';
@Component({
  selector: 'app-change',
  templateUrl: './change.component.html',
})

export class ChangeComponent implements OnInit {

  constructor(private passwordService: PasswordService, private sharedService: SharedService, private router: Router) { }

  ngOnInit() {
  }

  pwd: any = {};

  onSubmit(formValid: any) {
    this.sharedService.startLoading();
    var response = this.passwordService.change(this.pwd).subscribe((data: any) => {
      console.log(data);
      if (data.IsSuccess) {
        this.sharedService.showPopup("");

        // this.router.navigate(['/permissions']);
      } else {
        this.sharedService.showPopup(data.Message);

      }
    });
    response.add(() => {
      this.sharedService.stopLoading();
    })
  }


}
