import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { AlertService } from '@full-fledged/alerts';
import { Il, pageInfo, Tasinmaz } from 'src/app/Model';
import { AdresService } from 'src/app/shared/services/Adres.service';
import { ExcelService } from 'src/app/shared/services/excel.service';
import { ModalService } from 'src/app/shared/services/modal.service';
import { TasinmazService } from 'src/app/shared/services/Tasinmaz.service';

@Component({
  selector: 'app-tasinmaz',
  templateUrl: './tasinmaz.component.html',
  styleUrls: ['./tasinmaz.component.css']
})
export class TasinmazComponent implements OnInit
{

  SelectedTasinmaz    : Tasinmaz;
  tasinmazlar         : Tasinmaz[];
  values              : any;
  il                  : Il;
  term;
  pageInformation     : pageInfo;
  currentPage         = 1;
  buttonDisabled      = false;
  filter              : string;

  constructor(private tasinmazService : TasinmazService
            ,private adresService : AdresService
            ,private http : HttpClient
            ,private alertService: AlertService
            ,private modalService: ModalService
            ,private _router: Router
            ,private excelService : ExcelService)
            {

  }
  getTasinmazByFilterAndPageNumber()
  {
    if(this.filter == '' || this.filter == null )
    {
      this.getAllTasinmaz(this.currentPage);
    }
    else
    {
      this.tasinmazService.getAllTasinmazFiltered(this.currentPage,this.filter).subscribe(response =>{
        this.tasinmazlar = response;
      });
      this.getMaxPageNumberFiltered(this.filter);
    }

  }

  ngOnInit()
  {
    this.getTasinmazByFilterAndPageNumber();
  }
  filtrele()
  {
    this.getTasinmazByFilterAndPageNumber();
  }
  getMaxPageNumberFiltered(filter : string)
  {
    this.tasinmazService.getMaxPageNumberFiltered(filter).subscribe(response =>{
      this.pageInformation = response;
    });
  }
  counter()
  {
    if(this.pageInformation != null)
    {
       return Array.from(Array(this.pageInformation.maxPageNumber), (_, i) => i + 1);
    }
    else
      return 0;
  }
  show(i:number)
  {
    this.tasinmazService.getAllTasinmaz(i).subscribe(response =>
      {
        this.tasinmazlar =response;
      });

  }
  getAllTasinmaz(pageNumber : number)
  {
    this.tasinmazService.getAllTasinmaz(pageNumber).subscribe(response =>
      {
        this.tasinmazlar =response;
      });
    this.getMaxPageNumber();
  }
  getMaxPageNumber()
  {
    this.tasinmazService.getMaxPageNumber().subscribe(response =>
      {
       this.pageInformation = response;
      });
  }
  getMaxNumber():number
  {
    return this.pageInformation.maxPageNumber;
  }
  getIlNameById(id:number)
  {
    this.adresService.getIlById(id).subscribe(response =>
      {
        this.il = response;
      });
      return this.il.cityName;
  }

  previousPage()
  {
    this.currentPage--;
    this.getTasinmazByFilterAndPageNumber();
  }
  onChangeSelect(tmp:any)
  {
    this.currentPage=tmp;
    this.getTasinmazByFilterAndPageNumber();
  }
  nextPage()
  {
    this.currentPage++;
    this.getTasinmazByFilterAndPageNumber();
  }
  previousButtonDisable()
  {
    return (this.currentPage > 1) ? false : true;
  }
  nextButtonDisable()
  {
    if(this.pageInformation !=null)
      return (this.currentPage == this.pageInformation.maxPageNumber) ? true : false;
    else
      return false;
  }
  getTasinmazById(id : number)
  {
    this.tasinmazService.getTasinmazById(id).subscribe(response =>{
      localStorage.setItem('selectedTasinmazPost', JSON.stringify(response));
    });
  }
  guncelle(tmp : Tasinmaz)
  {
    localStorage.setItem('selectedTasinmaz', JSON.stringify(tmp));
    this.getTasinmazById(tmp.tasinmazID);
    localStorage.setItem('selectedTasinmazId',tmp.tasinmazID);
    this._router.navigateByUrl('/tasinmazGuncelle');
  }
  sil(tmp : Tasinmaz)
  {
    this.SelectedTasinmaz = tmp;
  }
  confirmedDelete(){
    console.log(this.SelectedTasinmaz);
    const tasinmazDeleteObserver = {
      next: x => {
        this.alertService.success('Silme islemi başarılı');
        this.tasinmazService.getAllTasinmaz(this.currentPage).subscribe(response =>
          {
            this.tasinmazlar =response;
            this.getMaxPageNumber();
          });
      },
      error: err =>{
        this.alertService.danger('Silme islemi basarisiz');
      }
    };
    this.tasinmazService.deleteTasinmaz(this.SelectedTasinmaz.tasinmazID).subscribe(tasinmazDeleteObserver);
  }
  raporla(tmp:Tasinmaz)
  {
    var tmpList = new Array(1);
    tmpList[0]=tmp;
    this.excelService.exportAsExcelFile(tmpList, 'TasinmazData');
  }
}
