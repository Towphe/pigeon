import { Component, inject, OnInit } from "@angular/core";
import { ActivatedRoute, Router, RouterOutlet } from "@angular/router";
import { AuthService, LogoutOptions } from "@auth0/auth0-angular";
import { TokenHandlerService } from "../../services/token-handler.service";

@Component({
  selector: "app-chat",
  standalone: true,
  imports: [],
  templateUrl: "./chat.component.html",
  styleUrl: "./chat.component.scss",
})
export class ChatComponent {
  auth: AuthService = inject(AuthService);
  tokenHandler: TokenHandlerService = inject(TokenHandlerService);

  constructor(
    private route: ActivatedRoute,
    private router: Router,
  ) {}

  logout() {
    this.tokenHandler.removeToken();
    this.auth.logout({
      logoutParams: {
        returnTo: "/",
      },
    });
  }
}
