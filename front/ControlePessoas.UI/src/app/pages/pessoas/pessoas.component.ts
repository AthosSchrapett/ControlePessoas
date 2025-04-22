import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatCardModule } from '@angular/material/card';
import { PessoaGetAllDTO } from '../../models/pessoa-get-all.dto';
import { PessoaService } from '../../services/pessoa.service';
import { PageHeaderComponent } from '../../components/shared/page-header/page-header.component';
import { DataTableComponent, DataTableColumn } from '../../components/shared/data-table/data-table.component';
import { ModalPessoaComponent } from './modal-pessoa/modal-pessoa.component';
import { FiltroPessoasEnum } from '../../enums/filtro-pessoas.enum';
import { FiltroPaginacao } from '../../models/filtro-paginacao.model';
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
  FiltroPessoasEnum = FiltroPessoasEnum;

  columns: DataTableColumn[] = [
    { name: 'nome', label: 'Nome', type: 'text' },
    { name: 'idade', label: 'Idade', type: 'custom' },
    { name: 'idoso', label: 'Idoso', type: 'custom' }
  ];

  dataSource: { data: PessoaGetAllDTO[] } = { data: [] };
  totalItems = 0;
  pageSize = 10;
  currentPage = 0;
  filtroIdoso: FiltroPessoasEnum = FiltroPessoasEnum.TODOS;

  private dialog: MatDialog = inject(MatDialog);
  private pessoaService: PessoaService = inject(PessoaService);
  private snackBar: MatSnackBar = inject(MatSnackBar);

  constructor() {}

  ngOnInit(): void {
    this.carregarDados();
  }

  carregarDados(): void {
    const filtro: FiltroPaginacao = {
      pagina: this.currentPage + 1,
      itensPorPagina: this.pageSize,
      filtroPessoas: this.filtroIdoso
    };

    this.pessoaService.getAll(filtro).subscribe({
      next: (resultado) => {
        this.dataSource.data = resultado.itens;
        this.totalItems = resultado.totalRegistros;
      },
      error: (e) => {
        this.snackBar.open(e.error.Message, 'Fechar', { duration: 3000 });
      },
      complete: () => {
      }
    });
  }

  onPageChange(event: any): void {
    this.currentPage = event.pageIndex;
    this.pageSize = event.pageSize;
    this.carregarDados();
  }

  aplicarFiltro(): void {
    this.currentPage = 0;
    this.carregarDados();
  }

  onNovaPessoa(): void {
    const dialogRef = this.dialog.open(ModalPessoaComponent, {
      width: '500px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.carregarDados();
      }
    });
  }

  onEdit(pessoa: PessoaGetAllDTO): void {
    this.pessoaService.getById(pessoa.id).subscribe({
      next: (pessoaDetalhada) => {
    const dialogRef = this.dialog.open(ModalPessoaComponent, {
      width: '500px',
          data: { pessoa: pessoaDetalhada }
    });

    dialogRef.afterClosed().subscribe(result => {
          if (result) {
            this.carregarDados();
          }
        });
      },
      error: () => {
        this.snackBar.open('Erro ao carregar dados da pessoa', 'Fechar', { duration: 3000 });
      }
    });
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
      if (result.success) {
      this.pessoaService.delete(pessoa.id).subscribe({
        next: () => {
            this.snackBar.open('Pessoa excluída com sucesso', 'Fechar', { duration: 3000 });
            this.carregarDados();
        },
        error: () => {
          this.snackBar.open('Erro ao excluir pessoa', 'Fechar', { duration: 3000 });
        }
      });
    }
    });
  }
}
