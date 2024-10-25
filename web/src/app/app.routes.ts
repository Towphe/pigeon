import { Routes } from "@angular/router";
import { HomeComponent } from "./pages/home/home.component";
import { ChatComponent } from "./pages/chat/chat.component";
import { ChatLayoutComponent } from "./components/chat-layout/chat-layout.component";

export const routes: Routes = [
  {
    path: "",
    component: HomeComponent,
  },
  {
    path: "chat",
    component: ChatLayoutComponent,
    children: [
      {
        path: "",
        component: ChatComponent,
      },
    ],
  },
];
