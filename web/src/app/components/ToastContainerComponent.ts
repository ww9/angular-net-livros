import { Component } from '@angular/core';
import { ToastService } from '../services/toast.service';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';
import { ToastMessage } from '../interfaces/toastMessage';

@Component({
  selector: 'app-toast-container',
  template: `
    <div class="position-fixed top-0 end-0 p-3" style="z-index: 1200;">
      <div *ngFor="let toast of toasts$ | async" class="toast show text-bg-{{toast.type}}" role="alert">
        <div class="d-flex">
          <div class="toast-body">
            {{ toast.text }}
          </div>
          <button type="button" class="btn-close me-2 m-auto" aria-label="Close"
            (click)="onRemoveToast(toast.id)"></button>
        </div>
      </div>
    </div>
  `,
  standalone: true,
  imports: [CommonModule]
})
export class ToastContainerComponent {
  toasts$: Observable<ToastMessage[]>;

  constructor(private toastService: ToastService) {
    this.toasts$ = this.toastService.toasts$;
  }

  onRemoveToast(id: number) {
    this.toastService.removeToast(id);
  }
}
