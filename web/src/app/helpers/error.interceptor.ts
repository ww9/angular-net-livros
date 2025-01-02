import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpErrorResponse, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { EMPTY, Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ToastService } from '../services/toast.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  toast = inject(ToastService);

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error?.error?.friendlyError) {
          this.toast.showToast(error?.error?.message, 'warning');
          return EMPTY; // Não propagar erro pois é amigável para usuário e não deve ser tratado como exceção.
        } else {
          console.error('Houve um erro não esperado. Favor contatar o suporte.');
        }
        return throwError(() => error);
      })
    );
  }
}
