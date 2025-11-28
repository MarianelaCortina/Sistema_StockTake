import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { AuthResponse, Sesion } from '../interfaces/auth-response';

@Injectable({ providedIn: 'root' })
export class AuthService {

  private urlAPI: string = environment.endpoint + "Auth/";

  constructor(private http: HttpClient) {}

  login(email: string, clave: string) {
    return this.http.post<AuthResponse>(`${this.urlAPI}Login`, {
      email,
      clave
    });
  }

  obtenerSesionUsuario(): Sesion | null {
    const dataCadena = localStorage.getItem('usuario');

    if (!dataCadena) return null;

    try {
      return JSON.parse(dataCadena);
    } catch (error) {
      console.error('Error parseando usuario', error);
      return null;
    }
  }

}
