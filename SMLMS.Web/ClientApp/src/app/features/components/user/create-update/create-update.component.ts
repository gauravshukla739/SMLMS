import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/core/services/user.service';
import { SharedService } from 'src/app/shared/services/shared.service.';

@Component({
  selector: 'app-user-form',
  templateUrl: './create-update.component.html',
})
export class UserFormComponent implements OnInit {

  ngOnInit() {
  }

}
