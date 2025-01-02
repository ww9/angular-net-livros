export interface ToastMessage {
  id: number;
  text: string;
  type?: 'success' | 'danger' | 'info' | 'warning';
}
