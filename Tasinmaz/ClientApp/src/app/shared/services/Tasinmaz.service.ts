import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Model, pageInfo, Tasinmaz, TasinmazPost, TasinmazPut } from 'src/app/Model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TasinmazService
{
  apiUrl = "http://localhost:5000/api/tasinmaz/tablo";
  apiPagedUrl = "http://localhost:5000/api/tasinmaz/";
  baseUrl = "http://localhost:5000/api/tasinmaz";
  model : Model;
  constructor(private http: HttpClient)
  {

  }
  getAllTasinmaz(page:number):Observable<Tasinmaz[]>
  {
    //return this.http.get<Tasinmaz[]>(this.apiUrl);
    return this.http.get<Tasinmaz[]>(this.apiPagedUrl+"page="+page);
  }
  getAllTasinmazFiltered(page:number,filter:string) : Observable<Tasinmaz[]>
  {
    return this.http.get<Tasinmaz[]>(this.apiPagedUrl+"pageNo="+page+"filter="+filter);
  }
  getMaxPageNumber():Observable<pageInfo>
  {
    return this.http.get<pageInfo>(this.apiPagedUrl+"maxPageNumber");
  }
  getMaxPageNumberFiltered(filter:string) : Observable<pageInfo>
  {
    return this.http.get<pageInfo>(this.apiPagedUrl+"maxPageNumberFiltreli="+filter);
  }
  createTasinmaz(tmp:any)
  {
    var newTasinmaz = {
      cityID : parseInt(tmp.selectedIl),
      countyID : parseInt(tmp.selectedIlce),
      mahalleID : parseInt(tmp.selectedMahalle),
      ada : tmp.ada,
      parsel : tmp.parsel,
      nitelik : tmp.nitelik,
      adres : tmp.adres,
      isActive : true
    };
    return this.http.post(this.baseUrl,newTasinmaz);

  }
  getTasinmazById(id : number) :Observable<TasinmazPost>
  {
    return this.http.get<TasinmazPost>(this.apiPagedUrl+id);
  }
  getTasinmazIds(id:number)
  {
    return this.http.get(this.baseUrl+"/getIDs="+id);
  }
  updateTasinmazbyId(id :number , tasinmaz : TasinmazPut)
  {
    return this.http.put(this.baseUrl+"/"+id,tasinmaz);
  }
  deleteTasinmaz(id:number)
  {
    return this.http.delete(this.baseUrl+"/"+id);
  }
}
