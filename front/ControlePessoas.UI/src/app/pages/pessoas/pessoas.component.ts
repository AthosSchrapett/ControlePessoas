import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatCardModule } from '@angular/material/card';
import { PageEvent } from '@angular/material/paginator';
import { PessoaGetAllDTO } from '../../models/pessoa-get-all.dto';
import { PessoaService } from '../../services/pessoa.service';
import { PageHeaderComponent } from '../../components/shared/page-header/page-header.component';
import { DataTableComponent, DataTableColumn } from '../../components/shared/data-table/data-table.component';
import { ConfirmModalComponent } from '../../components/shared/confirm-modal/confirm-modal.component';

@Component({
  selector: 'app-pessoas',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatCardModule,
    PageHeaderComponent,
    DataTableComponent
  ],
  templateUrl: './pessoas.component.html',
  styleUrls: ['./pessoas.component.scss']
})
export class PessoasComponent implements OnInit {

  columns: DataTableColumn[] = [
    { name: 'nome', label: 'Nome', type: 'text' },
    { name: 'idade', label: 'Idade', type: 'custom' },
    { name: 'idoso', label: 'Idoso', type: 'custom' }
  ];

  pessoas: PessoaGetAllDTO[] = [];
  dataSource: MatTableDataSource<PessoaGetAllDTO>;
  totalItems = 0;
  pageSize = 10;
  pageIndex = 0;
  filtroIdoso = 'todos';

  private pessoaService = inject(PessoaService);
  private dialog = inject(MatDialog);
  private snackBar = inject(MatSnackBar);

  constructor() {
    this.dataSource = new MatTableDataSource<PessoaGetAllDTO>([]);
  }

  ngOnInit(): void {
    this.loadPessoas();
  }

  loadPessoas(): void {
    this.pessoaService.getAll().subscribe({
      next: (data) => {
        this.pessoas = data;
        this.dataSource.data = data;
        this.totalItems = data.length;
        this.aplicarFiltro();
      },
      error: (error) => {
        this.snackBar.open('Erro ao carregar pessoas', 'Fechar', { duration: 3000 });
      }
    });
  }

  aplicarFiltro(): void {
    let dadosFiltrados: PessoaGetAllDTO[] = [];

    switch (this.filtroIdoso) {
      case 'idosos':
        dadosFiltrados = this.pessoas.filter(pessoa => pessoa.idoso);
        break;
      case 'nao-idosos':
        dadosFiltrados = this.pessoas.filter(pessoa => !pessoa.idoso);
        break;
      default:
        dadosFiltrados = this.pessoas;
        break;
    }

    this.dataSource.data = dadosFiltrados;
    this.totalItems = dadosFiltrados.length;
  }

  onPageChange(event: PageEvent): void {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.loadPessoas();
  }

  onNovaPessoa(): void {
  }

  onEdit(pessoa: PessoaGetAllDTO): void {
  }

  onDelete(pessoa: PessoaGetAllDTO): void {
    
    const dialogRef = this.dialog.open(ConfirmModalComponent, {
      width: '400px',
      data: {
        title: 'Confirmar Exclusão',
        message: `Tem certeza que deseja excluir ${pessoa.nome}?`,
        confirmText: 'Excluir',
        cancelText: 'Cancelar',
        confirmColor: 'warn'
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.pessoaService.delete(pessoa.id).subscribe({
          next: () => {
            this.snackBar.open('Pessoa excluída com sucesso', 'Fechar', { duration: 3000 });
            this.loadPessoas();
          },
          error: () => {
            this.snackBar.open('Erro ao excluir pessoa', 'Fechar', { duration: 3000 });
          }
        });
      }
    });
  }
}
