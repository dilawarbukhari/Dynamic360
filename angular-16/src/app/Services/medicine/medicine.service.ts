import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GenericHttpClientService } from 'src/app/utilities/generic-http-client.service';

@Injectable({
  providedIn: 'root'
})
export class MedicineService {

   
     _baseEndPoint: string = 'Medicine'
     constructor(private _httpService:GenericHttpClientService) {
   
     }
     AddMedicine(data:any):Observable<any>
     {
      return this._httpService.post<any>(`${this._baseEndPoint}/AddMedicine`, undefined, data)
     }
     UpdateMedicine(data:any):Observable<any>
     {
      return this._httpService.put<any>(`${this._baseEndPoint}/UpdateMedicine`, data)
     }
     DeleteMedicine(id:number):Observable<any>
     {
      return this._httpService.delete<any>(`${this._baseEndPoint}/DeleteMedicine?id=${encodeURIComponent(id)}`)
     }
     GetAllMedicine(data:any):Observable<any>
     {
      return this._httpService.post<any>(`${this._baseEndPoint}/GetMedicineDetail`, undefined, data)
     }
  
  }
  

