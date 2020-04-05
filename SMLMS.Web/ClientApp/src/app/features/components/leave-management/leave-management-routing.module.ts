import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { LeaveComponent } from './leave/leave.component';



const routes: Routes = [
  {
    path: '', component: LeaveComponent
    //canActivate: [AuthGuard],
    //children: [
    //  { path: '', component: SettingComponent },
    //  { path: 'permissions', component: SettingComponent }
    //]
  }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LeaveManagementRoutingModule { }
