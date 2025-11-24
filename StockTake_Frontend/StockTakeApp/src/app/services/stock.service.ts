import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Stock, MovimientoStock, AlertaStock } from '../interfaces/stock';
import { Response } from '../interfaces/response';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class StockService {
  //private apiUrl = 'https://localhost:7211/api/stock'; // tu URL base del backend
  private urlAPI: string = environment.endpoint + "Stock/";

  constructor(private http: HttpClient) {}

  getProductos(): Observable<Response<Stock[]>> {
    return this.http.get<Response<Stock[]>>(`${this.urlAPI}productos`);
  }

  getMovimientos(): Observable<Response<MovimientoStock[]>> {
    return this.http.get<Response<MovimientoStock[]>>(`${this.urlAPI}movimientos`);
  }

  getAlertas(): Observable<Response<AlertaStock[]>> {
    return this.http.get<Response<AlertaStock[]>>(`${this.urlAPI}alertas`);
  }
}
