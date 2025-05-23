import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Product } from '../interfaces/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private urlAPI: string = environment.endpoint + "Producto/";

  constructor(private https: HttpClient) { }

  getAllProducts(): Observable<Product[]> {
    return this.https.get<Product[]>(`${this.urlAPI}GetProducts`); 
  }
  /* lista():Observable<ResponseApi>{
    return this.http.get<ResponseApi>(`${this.urlAPI}Lista`)
  } */

  /* guardar(request: Producto):Observable<ResponseApi>{
    return this.http.post<ResponseApi>(`${this.urlAPI}Guardar`, request)
  } */

 /*  editar(request: Producto):Observable<ResponseApi>{
    return this.http.put<ResponseApi>(`${this.urlAPI}Editar`, request)
  } */
  
  /* eliminar(id: number):Observable<ResponseApi>{
    return this.http.delete<ResponseApi>(`${this.urlAPI}Eliminar/${id}`)
  } */


}
