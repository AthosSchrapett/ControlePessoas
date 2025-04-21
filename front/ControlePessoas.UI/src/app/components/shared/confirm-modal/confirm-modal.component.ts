import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

export interface ConfirmModalData {
  title: string;
  message: string;
  confirmText?: string;
  cancelText?: string;
  confirmColor?: 'primary' | 'accent' | 'warn';
}

@Component({
  selector: 'app-confirm-modal',
  standalone: true,
  imports: [
    CommonModule,
    MatDialogModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './confirm-modal.component.html',
  styleUrls: ['./confirm-modal.component.scss']
})
export class ConfirmModalComponent {
  constructor(
    public dialogRef: MatDialogRef<ConfirmModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ConfirmModalData
  ) {
    this.data = {
      ...data,
      confirmText: data.confirmText || 'Confirmar',
      cancelText: data.cancelText || 'Cancelar',
      confirmColor: data.confirmColor || 'warn'
    };
  }

  onConfirm(): void {
    this.dialogRef.close(true);
  }

  onCancel(): void {
    this.dialogRef.close(false);
  }
}
