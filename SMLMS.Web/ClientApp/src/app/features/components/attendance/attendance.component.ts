import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../../core/services/authentication.service';
import { SharedService } from '../../../shared/services/shared.service.';
import { Router } from '@angular/router';

@Component({
  selector: 'app-attendance',
  templateUrl: './attendance.component.html',
  styleUrls: ['./attendance.component.css']
})
export class AttendanceComponent implements OnInit {

  constructor(private authService: AuthenticationService, private sharedService: SharedService, private router: Router) { }

  ngOnInit() {
  }


  SignIn() {
    
  }
}
