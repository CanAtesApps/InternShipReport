import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Il, Ilce, Mahalle } from 'src/app/Model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdresService {
  ilUrl = "http://localhost:5000/api/Il";
  ilceUrl ="http://localhost:5000/api/Ilce";
  mahalleUrl ="http://localhost:5000/api/Mahalle";
  constructor(private http: HttpClient)
  {
  }

  getIlById(id:number):Observable<Il>
  {
    return this.http.get<Il>(this.ilUrl+"/"+id);
  }
  getIlByName(name:string):Observable<Il>
  {
    return this.http.get<Il>(this.ilUrl+"/name="+name);
  }
  getAllIls():Observable<Il[]>
  {
    return this.http.get<Il[]>(this.ilUrl);
  }
  getIlceById(id : number):Observable<Ilce[]>
  {
    return this.http.get<Ilce[]>(this.ilceUrl+"/"+id);
  }
  getIlceByName(name : string):Observable<Ilce[]>
  {
    return this.http.get<Ilce[]>(this.ilceUrl+"/name="+name);
  }
  getOneIlceByName(id:number,name : string):Observable<Ilce>
  {
    return this.http.get<Ilce>(this.ilceUrl+"/id="+id+"getOne="+name);
  }
  getOneIlceByNameOnly(name : string):Observable<Ilce>
  {
    return this.http.get<Ilce>(this.ilceUrl+"/getOne="+name);
  }
  getMahalleById(id : number):Observable<Mahalle[]>
  {
    return this.http.get<Mahalle[]>(this.mahalleUrl+"/"+id);
  }
  getOneMahalleByName(countyID:number,name : string):Observable<Mahalle>
  {
    return this.http.get<Mahalle>(this.mahalleUrl+"/id="+countyID+"getOne="+name);
  }
  getOneMahalleByNameOnly(name : string):Observable<Mahalle>
  {
    return this.http.get<Mahalle>(this.mahalleUrl+"/getOne="+name);
  }

}
