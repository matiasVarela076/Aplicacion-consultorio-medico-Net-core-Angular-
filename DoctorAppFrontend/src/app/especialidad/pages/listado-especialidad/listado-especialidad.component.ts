import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { Especialidad } from '../../interfaces/especialidad';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { EspecialidadService } from '../../servicios/especialidad.service';
import { CompartidoService } from 'src/app/compartido/compartido.service';

@Component({
  selector: 'app-listado-especialidad',
  templateUrl: './listado-especialidad.component.html',
  styleUrls: ['./listado-especialidad.component.css']
})
export class ListadoEspecialidadComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = [
    'nombreEspecialidad',
    'descripcion',
    'estado',
    'acciones'
  ];

  dataInit: Especialidad[] = [];

  dataSource = new MatTableDataSource(this.dataInit);

  @ViewChild(MatPaginator) paginacionTabla!: MatPaginator;

  constructor(private _especialidadService: EspecialidadService, private _compartidoService: CompartidoService) { }

  obtenerEspecialidades() {
    this._especialidadService.list().subscribe({
      next: (data) => {
        console.log('Datos recibidos:', data);
        if (data.isSuccess) {
          this.dataSource = new MatTableDataSource(data.result);
          this.dataSource.paginator = this.paginacionTabla;
        }
        else {
          this._compartidoService.mostrarAlerta(
            'No se encontrar datos',
            'Advertencia'
          );
        }
      },
      error: (e) => { }
    })
  }

  ngOnInit(): void {
    this.obtenerEspecialidades();
  }
  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginacionTabla;
  }
}

