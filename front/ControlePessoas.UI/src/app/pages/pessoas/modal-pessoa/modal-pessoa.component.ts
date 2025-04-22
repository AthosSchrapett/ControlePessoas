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
    @Inject(MAT_DIALOG_DATA) public data: { pessoa?: PessoaGetDTO }
  ) {
    this.pessoaForm = this.formBuilder.group({
      id: [null],
      nome: ['', [Validators.required, Validators.maxLength(60)]],
      idade: ['', [Validators.required, Validators.min(1), Validators.max(130)]],
      sexo: ['', [Validators.required, Validators.pattern(/^[MF]$/)]],
      peso: ['', [Validators.required, Validators.min(0.1)]],
      altura: ['', [Validators.required, Validators.min(0.3), Validators.max(2.3)]]
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

  getErrorMessage(field: string): string {
    const control = this.pessoaForm.get(field);
    if (!control?.errors) return '';
    const errors = control.errors;

    switch (field) {
      case 'nome':
        if (errors['required']) return 'O nome é obrigatório';
        else if (errors['maxlength']) return 'O nome deve ter no máximo 60 caracteres';
        break;
      case 'idade':
        if (errors['required']) return 'A idade é obrigatória';
        else if (errors['min']) return 'A idade deve ser maior que zero';
        else if (errors['max']) return 'A idade deve ser no máximo 130 anos';
        break;
      case 'sexo':
        if (errors['required']) return 'O sexo é obrigatório';
        else if (errors['pattern']) return "Sexo deve ser 'M' ou 'F'";
        break;
      case 'peso':
        if (errors['required']) return 'O peso é obrigatório';
        else if (errors['min']) return 'O peso deve ser maior que zero';
        break;
      case 'altura':
        if (errors['required']) return 'A altura é obrigatória';
        else if (errors['min']) return 'A altura deve ser maior que 0.30 metros';
        else if (errors['max']) return 'A altura deve ser no máximo 2.30 metros';
        break;
    }

    return '';
  }

  onSubmit(): void {
    if (this.pessoaForm.valid) {
      if (this.isEdit && this.pessoaId) {
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
      error: (e) => {
        this.snackBar.open(e.error.Message, 'Fechar', { duration: 3000 });
      },
      complete: () => {
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
      error: (e) => {
        this.snackBar.open(e.error.Message, 'Fechar', { duration: 3000 });
      },
      complete: () => {
      }
    });
  }

  onCancel(): void {
    this.dialogRef.close({ success: false });
  }
}
