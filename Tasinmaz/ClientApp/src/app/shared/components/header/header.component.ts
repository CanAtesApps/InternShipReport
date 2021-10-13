import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertService } from '@full-fledged/alerts';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit
{
  constructor(public authService: AuthService
              ,private alertService: AlertService
              ,private _router: Router) {

  }
  ngOnInit() {

  }

  logout()
  {
    const logoutObserver = {
      next: x => {
        this.alertService.success('Çıkış başarılı');
        //this._router.navigateByUrl('');
      },
      error: err =>{
        this.alertService.danger('Çıkış  yapılamadı');
      }
    };
    this.authService.logout().subscribe(logoutObserver);
    localStorage.removeItem('token');
    localStorage.removeItem('rol');
    localStorage.clear();
  }
}
