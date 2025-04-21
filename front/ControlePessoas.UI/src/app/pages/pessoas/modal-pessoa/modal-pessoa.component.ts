import { Component, inject, Inject, OnInit, OnDestroy, AfterViewInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { MatSnackBar } from '@angular/material/snack-bar';
import { PessoaCreateDTO } from '../../../models/pessoa-create.dto';
import { PessoaUpdateDTO } from '../../../models/pessoa-update.dto';
import { PessoaGetDTO } from '../../../models/pessoa-get.dto';
import { PessoaService } from '../../../services/pessoa.service';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-modal-pessoa',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatIconModule
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  templateUrl: './modal-pessoa.component.html',
  styleUrls: ['./modal-pessoa.component.scss']
})
export class ModalPessoaComponent implements OnInit, OnDestroy, AfterViewInit {
  pessoaForm: FormGroup;
  isEdit = false;
  pessoaId: string | null = null;
  private subscriptions: Subscription[] = [];

  private dialogRef = inject(MatDialogRef<ModalPessoaComponent>);
  private formBuilder = inject(FormBuilder);
  private pessoaService = inject(PessoaService);
  private snackBar = inject(MatSnackBar);

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: { pessoa?: PessoaGetDTO },
  ) {
    this.pessoaForm = this.formBuilder.group({
      id: [null],
      nome: ['', [Validators.required, Validators.minLength(3)]],
      idade: [null, [Validators.required, Validators.min(1)]],
      sexo: ['', [Validators.required]],
      peso: [null, [Validators.required, Validators.min(1)]],
      altura: [null]
    });

    if (data?.pessoa) {
      this.isEdit = true;
      this.pessoaId = data.pessoa.id;
      this.pessoaForm.patchValue(data.pessoa);
    }

    this.dialogRef.disableClose = true;
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

  ngAfterViewInit(): void {
    setTimeout(() => {
      const firstInput = document.querySelector('input[formControlName="nome"]');
      if (firstInput instanceof HTMLElement) {
        firstInput.focus();
      }
    });
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(sub => sub.unsubscribe());

    if (document.activeElement instanceof HTMLElement) {
      document.activeElement.blur();
    }
  }

  onSubmit(): void {
    if (this.pessoaForm.valid) {
      if (this.isEdit && this.pessoaId) {
        console.log('editarPessoa');
        this.editarPessoa();
      } else {
        this.criarPessoa();
      }
    }
  }

  editarPessoa(): void {
    const pessoaUpdate: PessoaUpdateDTO = {
      id: this.pessoaId,
      ...this.pessoaForm.value
    };

    this.pessoaService.update(pessoaUpdate).subscribe({
      next: () => {
        this.snackBar.open('Pessoa atualizada com sucesso!', 'Fechar', { duration: 3000 });
        this.dialogRef.close({ success: true, data: pessoaUpdate });
      },
      error: () => {
        this.snackBar.open('Erro ao atualizar pessoa', 'Fechar', { duration: 3000 });
      }
    });
  }

  criarPessoa(): void {
    const pessoaCreate: PessoaCreateDTO = this.pessoaForm.value;
    this.pessoaService.create(pessoaCreate).subscribe({
      next: () => {
        this.snackBar.open('Pessoa criada com sucesso!', 'Fechar', { duration: 3000 });
        this.dialogRef.close({ success: true, data: pessoaCreate });
      },
      error: () => {
        this.snackBar.open('Erro ao criar pessoa', 'Fechar', { duration: 3000 });
      }
    });
  }

  onCancel(): void {
    this.dialogRef.close({ success: false });
  }
}
