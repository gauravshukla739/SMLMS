import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared/services/shared.service.';

@Component({
  selector: 'app-left-menu',
  templateUrl: './left-menu.component.html',
  styleUrls: ['./left-menu.component.css']
})
export class LeftMenuComponent implements OnInit {

  isOpne = true;
  constructor(private sharedService :SharedService) { 
  }

  ngOnInit() {
  }

  showHideLeftNav() {
    this.isOpne = !this.isOpne;
  }

}
