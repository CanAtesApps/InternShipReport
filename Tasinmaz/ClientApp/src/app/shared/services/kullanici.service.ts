import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { KullaniciTable, pageInfo, UserAlldetails } from 'src/app/Model';

@Injectable({
  providedIn: 'root'
})
export class KullaniciService {

  baseUrl = 'http://localhost:5000/api/kullanici';
  constructor(private http: HttpClient)
  {

  }

  createUser(tmp:any){
    return this.http.post(this.baseUrl,tmp);
  }
  getAllUsers() : Observable<KullaniciTable[]>
  {
    return this.http.get<KullaniciTable[]>(this.baseUrl);
  }
  getSingleUser(id:number) : Observable<UserAlldetails>
  {
    return this.http.get<UserAlldetails>(this.baseUrl+"/"+id);
  }
  deleteUser(id:number)
  {
    return this.http.delete(this.baseUrl+"/"+id);
  }
  updateUser(tmp:UserAlldetails)
  {
    return this.http.put(this.baseUrl+"/"+tmp.userID,tmp);
  }
  getPagedUsers(page:number) : Observable<KullaniciTable[]>
  {
    return this.http.get<KullaniciTable[]>(this.baseUrl+"/pageNo="+page);
  }
  getMaxPageNumber() : Observable<pageInfo>
  {
    return this.http.get<pageInfo>(this.baseUrl+"/maxPageNumber");
  }
  getPagedUsersWithFilter(page:number, filter :string):Observable<KullaniciTable[]>
  {
    return this.http.get<KullaniciTable[]>(this.baseUrl+"/page="+page+"filter="+filter);
  }

  getMaxPageNumberFiltreli(search: string):Observable<pageInfo>
  {
    return this.http.get<pageInfo>(this.baseUrl+"/maxPageNumberFiltreli="+search);
  }
}
