import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {AuthGuard} from './shared/auth.guard';


const routes: Routes = [
  {
    path: '',
    redirectTo: 'comanda',
    pathMatch: 'full'
  },
  {
    path: 'comanda',
    canActivate: [AuthGuard],
    loadChildren: () => import('./pages/comanda/comanda.module').then(m => m.ComandaModule)
  },
  {
    path: 'usuario',
    canActivate: [AuthGuard],
    loadChildren: () => import('./pages/usuario/usuario.module').then(m => m.UsuarioModule)
  },
  {
    path: 'produto',
    canActivate: [AuthGuard],
    loadChildren: () => import('./pages/produto/produto.module').then(m => m.ProdutoModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
