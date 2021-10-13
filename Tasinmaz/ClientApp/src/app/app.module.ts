import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SharedModule } from './shared/shared.module';
import { AuthModule } from './auth/auth.module';
import { HttpClientModule } from '@angular/common/http';
import { TasinmazComponent } from './pages/tasinmaz/tasinmaz.component';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { NgxPaginationModule } from 'ngx-pagination';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TasinmazEkleComponent } from './pages/tasinmaz-ekle/tasinmaz-ekle.component';
import { AlertModule } from '@full-fledged/alerts';
import { TasinmazGuncelleComponent } from './pages/tasinmaz-guncelle/tasinmaz-guncelle.component';
import { ModalComponent } from './pages/modal/modal.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { KullaniciComponent } from './pages/kullanici/kullanici.component';
import { LogComponent } from './pages/log/log.component';
import { ExcelService } from './shared/services/excel.service';


@NgModule({
  declarations: [
    AppComponent,
    TasinmazComponent,
    TasinmazEkleComponent,
    TasinmazGuncelleComponent,
    ModalComponent,
    KullaniciComponent,
    LogComponent
  ],
  entryComponents: [ModalComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    SharedModule,
    AuthModule,
    HttpClientModule,
    Ng2SearchPipeModule,
    NgxPaginationModule,
    FormsModule,
    NgbModule,
    ReactiveFormsModule,
    AlertModule.forRoot({maxMessages: 5, timeout: 5000, positionX: 'right'})
  ],
  providers: [ExcelService],
  bootstrap: [AppComponent]
})
export class AppModule { }
