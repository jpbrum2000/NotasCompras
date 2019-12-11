import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AprovacaoNotasCompraComponent } from './aprovacao-notas-compra/aprovacao-notas-compra.component';


const routes: Routes = [
  { path: 'AprovacaoNotasCompra', component: AprovacaoNotasCompraComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
