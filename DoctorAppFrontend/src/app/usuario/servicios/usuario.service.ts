import { Injectable } from '@angular/core';
import { enviroment } from 'src/Enviroments/enviroment';
import { HttpClient } from '@angular/common/http';
import { Sesion } from '../interfaces/sesion';
import { Observable } from 'rxjs';
import { Login } from '../interfaces/login';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {
  baseUrl: string = enviroment.apiUrl + "usuario"; //ruta de mi app back

  constructor(private http: HttpClient) { } //inyeccion de dependencias para usar http

  IniciarSesion(request: Login): Observable<Sesion> {
    return this.http.post<Sesion>(`${this.baseUrl}/login`, request);
  }
}
