import { Component, OnInit } from '@angular/core';
import { Il, Ilce, Mahalle, Tasinmaz, TasinmazPost, TasinmazPut } from 'src/app/Model';
import { TasinmazService } from 'src/app/shared/services/Tasinmaz.service';
import { FormBuilder, FormControl, FormGroup, NgForm, NgModel, Validators } from '@angular/forms';
import { AdresService } from 'src/app/shared/services/Adres.service';
import { AlertService } from '@full-fledged/alerts';
import { Router } from '@angular/router';
import { timeout } from 'rxjs/operators';

@Component({
  selector: 'app-tasinmaz-guncelle',
  templateUrl: './tasinmaz-guncelle.component.html',
  styleUrls: ['./tasinmaz-guncelle.component.css']
})
export class TasinmazGuncelleComponent implements OnInit {

  selectedTasinmaz     : Tasinmaz
  selectedTasinmazPost : TasinmazPost;
  tasinmazForm         : FormGroup;
  iller                : Il[];
  ilceler              : Ilce[];
  mahalleler           : Mahalle[];
  id                   : any;
  submitted            = false;

  constructor(private tasinmazService : TasinmazService
    ,private adresService : AdresService
    ,private alertService: AlertService
    ,private formBuilder: FormBuilder
    ,private _router: Router)
    {

      this.selectedTasinmaz = (JSON.parse(localStorage.getItem("selectedTasinmaz")));
      this.selectedTasinmazPost = (JSON.parse(localStorage.getItem("selectedTasinmazPost")));
      this.getIls();

      var il : Il ;
      var ilce : Ilce;
      var mahalle : Mahalle;
      this.adresService.getIlByName(this.selectedTasinmaz.cityName).subscribe(response =>{
        il = response;
        localStorage.setItem('ilID',il.cityID);
        this.getIlceById(il.cityID);
        this.adresService.getOneIlceByName(il.cityID,this.selectedTasinmaz.countyName).subscribe(responseilce =>
          {
            ilce = responseilce;
            localStorage.setItem('ilceID',ilce.countyID);
            this.adresService.getMahalleById(ilce.countyID).subscribe(response => {
              this.mahalleler = response;
              this.adresService.getOneMahalleByName(ilce.countyID,this.selectedTasinmaz.areaName).subscribe(response =>{
                mahalle = response;
                localStorage.setItem('mahalleID',mahalle.mahalleID);
              });
            });
          });
      });
    }


  ngOnInit() {
    this.selectedTasinmaz = (JSON.parse(localStorage.getItem("selectedTasinmaz")));
    this.selectedTasinmazPost = (JSON.parse(localStorage.getItem("selectedTasinmazPost")));

    this.tasinmazForm = this.formBuilder.group({
      selectedIl      : ['', Validators.required],
      selectedIlce    : ['', Validators.required],
      selectedMahalle : ['', Validators.required],
      ada             : ['', [Validators.required, Validators.min(1)]],
      parsel          : ['', [Validators.required, Validators.min(1)]],
      nitelik         : ['', [Validators.required, Validators.minLength(3)]],
      adres           : ['', [Validators.required, Validators.minLength(6)]]
    });

    this.tasinmazForm.setValue({
      selectedIl      : this.selectedTasinmaz.cityName,
      selectedIlce    : this.selectedTasinmaz.countyName,
      selectedMahalle : this.selectedTasinmaz.areaName,
      ada             : this.selectedTasinmaz.ada,
      parsel          : this.selectedTasinmaz.parsel,
      nitelik         : this.selectedTasinmaz.nitelik,
      adres           : this.selectedTasinmaz.adres
    });

  }
  async getIls()
  {
     await this.adresService.getAllIls().subscribe(response =>
      {
        this.iller = response;
      });
  }
  IlID : number;

  onChangeIlSelect(newValue)
  {
    var il : Il ;
    this.selectedTasinmaz.cityName =newValue;
    this.adresService.getIlByName(newValue).subscribe(response =>{
      il =response;
      this.mahalleler = null;
      localStorage.setItem('ilID',il.cityID);
      this.getIlceById(il.cityID);
    });
  }
  IlceID:number;

  onChangeIlceSelect(newValue)
  {
    this.selectedTasinmaz.countyName = newValue;
    this.IlID = parseInt(localStorage.getItem('ilID'));
    var ilce : Ilce;
    this.adresService.getOneIlceByName(this.IlID,newValue).subscribe(response =>{
      ilce =response;
      localStorage.setItem('ilceID',ilce.countyID);
      this.adresService.getMahalleById(ilce.countyID).subscribe(responseMahalle =>{
        this.mahalleler = responseMahalle;
      });
    })
  }
  mahalleID:number;
  onChangeMahalleSelect(newValue)
  {
    this.IlceID = parseInt(localStorage.getItem('ilceID'));
    var mahalle : Mahalle;
    this.selectedTasinmaz.areaName=newValue;
    this.adresService.getOneMahalleByName(this.IlceID,newValue).subscribe(response =>{
      mahalle = response;
      localStorage.setItem('mahalleID', mahalle.mahalleID);
    })

    this.tasinmazForm.markAsPristine();
  }
  getIlceById(id : number)
  {
     this.adresService.getIlceById(id).subscribe(response =>
      {
        this.ilceler = response;
      })
  }
  getIlceByName(name : string)
  {

     this.adresService.getIlceByName(name.toLowerCase()).subscribe(response =>
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
  // convenience getter for easy access to form fields
  get f() { return this.tasinmazForm.controls; }

  tmp : TasinmazPut = new TasinmazPut();
  onSubmit(f: NgForm)
  {
    this.submitted = true;

      // stop here if form is invalid
      if (this.tasinmazForm.invalid)
      {
        return;
      }
      const tasinmazUpdateObserver = {
        next: x => {
          this.alertService.success('Guncelleme başarılı');
          this._router.navigateByUrl('/tasinmaz');
        },
        error: err =>{
          this.alertService.danger('Guncelleme basarisiz');
        }
      };

      var il : Il;
      var ilce : Ilce;
      var mahalle : Mahalle;

      this.selectedTasinmaz.cityName   = this.tasinmazForm.get('selectedIl').value;
      this.selectedTasinmaz.countyName = this.tasinmazForm.get('selectedIlce').value;
      this.selectedTasinmaz.areaName   = this.tasinmazForm.get('selectedMahalle').value;

      this.tmp.tasinmazID = parseInt(localStorage.getItem('selectedTasinmazId'));
      this.tmp.cityID     = parseInt(localStorage.getItem('ilID'));
      this.tmp.countyID   = parseInt(localStorage.getItem('ilceID'));
      this.tmp.MahalleID  = parseInt(localStorage.getItem('mahalleID'));
      this.tmp.ada        = this.tasinmazForm.get('ada').value;
      this.tmp.parsel     = this.tasinmazForm.get('parsel').value;
      this.tmp.adres      = this.tasinmazForm.get('adres').value;
      this.tmp.nitelik    = this.tasinmazForm.get('nitelik').value;
      this.tmp.isActive   = true;

      this.tasinmazService.updateTasinmazbyId(this.tmp.tasinmazID,this.tmp).subscribe(tasinmazUpdateObserver);
  }
  getTasinmazIds(id : number)
  {
    return this.tasinmazService.getTasinmazIds(id).subscribe();
  }
  isSelectedCity(id:number):boolean
  {
    return (this.selectedTasinmazPost.cityID == id ) ? true : false;
  }
}
