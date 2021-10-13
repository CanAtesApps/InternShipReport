import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from "@auth0/angular-jwt";
import { stringify } from '@angular/core/src/util';

@Injectable({providedIn: 'root'})
export class ServiceNameService {
  constructor(private httpClient: HttpClient) { }

}
@Injectable({
  providedIn: 'root'
})
export class AuthService
{
  authurl      = "http://localhost:5000/api/auth";
  helper       = new JwtHelperService();
  decodedToken : any;
  tokenInfo    = {token: ""};
  headerString : any;
  isAdmin      : boolean;

  constructor(private http: HttpClient) { }

  login(model: any)
  {
    return this.http.post(this.authurl + '/login',model).pipe(
      map((response : any) => {
        const user        = response;
        this.decodedToken = this.helper.decodeToken(user.token);
        this.headerString = (user.result.rol) ? "Yönetici Paneli" : "Kullanıcı Paneli";
        this.isAdmin      = user.result.rol;
        localStorage.setItem('token', user.token);
        localStorage.setItem('rol',user.result.rol);
      })
    );
  }

  loggedIn()
  {
    const token = localStorage.getItem('token');
    return !this.helper.isTokenExpired(token);
  }

  logout()
  {
    this.tokenInfo.token =localStorage.getItem('token');
    var loginInfo = {username : this.decodedToken.unique_name , password :"" };
    return this.http.post(this.authurl + '/logout',loginInfo);
  }
}
