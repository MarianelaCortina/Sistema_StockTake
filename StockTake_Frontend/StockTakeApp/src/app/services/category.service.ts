import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Category } from '../interfaces/category';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  private urlAPI: string = environment.endpoint + "Categoria/";

  constructor(private https: HttpClient) { }

  getAllCategories(): Observable<Category[]> {
    return this.https.get<Category[]>(`${this.urlAPI}GetCategories`); 
  }

}
