import { Component, inject, Inject, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Subscription } from 'rxjs';

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
export class ConfirmModalComponent implements OnInit, OnDestroy {
  private subscriptions: Subscription[] = [];
  private dialogRef = inject(MatDialogRef<ConfirmModalComponent>);

  constructor(@Inject(MAT_DIALOG_DATA) public data: ConfirmModalData) {
    this.data = {
      ...data,
      confirmText: data.confirmText || 'Confirmar',
      cancelText: data.cancelText || 'Cancelar',
      confirmColor: data.confirmColor || 'warn'
    };
  }

  ngOnInit(): void {
    this.subscriptions.push(
      this.dialogRef.keydownEvents().subscribe(event => {
        if (event.key === 'Escape') {
          this.onCancel();
        }
      }),

      this.dialogRef.backdropClick().subscribe(() => {
        this.onCancel();
      })
    );
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(sub => sub.unsubscribe());
    
    if (document.activeElement instanceof HTMLElement) {
      document.activeElement.blur();
    }
  }

  onConfirm(): void {
    this.dialogRef.close({ success: true });
  }

  onCancel(): void {
    this.dialogRef.close({ success: false });
  }
}
