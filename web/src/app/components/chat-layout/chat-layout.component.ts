import { Component, Inject } from "@angular/core";
import { RouterOutlet } from "@angular/router";
import { ChatSidebarComponent } from "../chat-sidebar/chat-sidebar.component";

@Component({
  selector: "app-chat-layout",
  standalone: true,
  imports: [RouterOutlet, ChatSidebarComponent],
  templateUrl: "./chat-layout.component.html",
  styleUrl: "./chat-layout.component.scss",
})
export class ChatLayoutComponent {}
