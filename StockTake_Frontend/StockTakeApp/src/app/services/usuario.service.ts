import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { CreateUser, UpdateUser, User } from '../interfaces/user';
import { Response } from '../interfaces/response';


@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  private urlAPI = environment.endpoint + 'Usuario/';

  constructor(private http: HttpClient) {}

  getUsers(): Observable<Response<User[]>> {
    return this.http.get<Response<User[]>>(`${this.urlAPI}Lista`);
  }

  createUser(data: CreateUser): Observable<Response<User>> {
    return this.http.post<Response<User>>(`${this.urlAPI}Crear`, data);
  }

  updateUser(data: UpdateUser): Observable<Response<User>> {
    return this.http.put<Response<User>>(`${this.urlAPI}Actualizar`, data);
  }

  deleteUser(id: number): Observable<Response> {
    return this.http.delete<Response>(`${this.urlAPI}Eliminar/${id}`);
  }
}
