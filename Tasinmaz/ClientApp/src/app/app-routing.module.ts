import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { LoginComponent } from './auth/components/login/login.component';
import { KullaniciComponent } from './pages/kullanici/kullanici.component';
import { LogComponent } from './pages/log/log.component';
import { ModalComponent } from './pages/modal/modal.component';
import { TasinmazEkleComponent } from './pages/tasinmaz-ekle/tasinmaz-ekle.component';
import { TasinmazGuncelleComponent } from './pages/tasinmaz-guncelle/tasinmaz-guncelle.component';
import { TasinmazComponent } from './pages/tasinmaz/tasinmaz.component';
import { CollumOneComponent } from './shared/layouts/collum-one/collum-one.component';

const routes: Routes = [
  {
    path: '',
    component: LoginComponent
  },
  {
    path: 'tasinmaz',
    component: TasinmazComponent
  },
  {
    path: 'tasinmazEkle',
    component: TasinmazEkleComponent
  },
  {
    path: 'tasinmazGuncelle',
    component: TasinmazGuncelleComponent
  },
  {
    path:'modalDeneme',
    component: ModalComponent
  },
  {
    path:'kullanici',
    component: KullaniciComponent
  },
  {
    path:'log',
    component: LogComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {  }
