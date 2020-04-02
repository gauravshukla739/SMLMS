import { Component, OnInit, Renderer } from '@angular/core';
import { Router } from '@angular/router';
import { SharedService } from 'src/app/shared/services/shared.service.';

@Component({
  selector: 'app-header-nav',
  templateUrl: './header-nav.component.html',
  styleUrls: ['./header-nav.component.css']
})
export class HeaderNavComponent implements OnInit {

  constructor(private router: Router, private sharedService: SharedService, private renderer: Renderer) { }
  isOpne = true;
  ngOnInit() {
  }

  logOut(){
    this.sharedService.accessToken="";
    this.router.navigate(['/secure']);
  }

  showHideLeftNav() {
    const el = this.renderer.selectRootElement('.sidebar');
    debugger;
    if (this.isOpne) {
      this.renderer.setElementStyle(el, 'width', '50px');
    }
    else {
      this.renderer.setElementStyle(el, 'width', '300px');
    }
    this.isOpne = !this.isOpne;
 
  }
}
