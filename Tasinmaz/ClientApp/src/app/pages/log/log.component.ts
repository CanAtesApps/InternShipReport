import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertService } from '@full-fledged/alerts';
import { Log, pageInfo } from 'src/app/Model';
import { ExcelService } from 'src/app/shared/services/excel.service';
import { LogService } from 'src/app/shared/services/log.service';

@Component({
  selector: 'app-log',
  templateUrl: './log.component.html',
  styleUrls: ['./log.component.css']
})
export class LogComponent implements OnInit {

  currentPage     = 1;
  logs            : Log[];
  pageInformation : pageInfo;
  filter          = "";


  constructor(private logService  : LogService
            ,private excelService : ExcelService) {

  }
  getLogByFilterAndPageNumber()
  {
    if(this.filter == '' || this.filter == null )
    {
      this.getPagedLogs(this.currentPage);
    }
    else
    {
      this.logService.getPagedLogsWithFilter(this.currentPage,this.filter).subscribe(response =>{
        this.logs = response;
      });
      this.getMaxPageNumberFiltered(this.filter);
    }
  }

  ngOnInit()
  {
    this.getLogByFilterAndPageNumber();
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
  previousPage()
  {
    this.currentPage--;
    this.getLogByFilterAndPageNumber();
  }
  nextPage()
  {
    this.currentPage++;
    this.getLogByFilterAndPageNumber();
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
  getPagedLogs(page:number)
  {
    this.logService.getPagedLogs(page).subscribe(response => {
      this.logs = response;
    });
    this.getMaxPageNumber();
  }
  getMaxPageNumber()
  {
    this.logService.getMaxPageNumber().subscribe(response =>
      {
       this.pageInformation = response;
      });
  }
  getMaxPageNumberFiltered(search : string)
  {
    this.logService.getMaxPageNumberFiltreli(search).subscribe(response =>{
      this.pageInformation = response;
    });
  }
  onChangeSelect(tmp:any)
  {
    this.currentPage = tmp;
    this.getLogByFilterAndPageNumber();
  }
  filtrele()
  {
    this.getLogByFilterAndPageNumber();
  }
  raporla()
  {
    this.excelService.exportAsExcelFile(this.logs, 'Loglar');
  }
}
