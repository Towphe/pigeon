import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AuthService } from '@auth0/auth0-angular';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit  {
  title = 'pigeon';
  auth:AuthService = inject(AuthService);
  isAuthenticated:boolean = false;
  token!:string;

  constructor()
  {
    this.auth.isAuthenticated$.subscribe(
      n => {
        this.isAuthenticated = n.valueOf();

        if (this.isAuthenticated)
        {
          this.auth.getAccessTokenSilently().subscribe(
            token => {
              console.log(token);
            }
          )
        }
      }
    )
  }

  ngOnInit()
  {
    // this.auth.getAccessTokenSilently()
    //   .subscribe(token => {
    //     this.token = token;
    //     console.log(token);
    //   });
  }

  async login()
  {
    await this.auth.loginWithRedirect();
  }


}
