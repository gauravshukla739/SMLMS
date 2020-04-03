import { Component, OnInit } from '@angular/core';
import { LeaveService } from '../../../../core/services/leave.service';
import { Observable } from 'rxjs/internal/Observable';
import { LeaveType } from '../../../../core/models/LeaveType';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';



@Component({
  selector: 'app-leave',
  templateUrl: './leave.component.html',
  styleUrls: ['./leave.component.css']
})
export class LeaveComponent implements OnInit {
  leavetyps: Observable<LeaveType[]>;
  registerForm: FormGroup;
  submitted = false;
  display = 'none';
  leaveid: any;
  alertMsg: string;

  constructor(private _lserv: LeaveService,
    private formBuilder: FormBuilder) {
    this.leaveid = "";
    this.getLeaveData();
  }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      leavename: ['', Validators.required],
      leavecount: ['', Validators.required]
    });
  }

  get f() { return this.registerForm.controls; }
  onSubmit() {
    this.submitted = true;
    if (this.registerForm.invalid) {
      return;
    }

    const ldata = {
      "Name": this.registerForm.controls.leavename.value,
      "Count": this.registerForm.controls.leavecount.value,
      "ID": this.leaveid
    }
    this._lserv.addLeaveTypes(ldata).subscribe((res: any) => {

      var response = res;
      if (response.isSuccess) {
        if (this.leaveid == "")
          this.alertMsg = "Record created";
        else
          this.alertMsg = "Record update";
        this.getLeaveData();
        this.closeModalDialog();
        this.leaveid = "";
        this.submitted = false;
      }

    }, () => {

    });

  }
  editLeaveType(item: any) {
    this.openModalDialog();
    this.registerForm.controls.leavename.setValue(item.name);
    this.registerForm.controls.leavecount.setValue(item.count);
    this.leaveid = item.id;
    
  }
  openModalDialog() {
    this.submitted = false;
    this.display = 'block'; //Set block css
  }

  closeModalDialog() {
    this.registerForm.reset();
    this.display = 'none'; //set none css after close dialog
  }
  // load all employes
  getLeaveData() {
    this._lserv.GetAllLeaveTypes().subscribe((res: any) => {

      var response = res;
      if (response.isSuccess) {
        this.leavetyps = res.data;
      }

    }, () => {

    });
  }
  deleteLeaveType(id: any) {
    var isDeleted = confirm("Are you sure to want this record?");
    if (isDeleted) {
      this._lserv.deleteLeaveTypes(id).subscribe((res: any) => {

        var response = res;
        if (response.isSuccess) {
          this.alertMsg = "Record deleted.";
          this.getLeaveData();
        }

      }, () => {

      });
    }
  }
}
