import { Routes } from '@angular/router';
import { PessoasComponent } from './pages/pessoas/pessoas.component';

export const routes: Routes = [
  { path: '', redirectTo: '/pessoas', pathMatch: 'full' },
  { path: 'pessoas', component: PessoasComponent },
];
