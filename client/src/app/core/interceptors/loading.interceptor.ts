import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, delay, finalize } from 'rxjs';
import { BusyService } from '../services/busy.service';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {

  constructor(private busyService: BusyService) {}

  /* Fake delay for development */
  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    /* Start spinning loading thingi before making request and hide it when request is done */
    if (!request.url.includes('emailExists')) {
      this.busyService.busy();
    }
    return next.handle(request).pipe(
      delay(1),
      finalize(() => this.busyService.idle())
    )
  }
}
