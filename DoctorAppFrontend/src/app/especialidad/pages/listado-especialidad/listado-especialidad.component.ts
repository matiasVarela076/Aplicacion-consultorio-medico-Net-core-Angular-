import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { Especialidad } from '../../interfaces/especialidad';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { EspecialidadService } from '../../servicios/especialidad.service';
import { CompartidoService } from 'src/app/compartido/compartido.service';
import { MatDialog } from '@angular/material/dialog';
import { ModalEspecialidadComponent } from '../../modals/modal-especialidad/modal-especialidad.component';
import Swal from 'sweetalert2';

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

  constructor(private _especialidadService: EspecialidadService, private _compartidoService: CompartidoService, private dialog: MatDialog) { }

  nuevoEspecialidad() {
    this.dialog.open(ModalEspecialidadComponent, {
      width: '400px',
      disableClose: true
    }).afterClosed().subscribe((resultado) => {
      if (resultado == 'true') {
        this.obtenerEspecialidades();
      }
    });
  }

  editarEspecialidad(especialidad: Especialidad) {
    this.dialog.open(ModalEspecialidadComponent, {
      width: '400px',
      data: especialidad,
      disableClose: true
    }).afterClosed().subscribe((resultado) => {
      if (resultado == 'true') {
        this.obtenerEspecialidades();
      }
    });

  }

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

  removerEspecialidad(especialidad: Especialidad) {
    Swal.fire({
      title: 'Desea eliminar la Especialidad?',
      text: especialidad.nombreEspecialidad,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      confirmButtonText: 'Si, eliminar',
      cancelButtonText: 'No'
    }).then((resultado) => {
      if (resultado.isConfirmed) {
        this._especialidadService.delete(especialidad.id).subscribe({
          next: (data) => {
            if (data.isSuccess) {
              this._compartidoService.mostrarAlerta('La especialidad ha sido eliminada con exito!', 'Completo');
              this.obtenerEspecialidades();
            }
            else {
              this._compartidoService.mostrarAlerta('No se pudo eliminar la especialidad', 'Error');
            }
          },
          error: (e) => {
            console.log(e);
          }
        })
      }
    });
  }

  aplicarFiltroList(event: Event) {
    const filtroValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filtroValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  ngOnInit(): void {
    this.obtenerEspecialidades();
  }
  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginacionTabla;
  }
}

