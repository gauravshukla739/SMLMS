import { Component, OnDestroy, OnInit } from '@angular/core';
import { SharedService } from './shared/services/shared.service.';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit, OnDestroy{
  showPupup: boolean = false;
  showLoader: boolean = false;
  title = 'SM Portal';
  popUpMsg: string;
  popupSubscription: Subscription;
  loaderSubscription: Subscription;

  constructor(private service: SharedService) {
  }

  ngOnInit() {
    this.popupSubscription = this.service.popupStatus.subscribe((value: any) => {
      this.popUpMsg = value;
      this.showPupup = true;
      setTimeout(() => {
        this.showPupup = false;
      }, 3500);
    });

    this.loaderSubscription = this.service.loadingStatus.subscribe((value: any) => {
      this.showLoader = value;
    });
  }
  dismiss() {
    this.showPupup = false;
  }
  ngOnDestroy() {
    this.popupSubscription.unsubscribe();
  }
}
