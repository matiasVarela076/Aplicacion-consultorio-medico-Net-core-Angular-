import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from './layout.component';
import { DashboardComponent } from '../dashboard/dashboard.component';
import { ListadoEspecialidadComponent } from 'src/app/especialidad/pages/listado-especialidad/listado-especialidad.component';

const routes: Routes = [
    {
        path: '',
        component: LayoutComponent,
        children: [
            { path: 'dashboard', component: DashboardComponent, pathMatch: 'full' },
            { path: 'especialidades', component: ListadoEspecialidadComponent, pathMatch: 'full' },
            { path: '', redirectTo: 'dashboard', pathMatch: 'full' },

        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class LayoutRoutingModule { }
