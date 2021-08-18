import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {AuthGuard} from './shared/auth.guard';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'comandas',
    pathMatch: 'full'
  },
  {
    path: 'comandas',
    canActivate: [AuthGuard],
    loadChildren: () => import('./pages/comanda/comanda.module').then(m => m.ComandaModule)
  },
  {
    path: 'usuarios',
    canActivate: [AuthGuard],
    loadChildren: () => import('./pages/usuario/usuario.module').then(m => m.UsuarioModule)
  },
  {
    path: 'produtos',
    canActivate: [AuthGuard],
    loadChildren: () => import('./pages/produto/produto.module').then(m => m.ProdutoModule)
  },
  {
    path: 'login',
    loadChildren: () => import('./pages/login/login.module').then(m => m.LoginModule)
  },
  {path: '**', redirectTo: ''}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
