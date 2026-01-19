import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UsuarioService } from '../servicios/usuario.service';
import { CompartidoService } from 'src/app/compartido/compartido.service';
import { Login } from '../interfaces/login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  formLogin: FormGroup; //Formulario de login
  ocultarPassword: boolean = true; //Ocultar password
  mostrarLoading: boolean = false; //Mostrar loading

  constructor(private fb: FormBuilder, //agrego el formulario
    private router: Router, //agrego el router
    private usuarioService: UsuarioService, //agrego el servicio
    private compartidoService: CompartidoService) {
    this.formLogin = this.fb.group({
      username: ['', [Validators.required]], //campos requeridos
      password: ['', [Validators.required]]
    })
  }

  iniciarSesion() {
    this.mostrarLoading = true; //cargando 
    const request: Login = {
      username: this.formLogin.value.username,
      password: this.formLogin.value.password
    }
    console.log('Enviando:', request); // Debug
    this.usuarioService.IniciarSesion(request).subscribe({
      next: (response) => {
        console.log('Respuesta de API en login:', response);
        // Agregar el username del formulario a la respuesta
        response.username = this.formLogin.value.username;
        this.compartidoService.guardarSesion(response);
        console.log('Sesión guardada:', this.compartidoService.obtenerSesion());
        this.router.navigate(['layout']); //redirige a la layout
      },
      complete: () => {
        this.mostrarLoading = false;
      },
      error: (error) => {
        console.log('Error completo:', error); // Debug completo
        const mensaje = error.error && typeof error.error === 'string' ? error.error : 'Usuario o contraseña incorrectas';
        this.compartidoService.mostrarAlerta(mensaje, 'error');
        this.mostrarLoading = false;
      }
    })
  }

}
