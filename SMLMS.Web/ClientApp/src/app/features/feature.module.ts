import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { MainLayoutComponent } from './layout/main-layout/main-layout.component';
import { FeatureRoutingModule } from './feature-routing.module';
import { LeftMenuComponent } from './components/left-menu/left-menu.component';
import { HeaderNavComponent } from './components/header-nav/header-nav.component';
import { LoginComponent } from './components/account/login/login.component';
import { SettingComponent } from './components/setting/setting.component';
import { SharedModule } from '../shared/shared.module';
import { AccountLayoutComponent } from './layout/account-layout/account-layout.component';


@NgModule({
  declarations: [ 
  MainLayoutComponent,
  LeftMenuComponent,    
  HeaderNavComponent,  
  LoginComponent,   
  SettingComponent,
  AccountLayoutComponent],
  imports: [
    FeatureRoutingModule,
    //BrowserModule,
    FormsModule  ,
    SharedModule 
  ],
  providers: [],
  //bootstrap: []
})
export class FeatureModule { }
