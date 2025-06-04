import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GenericHttpClientService } from 'src/app/utilities/generic-http-client.service';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {


 
   _baseEndPoint: string = 'Category'
   constructor(private _httpService:GenericHttpClientService) {
 
   }
   AddCategory(data:any):Observable<any>
   {
    return this._httpService.post<any>(`${this._baseEndPoint}/AddCategory`, undefined, data)
   }
   UpdateCategory(data:any):Observable<any>
   {
    return this._httpService.put<any>(`${this._baseEndPoint}/UpdateCategory`, data)
   }
   DeleteCategory(id:number):Observable<any>
   {
    return this._httpService.delete<any>(`${this._baseEndPoint}/DeleteCategory?id=${encodeURIComponent(id)}`)
   }
   GetAllCategory():Observable<any>
   {
    return this._httpService.get<any>(`${this._baseEndPoint}/GetAllCategory`)
   }

}
