import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SharedService } from 'src/app/shared/services/shared.service.';
import { finalize ,tap } from "rxjs/operators";
import { Router } from '@angular/router';


@Injectable()
export class RequestInterceptor implements HttpInterceptor {
    constructor(private sharedService: SharedService ,  private router: Router) {
    }
    activeRequests: number = 0;
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
       // add authorization header with jwt token if available
        // let currentUser = this.authenticationService.currentUserValue;
        // let currentUser :any={};
        if (this.activeRequests === 0) {
            this.sharedService.startLoading();
          }

        if (true) {
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${this.sharedService.accessToken}`
                }
            });
        }
        this.activeRequests++;
        return next.handle(request).pipe(
            tap(
                event => {
                  status = '';
                  if (event instanceof HttpResponse) {
                    status = 'succeeded';
                    //console.log(status);
                  }
                },
                error => {
                    status = 'failed'
                    if (error instanceof HttpErrorResponse) {
                        if (error.status === 401) {
                         console.log(error);
                         this.router.navigate(['/secure']);
                        }
                        else{
                            this.sharedService.showPopup("Unable to process your request ...");
                        }

                      }
                }
              ),
            finalize(() => {
              this.activeRequests--;
              if (this.activeRequests === 0) {
                this.sharedService.stopLoading();
              }
            },
            )
          );
    }
}