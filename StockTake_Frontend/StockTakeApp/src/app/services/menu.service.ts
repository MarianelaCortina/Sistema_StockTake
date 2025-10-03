// menu.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Response } from '../interfaces/response';

@Injectable({
  providedIn: 'root'
})
export class MenuService {

  private urlAPI: string = environment.endpoint + "Menu/";

  constructor(private http: HttpClient) { }

  /*  lista(idUsuario: number): Observable<Response> {
    return this.http.get<Response>(`${this.urlAPI}idUsuario=${idUsuario}`);
  }  */
  lista(idUsuario: number): Observable<Response> {
    return this.http.get<Response>(`${this.urlAPI}${idUsuario}`);
  } 
}
