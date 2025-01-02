import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { ToastMessage } from '../interfaces/toastMessage';

@Injectable({
  providedIn: 'root'
})
export class ToastService {
  static instance: ToastService;

  private toasts: ToastMessage[] = [];
  private toastsSubject = new BehaviorSubject<ToastMessage[]>([]);
  public toasts$ = this.toastsSubject.asObservable();

  constructor() {
    ToastService.instance = this;
  }

  showToast(text: string, type: 'success' | 'danger' | 'info' | 'warning' = 'info') {
    const toast: ToastMessage = {
      id: new Date().getTime(),
      text,
      type
    };
    this.toasts.push(toast);
    this.toastsSubject.next(this.toasts);
    setTimeout(() => this.removeToast(toast.id), 10000);
  }

  removeToast(id: number) {
    this.toasts = this.toasts.filter(t => t.id !== id);
    this.toastsSubject.next(this.toasts);
  }
}
