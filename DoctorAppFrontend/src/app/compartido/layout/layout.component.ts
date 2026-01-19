import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CompartidoService } from '../compartido.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent implements OnInit {
  username: string = '';

  constructor(private router: Router, private compartidoService: CompartidoService) {
    const sesion = this.compartidoService.obtenerSesion();
    if (sesion) {
      this.username = sesion.username;
    }
  }

  ngOnInit(): void {
    const usuarioToken = this.compartidoService.obtenerSesion();
    console.log('Layout - Sesión obtenida:', usuarioToken);

    if (usuarioToken != null) {
      this.username = usuarioToken.username;
      console.log('Layout - Username asignado:', this.username);
    }
  }

  //usa el servicio compartido para eliminar la sesion y redirige al login
  cerrarSesion() {
    this.compartidoService.eliminarSesion();
    this.router.navigate(['login']);
  }
}
