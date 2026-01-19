import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './usuario/login/login.component';
import { LayoutComponent } from './compartido/layout/layout.component';

const routes: Routes = [
  {path: '', 
   component: LoginComponent, 
   pathMatch: 'full'}, // cuanto este el path vacio que me muestre el login

  {path: 'login', 
  component: LoginComponent,
  pathMatch: 'full'}, // cuanto este el path login que me muestre el login  
  {
    path: 'layout', //layout/dashboard /layout/especialidades
    loadChildren: () => import('./compartido/compartido.module').then(m => m.CompartidoModule)
  },
  {
    path: '**',
    redirectTo: '',
    pathMatch: 'full' // cuanto no este el path que me muestre el login
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
