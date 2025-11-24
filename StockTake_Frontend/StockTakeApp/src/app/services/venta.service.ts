import { Injectable } from '@angular/core';

import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Response } from '../interfaces/response';
import { Venta } from '../interfaces/venta';

@Injectable({
  providedIn: 'root'
})

export class VentaService {

  private urlAPI: string = environment.endpoint + "Venta/";

  constructor(private http: HttpClient) { }

  registrar(request: Venta):Observable<Response>{
    return this.http.post<Response>(`${this.urlAPI}Registrar`, request)
  }

  historial(buscarPor:string, numeroVenta:string, fechaInicio:string, fechaFin:string):Observable<Response>{
    return this.http.get<Response>(`${this.urlAPI}historial?buscarPor=${buscarPor}&numeroVenta=${numeroVenta}&fechaInicio=${fechaInicio}&fechaFin=${fechaFin}`)
  }
  reporte(fechaInicio:string, fechaFin:string):Observable<Response>{
    return this.http.get<Response>(`${this.urlAPI}Reporte?fechaInicio=${fechaInicio}&fechaFin=${fechaFin}`)
  }


}