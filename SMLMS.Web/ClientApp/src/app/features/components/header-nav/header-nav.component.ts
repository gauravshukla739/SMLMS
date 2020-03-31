import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SharedService } from 'src/app/shared/services/shared.service.';

@Component({
  selector: 'app-header-nav',
  templateUrl: './header-nav.component.html',
  styleUrls: ['./header-nav.component.css']
})
export class HeaderNavComponent implements OnInit {

  constructor(private router: Router,private sharedService: SharedService) { }

  ngOnInit() {
  }

  logOut(){
    this.sharedService.accessToken="";
    this.router.navigate(['/secure']);
  }
}
