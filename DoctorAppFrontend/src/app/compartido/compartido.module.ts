import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { LayoutComponent } from './layout/layout.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { MaterialModule } from '../material/material.module';
import { LayoutRoutingModule } from './layout/layout-routing.module';



@NgModule({
  declarations: [
    LayoutComponent,
    DashboardComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    LayoutRoutingModule
  ],
  exports: [ //aca estan las dependencias que se estan compartidas entre usuario y otros componentes
    ReactiveFormsModule,
    FormsModule,
    CommonModule,
    HttpClientModule,
    LayoutComponent,
    DashboardComponent
  ]
})
export class CompartidoModule { }
