import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Register } from '../shared/entity/register';
import { environment } from 'src/environments/environment.development';
import { Login } from '../shared/entity/login';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private http: HttpClient) { }


  register(module:Register){

    return this.http.post(`${environment.appUrl}api/account/register`, module);

  }

  login(modal:Login){
   
    return this.http.post(`${environment.appUrl}api/account/login`, modal)
  }
}
