import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';

@Component({
  selector: 'app-page-header',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule
  ],
  templateUrl: './page-header.component.html',
  styleUrl: './page-header.component.scss'
})
export class PageHeaderComponent {
  @Input() title: string = '';
  @Input() subtitle: string = '';
  @Input() buttonLabel: string = '';
  @Input() buttonIcon: string = '';
  @Output() buttonClick = new EventEmitter<void>();

  onButtonClick(): void {
    this.buttonClick.emit();
  }
}
