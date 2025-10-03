import { Injectable } from '@angular/core';

import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Response } from '../interfaces/response';

@Injectable({
  providedIn: 'root'
})
export class DashBoardService {

  private urlAPI: string = environment.endpoint + "DashBoard/";

  constructor(private http: HttpClient) { }

  resumen():Observable<Response>{
    return this.http.get<Response>(`${this.urlAPI}Resumen`)
  }

}
