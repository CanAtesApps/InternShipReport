import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from './shared/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'ClientApp';
  helper = new JwtHelperService();

  constructor(public authService: AuthService,private _router: Router) {

  }
  ngOnInit(){
    const token = localStorage.getItem('token');
    this.authService.decodedToken = this.helper.decodeToken(token);
    if(localStorage.getItem('rol')== 'true')
    {
      this.authService.isAdmin = true;
    }
    else
    this.authService.isAdmin = false;
    if(this.authService.loggedIn())
    {
      this._router.navigateByUrl('/tasinmaz');
    }
    else
    {
      this._router.navigateByUrl('');
    }
    //this.authService.isAdmin=localStorage.getItem('rol');

  }
}
