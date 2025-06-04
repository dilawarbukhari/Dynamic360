import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { GenericHttpClientService } from 'src/app/utilities/generic-http-client.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  _baseEndPoint: string = 'User'
  constructor(private _httpService:GenericHttpClientService) {

  }
  Signup(data:any):Observable<any>
  
  {
   return this._httpService.post<any>(`${this._baseEndPoint}/Register`, undefined, data)
  }
  login(data:any): Observable<any> {
    return this._httpService.post<any>(`${this._baseEndPoint}/Login`, undefined, data)
  }

   }

