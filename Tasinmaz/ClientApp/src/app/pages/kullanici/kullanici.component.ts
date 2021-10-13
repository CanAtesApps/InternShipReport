import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AlertService } from '@full-fledged/alerts';
import { KullaniciTable, pageInfo, UserAlldetails, userPost } from 'src/app/Model';
import { ExcelService } from 'src/app/shared/services/excel.service';
import { KullaniciService } from 'src/app/shared/services/kullanici.service';

@Component({
  selector: 'app-kullanici',
  templateUrl: './kullanici.component.html',
  styleUrls: ['./kullanici.component.css']
})
export class KullaniciComponent implements OnInit {

  selectedKullanici : KullaniciTable
  term              :string;
  kullanicilar      : KullaniciTable[]
  pageInformation   : pageInfo;
  filter            = "";
  currentPage       = 1;

  submitted = false;
  submittedUpdate = false;
  kullaniciForm = this.formBuilder.group({
    ad       : ['', Validators.required],
    soyad    : ['', Validators.required],
    rol      : ['', Validators.required],
    password : ['', Validators.required],
    email    : ['', [Validators.required,Validators.pattern(/^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/)]]
  });
  kullaniciDuzenleForm = this.formBuilder.group({
    ad       : [null, Validators.required],
    soyad    : [null, Validators.required],
    rol      : [null, Validators.required],
    password : [null, Validators.required],
    email    : [null, [Validators.required,Validators.pattern(/^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/)]]
  });

  constructor(private kullaniciService : KullaniciService
      ,private alertService: AlertService
      ,private formBuilder: FormBuilder
      ,private excelService: ExcelService)
  {}

  getUsersByFilterAndPageNumber()
  {
    if(this.filter=='' || this.filter==null )
    {
      this.getPagedUsers(this.currentPage);
    }
    else
    {
      this.kullaniciService.getPagedUsersWithFilter(this.currentPage,this.filter).subscribe(response =>{
        this.kullanicilar = response;
      });
      this.getMaxPageNumberFiltered(this.filter);
    }
  }

  ngOnInit()
  {
    this.getUsersByFilterAndPageNumber();
  }

  getPagedUsers(pageNumber:number)
  {
    this.kullaniciService.getPagedUsers(pageNumber).subscribe(response =>{
      this.kullanicilar = response;
    });
    this,this.getMaxPageNumber();
  }

  getMaxPageNumberFiltered(search : string)
  {
    this.kullaniciService.getMaxPageNumberFiltreli(search).subscribe(response =>{
      this.pageInformation = response;
    });
  }

  getMaxPageNumber()
  {
    this.kullaniciService.getMaxPageNumber().subscribe(response =>{
      this.pageInformation = response;
    });
  }

  previousPage()
  {
    this.currentPage--;
    this.getUsersByFilterAndPageNumber();
  }

  previousButtonDisable()
  {
    return (this.currentPage > 1) ? false : true;
  }

  nextPage()
  {
    this.currentPage++
    this.getUsersByFilterAndPageNumber();
  }

  nextButtonDisable()
  {
    if(this.pageInformation !=null)
      return (this.currentPage == this.pageInformation.maxPageNumber) ? true : false;
    else
      return false;
  }

  getAllUsers()
  {
    this.kullaniciService.getAllUsers().subscribe(response => {
      this.kullanicilar = response;
    });
  }

  resetForm(){
    this.kullaniciForm.reset();
    this.submitted = false;
  }

  onSubmitCreateUser(){

    this.submitted=true;
    if (this.kullaniciForm.invalid)
    {
      return;
    }
    console.log(this.kullaniciForm.value);
    const kullaniciPostObserver = {
      next: x => {
        this.alertService.success('Ekleme başarılı');
        this.getAllUsers();
      },
      error: err =>{
        this.alertService.danger('Ekleme basarisiz');
      }
    };

    var tmp      = new userPost();
    tmp.ad       = this.kullaniciForm.get('ad').value;
    tmp.soyad    = this.kullaniciForm.get('soyad').value;
    tmp.email    = this.kullaniciForm.get('email').value;
    tmp.password = this.kullaniciForm.get('password').value;
    tmp.rol      = this.kullaniciForm.get('rol').value == "true"  ? true: false;
    this.kullaniciService.createUser(tmp).subscribe(kullaniciPostObserver);
  }
  selectedUser(tmp:KullaniciTable)
  {
    this.selectedKullanici = tmp;
  }
  selectedUserUpdate(tmp:KullaniciTable)
  {
    this.selectedKullanici = tmp;
    this.kullaniciDuzenleForm.get('ad').setValue(this.selectedKullanici.ad);
    this.kullaniciDuzenleForm.get('soyad').setValue(this.selectedKullanici.soyad);
    this.kullaniciDuzenleForm.get('email').setValue(this.selectedKullanici.email);
    this.kullaniciDuzenleForm.get('rol').setValue(this.selectedKullanici.rol);
  }
  updateUser()
  {
    this.submittedUpdate=true;
    if (this.kullaniciDuzenleForm.invalid)
    {
      return;
    }
    const kullaniciUpdateObserver = {
      next: x => {
        this.alertService.success('Duzenleme başarılı');
        this.submittedUpdate= false;
        this.getAllUsers();
      },
      error: err =>{
        this.alertService.danger('Duzenleme basarisiz');
      }
    };

    var tmpUser = new UserAlldetails();
    this.kullaniciService.getSingleUser(this.selectedKullanici.userID).subscribe(response =>{
      tmpUser          = response;
      tmpUser.ad       = this.kullaniciDuzenleForm.get('ad').value;
      tmpUser.soyad    = this.kullaniciDuzenleForm.get('soyad').value;
      tmpUser.email    = this.kullaniciDuzenleForm.get('email').value;
      tmpUser.password = this.kullaniciDuzenleForm.get('password').value;
      tmpUser.rol      = this.kullaniciDuzenleForm.get('rol').value == "admin"  ? true: false;
      this.kullaniciService.updateUser(tmpUser).subscribe(kullaniciUpdateObserver);
    });
  }
  confirmedDelete()
  {
    const kullaniciDeleteObserver = {
      next: x => {
        this.alertService.success('Silme başarılı');
        this.getAllUsers();
      },
      error: err =>{
        this.alertService.danger('Silme basarisiz');
      }
    };

    this.kullaniciService.deleteUser(this.selectedKullanici.userID).subscribe(kullaniciDeleteObserver);
  }
  get f() { return this.kullaniciForm.controls; }

  get GuncelleForm() { return this.kullaniciDuzenleForm.controls; }

  counter()
  {
    if(this.pageInformation != null)
    {
       return Array.from(Array(this.pageInformation.maxPageNumber), (_, i) => i + 1);
    }
    else
      return 0;
  }
  onChangeSelect(tmp:any)
  {
    this.currentPage = tmp;
    this.getUsersByFilterAndPageNumber();
  }
  filtrele()
  {
    this.getUsersByFilterAndPageNumber();
  }
  raporla(tmp:KullaniciTable)
  {
    var tmpList = new Array(1);
    tmpList[0]=tmp;
    this.excelService.exportAsExcelFile(tmpList, 'KullaniciDetay');
  }
}
