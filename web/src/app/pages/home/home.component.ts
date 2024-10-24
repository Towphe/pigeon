import { Component, inject, OnInit } from "@angular/core";
import { ActivatedRoute, Router, RouterOutlet } from "@angular/router";
import { AuthService } from "@auth0/auth0-angular";
import { TokenHandlerService } from "../../services/token-handler.service";

@Component({
  selector: "app-home",
  standalone: true,
  imports: [],
  templateUrl: "./home.component.html",
  styleUrl: "./home.component.scss",
})
export class HomeComponent {
  title = "Home";
  auth: AuthService = inject(AuthService);
  tokenHandler: TokenHandlerService = inject(TokenHandlerService);
  isAuthenticated: boolean = false;
  token!: string;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
  ) {}

  ngOnInit() {
    this.route.queryParamMap.subscribe((params) => {
      if (params.get("callback") != null) {
        if (this.tokenHandler.isTokenPresent()) {
          this.auth.isAuthenticated$.subscribe((isAuthenticated) => {
            alert(isAuthenticated);
          });
        } else {
          window.location.href = "/chat";
        }
      }
    });
  }

  async login() {
    this.auth.loginWithRedirect({
      appState: {
        target: '?callback="REDIRECT"',
      },
    });
  }

  async logout() {
    this.tokenHandler.removeToken();
    this.auth.logout();
  }
}
