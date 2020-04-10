import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-attendance-detail',
  templateUrl: './attendance-detail.component.html',
  styleUrls: ['./attendance-detail.component.css']
})
export class AttendanceDetailComponent implements OnInit {
  employeeAttendanceTrack:any[];
  constructor(private router: Router,private route: ActivatedRoute,) { }

  ngOnInit() {
debugger;
    this.route.queryParams.subscribe(params => {
      this.employeeAttendanceTrack = params.employeeAttendanceTrack;
    });
  }
}
