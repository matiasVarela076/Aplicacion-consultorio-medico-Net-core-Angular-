import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Sesion } from '../usuario/interfaces/sesion';

@Injectable({
  providedIn: 'root'
})
export class CompartidoService {

  constructor(private _snackBar: MatSnackBar) { }

  mostrarAlerta(mensaje: string, tipo: string) {
    this._snackBar.open(mensaje, tipo, {
      horizontalPosition: 'end',
      verticalPosition: 'top',
      duration: 3000
    });
  }

  guardarSesion(sesion: Sesion) {
    localStorage.setItem('usuarioSesion', JSON.stringify(sesion)); //guarda la sesion
  }

  obtenerSesion() {
    const sesionString = localStorage.getItem('usuarioSesion'); //obtiene la sesion

    if (!sesionString) {
      return null; //retorna null si no hay sesion
    }

    const usuarioToken = JSON.parse(sesionString); //parsea la sesion
    return usuarioToken;
  }

  eliminarSesion() //elimina la sesion
  {
    localStorage.removeItem('usuarioSesion');
  }
}
