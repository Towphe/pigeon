import { Component, inject } from "@angular/core";
import { ActivatedRoute, Router, RouterOutlet } from "@angular/router";
import { AuthService } from "@auth0/auth0-angular";
import { TokenHandlerService } from "../../services/token-handler.service";

// temporary contact object for mapping contacts
interface ContactDTO {
  contactId: string;
  username: string;
  isOnline: boolean;
}

@Component({
  selector: "chat-sidebar",
  standalone: true,
  imports: [],
  templateUrl: "./chat-sidebar.component.html",
  styleUrl: "./chat-sidebar.component.scss",
})
export class ChatSidebarComponent {
  auth: AuthService = inject(AuthService);
  tokenHandler: TokenHandlerService = inject(TokenHandlerService);

  testImageUrl: string =
    "https://avatars.githubusercontent.com/u/74641207?s=400&u=8e4844ae11895c4cfe90593f04de677ccdfef8a3&v=4";
  isCollapsed: boolean = true;
  contacts: ContactDTO[] = [
    {
      contactId: "joejeabean",
      username: "joejeadoodee",
      isOnline: false,
    },
    {
      contactId: "joejeabean",
      username: "joejeadoodee",
      isOnline: false,
    },
  ];

  async logout() {
    this.tokenHandler.removeToken();
    this.auth.logout();
  }
}
