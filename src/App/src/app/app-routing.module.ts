import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AprovacaoNotasCompraComponent } from './aprovacao-notas-compra/aprovacao-notas-compra.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './auth.guard';


const routes: Routes = [
  { path: 'aprovacao-notas-compra', component: AprovacaoNotasCompraComponent, canActivate: [AuthGuard]},
  { path: 'login', component: LoginComponent},
  { path: '', pathMatch: 'full', redirectTo: 'login'}
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
