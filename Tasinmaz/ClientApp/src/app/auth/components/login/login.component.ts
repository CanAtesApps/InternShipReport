import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AlertService } from '@full-fledged/alerts';
import { sha256, sha224 } from 'js-sha256';
import { AuthService } from 'src/app/shared/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {

  constructor(private authService: AuthService
              ,private alertService: AlertService
              ,private _router: Router){

  }

  ngOnInit() {
  }
  loginInfo       = {username : "" , password :"" };
  loginInfoServer = {username : "" , password :"" };

  onSubmit(f: NgForm)
  {
    this.loginInfoServer.username = this.loginInfo.username;
    this.loginInfoServer.password = sha256(this.loginInfo.password).toUpperCase()

    const loginObserver = {
      next: x => {
        this.alertService.success('Giriş başarılı');
        this._router.navigateByUrl('/tasinmaz');
      },
      error: err =>{
        this.alertService.danger('Hatalı kullanıcı adı yada parola');
      }
    };
    this.authService.login(this.loginInfoServer).subscribe(loginObserver);
  }


}
