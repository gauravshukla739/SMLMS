import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared/services/shared.service.';
import { Router } from '@angular/router';
import { PasswordService } from 'src/app/core/services/password.service';

@Component({
  selector: 'app-forgot',
  templateUrl: './forgot.component.html',
  styleUrls: ['./forgot.component.css']
})
export class ForgotComponent implements OnInit {

  constructor(private passwordService: PasswordService, private sharedService: SharedService, private router: Router) { }

  ngOnInit() {
  }

  email: string;

  onSubmit(formValid: any) {
    this.sharedService.startLoading();
    var response = this.passwordService.forgot(this.email).subscribe((data: any) => {
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
