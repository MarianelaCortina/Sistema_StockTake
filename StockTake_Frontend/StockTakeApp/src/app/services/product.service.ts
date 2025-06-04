//StockTake
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

  createProduct(request: Product):Observable<Product>{
    return this.https.post<Product>(`${this.urlAPI}CreateProduct`, request)
  }

  editProduct(id: number, product: Product): Observable<void> {
    return this.https.put<void>(`${this.urlAPI}EditProduct/${id}`, product);
  }

  deleteProduct(id: number): Observable<void> {
    return this.https.delete<void>(`${this.urlAPI}DeleteProduct/${id}`);
  }


}
