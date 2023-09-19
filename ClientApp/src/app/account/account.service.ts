import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Register } from '../shared/entity/register';
import { environment } from 'src/environments/environment.development';
import { Login } from '../shared/entity/login';
import { User } from '../shared/entity/user';
import { ReplaySubject, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private http: HttpClient) { }

  private userSource = new ReplaySubject<User | null>(1); 
  user$= this.userSource.asObservable();

  register(module:Register){

    return this.http.post(`${environment.appUrl}api/account/register`, module);

  }

  login(modal:Login){
   
    return this.http.post<User>(`${environment.appUrl}api/account/login`, modal).pipe(
      map((user:User)=> {
        if(user){
          this.setUser(user);
          return user;
        }
        return null;
      })
    )
  }

  private setUser(user:User){
    localStorage.setItem(environment.userKey , JSON.stringify(user));
    this.userSource.next(user);
  }
}
