import { HttpErrorResponse } from "@angular/common/http";
import { MatSnackBar } from "@angular/material/snack-bar";

export function handleApiError(
    error: HttpErrorResponse,
    snackBar: MatSnackBar
): void {
    if (error.status === 400 && error.error?.errors) {
        const mensagens = Object.values(error.error.errors).flat() as string[];
        mensagens.forEach(msg => {
            snackBar.open(msg, 'Fechar', { duration: 3000 });
        });
    } else if (error.error?.message || error.error?.Message) {
        snackBar.open(error.error.message || error.error.Message, 'Fechar', { duration: 3000 });
    } else {
        snackBar.open('Erro inesperado ao processar a requisição.', 'Fechar', { duration: 3000 });
    }
}