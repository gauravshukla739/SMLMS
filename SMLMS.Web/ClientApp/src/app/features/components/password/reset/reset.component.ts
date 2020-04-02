import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared/services/shared.service.';
import { Router } from '@angular/router';
import { PasswordService } from 'src/app/core/services/password.service';

@Component({
  selector: 'app-reset',
  templateUrl: './reset.component.html',
  styleUrls: ['./reset.component.css']
})
export class ResetComponent implements OnInit {

  constructor(private passwordService: PasswordService, private sharedService: SharedService, private router: Router) { }

  ngOnInit() {
  }

  pwd: any = {};

  onSubmit(formValid: any) {
    this.sharedService.startLoading();
    var response = this.passwordService.reset(this.pwd).subscribe((data: any) => {
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
