import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Log, pageInfo } from 'src/app/Model';

@Injectable({
  providedIn: 'root'
})
export class LogService {

  apiPagedUrl = "http://localhost:5000/api/log/";

  constructor(private http: HttpClient) { }

  getPagedLogs(page:number):Observable<Log[]>
  {
    return this.http.get<Log[]>(this.apiPagedUrl+"pageNo="+page);
  }

  getPagedLogsWithFilter(page:number, filter :string):Observable<Log[]>
  {
    return this.http.get<Log[]>(this.apiPagedUrl+"page="+page+"filter="+filter);
  }

  getMaxPageNumber():Observable<pageInfo>
  {
    return this.http.get<pageInfo>(this.apiPagedUrl+"maxPageNumber");
  }

  getMaxPageNumberFiltreli(search: string):Observable<pageInfo>
  {
    return this.http.get<pageInfo>(this.apiPagedUrl+"maxPageNumberFiltreli="+search);
  }
}
