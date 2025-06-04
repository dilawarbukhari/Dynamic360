import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GenericHttpClientService } from 'src/app/utilities/generic-http-client.service';

@Injectable({
  providedIn: 'root'
})
export class SupplierService {

  _baseEndPoint: string = 'Supplier'
     constructor(private _httpService:GenericHttpClientService) {
   
     }
     AddSupplier(data:any):Observable<any>
     {
      return this._httpService.post<any>(`${this._baseEndPoint}/AddSupplier`, undefined, data)
     }
     UpdateSupplier(data:any):Observable<any>
     {
      return this._httpService.put<any>(`${this._baseEndPoint}/UpdateSupplier`, data)
     }
     DeleteSupplier(id:number):Observable<any>
     {
      return this._httpService.delete<any>(`${this._baseEndPoint}/DeleteSupplier?id=${encodeURIComponent(id)}`)
     }
     GetAllSupplier():Observable<any>
     {
      return this._httpService.get<any>(`${this._baseEndPoint}/GetAllSupplier`)
     }
  
}
