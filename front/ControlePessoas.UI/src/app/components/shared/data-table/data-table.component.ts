import { Component, Input, Output, EventEmitter, ContentChildren, QueryList, TemplateRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

export interface DataTableColumn {
  name: string;
  label: string;
  type: 'text' | 'custom' | 'actions' | undefined;
}

@Component({
  selector: 'app-data-table',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './data-table.component.html',
  styleUrls: ['./data-table.component.scss']
})

export class DataTableComponent {
  @Input() columns: DataTableColumn[] = [];
  @Input() dataSource: any[] = [];
  @Input() totalItems = 0;
  @Input() pageSize = 10;
  @Input() pageSizeOptions = [5, 10, 25];
  @Input() showActions = true;

  @Output() pageChange = new EventEmitter<PageEvent>();
  @Output() edit = new EventEmitter<any>();
  @Output() delete = new EventEmitter<any>();

  @ContentChildren(TemplateRef) templates!: QueryList<TemplateRef<any>>;
  private templateMap = new Map<string, TemplateRef<any>>();

  ngAfterContentInit() {
    this.templates.forEach((template: TemplateRef<any>) => {
      const columnName = (template as any)._declarationTContainer.localNames?.[0];
      if (columnName) {
        this.templateMap.set(columnName, template);
      }
    });
  }

  getColumnTemplate(columnName: string): TemplateRef<any> | null {
    return this.templateMap.get(columnName) || null;
  }

  get displayedColumns(): string[] {
    const columns = this.columns.map(col => col.name);
    return this.showActions ? [...columns, 'actions'] : columns;
  }

  onPageChange(event: PageEvent): void {
    this.pageChange.emit(event);
  }

  onEdit(item: any): void {
    this.edit.emit(item);
  }

  onDelete(item: any): void {
    this.delete.emit(item);
  }
}
