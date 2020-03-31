import { Component, OnInit,OnDestroy,Renderer2  } from '@angular/core';

@Component({
  selector: 'app-account-layout',
  templateUrl: './account-layout.component.html',
  styleUrls: ['./account-layout.component.css']
})
export class AccountLayoutComponent implements OnInit ,OnDestroy  {

  constructor(private renderer: Renderer2) {
    this.renderer.addClass(document.body, 'login_page');
   }

  ngOnInit() {
  }
  ngOnDestroy() {
    this.renderer.removeClass(document.body, 'login_page');
  }
}
