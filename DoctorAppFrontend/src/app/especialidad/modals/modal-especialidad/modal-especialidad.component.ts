import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Especialidad } from '../../interfaces/especialidad';
import { EspecialidadService } from '../../servicios/especialidad.service';
import { CompartidoService } from 'src/app/compartido/compartido.service';

@Component({
  selector: 'app-modal-especialidad',
  templateUrl: './modal-especialidad.component.html',
  styleUrls: ['./modal-especialidad.component.css']
})
export class ModalEspecialidadComponent implements OnInit {

  formEspecialidad: FormGroup;
  titulo: string = "Agregar";
  nombreBoton: string = "Guardar";

  constructor(private modal: MatDialogRef<ModalEspecialidadComponent>,
    @Inject(MAT_DIALOG_DATA) public datosEspecialidad: Especialidad,
    private fb: FormBuilder,
    private _especialidadService: EspecialidadService,
    private _compartidoService: CompartidoService) {
    this.formEspecialidad = this.fb.group({
      nombreEspecialidad: ['', Validators.required],
      nombreDescripciopn: ['', Validators.required],
      estado: ['1', Validators.required]
    })

  }

  ngOnInit(): void {
    if (this.datosEspecialidad != null) {
      this.formEspecialidad.patchValue({
        nombreEspecialidad: this.datosEspecialidad.nombreEspecialidad,
        nombreDescripciopn: this.datosEspecialidad.descripcion,
        estado: this.datosEspecialidad.estado.toString()
      })
    }
  }

  createAndModifyEspecialidad() {
    const especialidad: Especialidad = {
      id: this.datosEspecialidad.id == null ? 0 : this.datosEspecialidad.id,
      nombreEspecialidad: this.formEspecialidad.get('nombreEspecialidad')?.value,
      descripcion: this.formEspecialidad.get('nombreDescripciopn')?.value,
      estado: parseInt(this.formEspecialidad.value.estado)
    }
    if (this.datosEspecialidad == null)
    //crea una nueva especialidad
    {
      this._especialidadService.create(especialidad).subscribe({
        next: (data) => {
          if (data.isSuccess) {
            this._compartidoService.mostrarAlerta('La especialidad ha sido grabada con exito!', 'Completo')

            this.modal.close('true');
          }
          else {
            this._compartidoService.mostrarAlerta('No se pudo crear la especialidad', 'Error!');
          }
        },
        error: (e) => { }
      });
    }
    else {
      //edita o actualiza una nueva especialidad
      this._especialidadService.edit(especialidad).subscribe({
        next: (data) => {
          if (data.isSuccess) {
            this._compartidoService.mostrarAlerta('La especialidad ha sido actualizada con exito!', 'Completo')

            this.modal.close('true');
          }
          else {
            this._compartidoService.mostrarAlerta('No se pudo actualizar la especialidad', 'Error!');
          }
        },
        error: (e) => { }
      });
    }
  }


}

