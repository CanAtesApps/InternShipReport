import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, NgForm, NgModel, Validators } from '@angular/forms';
import { inspectNativeElement } from '@angular/platform-browser/src/dom/debug/ng_probe';
import { Router } from '@angular/router';
import { AlertService } from '@full-fledged/alerts';
import { Il, Ilce, Mahalle, Tasinmaz } from 'src/app/Model';
import { AdresService } from 'src/app/shared/services/Adres.service';
import { TasinmazService } from 'src/app/shared/services/Tasinmaz.service';

@Component({
  selector: 'app-tasinmaz-ekle',
  templateUrl: './tasinmaz-ekle.component.html',
  styleUrls: ['./tasinmaz-ekle.component.css']
})
export class TasinmazEkleComponent implements OnInit {

  iller : Il[];
  ilceler : Ilce[];
  mahalleler : Mahalle[];
  tasinmaz : Tasinmaz;

  model ={
    cityID : 0,
    countyID :0,
    mahalleID : 0,
    ada : 0,
    parsel:0,
    nitelik : "",
    adres : "",
    isActive : true
  };

  tasinmazForm: FormGroup;
  submitted = false;

  constructor(private tasinmazService : TasinmazService
    ,private adresService : AdresService
    ,private http : HttpClient
    ,private alertService: AlertService
    ,private formBuilder: FormBuilder
    ,private _router: Router)
    {

}
  ngOnInit()
  {
    this.tasinmazForm = this.formBuilder.group({
      selectedIl      : ['', Validators.required],
      selectedIlce    : ['', Validators.required],
      selectedMahalle : ['', Validators.required],
      ada             : ['', [Validators.required, Validators.min(1)]],
      parsel          : ['', [Validators.required, Validators.min(1)]],
      nitelik         : ['', [Validators.required, Validators.minLength(3)]],
      adres           : ['', [Validators.required, Validators.minLength(6)]]
  });
    this.getIls();
  }

  async getIls()
  {
     await this.adresService.getAllIls().subscribe(response =>
      {
        this.iller = response;
      });
  }
  onChangeIlSelect(newValue)
  {
    this.getIlceById(newValue);
    this.mahalleler = null;
  }
  onChangeIlceSelect(newValue)
  {
    this.gelMahalleByID(newValue);

  }
  onChangeMahalleSelect()
  {
    this.tasinmazForm.markAsPristine();
  }
  onSubmit(f: NgForm)
  {
    this.submitted = true;
      if (this.tasinmazForm.invalid)
      {
        return;
      }
      const tasinmazPostObserver = {
        next: x => {
          this.alertService.success('Ekleme başarılı');
          this._router.navigateByUrl('/tasinmaz');
        },
        error: err =>{
          this.alertService.danger('Ekleme basarisiz');
        }
      };
      this.tasinmazService.createTasinmaz(this.tasinmazForm.value).subscribe(tasinmazPostObserver);
  }
  getIlceById(id : number)
  {
     this.adresService.getIlceById(id).subscribe(response =>
      {
        this.ilceler = response;
      })
  }
  gelMahalleByID(id :number)
  {
    this.adresService.getMahalleById(id).subscribe(response =>
      {
        this.mahalleler = response;
      })
  }

  get f() { return this.tasinmazForm.controls; }

}
