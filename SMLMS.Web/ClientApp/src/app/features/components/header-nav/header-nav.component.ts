import { Component, OnInit, Renderer } from '@angular/core';
import { Router } from '@angular/router';
import { SharedService } from 'src/app/shared/services/shared.service.';
import { AuthenticationService } from '../../../core/services/authentication.service';
import { UserService } from '../../../core/services/user.service';

@Component({
  selector: 'app-header-nav',
  templateUrl: './header-nav.component.html',
  styleUrls: ['./header-nav.component.css']
})
export class HeaderNavComponent implements OnInit {

  constructor(private authService: AuthenticationService, private userService: UserService, private router: Router, private sharedService: SharedService, private renderer: Renderer) { }
  isOpne = true;
  username: string;
  userId: any;
  image: any;
  roleName: any;
  ngOnInit() {
    debugger;
    this.image = (this.sharedService.user.image == "") ? "/assets/images/user.png" : this.sharedService.user.image;
    this.roleName = this.sharedService.user.roleName;
    this.userService.response.subscribe((event: any) => {
      debugger;
      var reader = new FileReader();
      reader.onload = function () {
        var output = document.getElementById('pImage') as any;
        output.src = reader.result;
      }
      reader.readAsDataURL(event.target.files[0]);
    });
   this.userId = this.sharedService.user.id;
    if (this.sharedService.user.firstName == undefined || this.sharedService.user.firstName == null) {
      this.username = this.sharedService.user.email;
    } else {
      this.username = this.sharedService.user.firstName + " " + this.sharedService.user.lastName||""
    }
    
  }

  logOut(){

    var response = this.authService.logout().subscribe((data: any) => {
      console.log(data);
      debugger;
      if (data.isSuccess) {
        localStorage.removeItem("user-token");
        localStorage.removeItem("user");
        this.sharedService.accessToken = "";
        this.router.navigate(['/secure']);
      } else {
        this.sharedService.showPopup(data.message);
        //this.sharedService.showPopup("Login failed , Invalid user");
      }
    });
    response.add(() => {
      this.sharedService.stopLoading();
    })
  
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
