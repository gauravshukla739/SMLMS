import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LeaveManagementRoutingModule } from './leave-management-routing.module';
import { LeaveComponent } from './leave/leave.component';
import { LeaveService } from '../../../core/services/leave.service';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [LeaveComponent],
  imports: [
    CommonModule,
    LeaveManagementRoutingModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [LeaveService]
})
export class LeaveManagementModule { }
