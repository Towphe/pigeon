import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';
import { PreloadAllModules, provideRouter, withPreloading } from '@angular/router';
import { provideAuth0 } from '@auth0/auth0-angular';
import { provideHttpClient } from '@angular/common/http';
import { routes } from './app/app.routes';

bootstrapApplication(AppComponent, 
  {
    providers: [
      provideRouter(routes, withPreloading(PreloadAllModules)),
      provideAuth0({
        domain: 'https://dev-6fhz7djtuwfudwow.us.auth0.com',
        clientId: 'Zv579OpwKCR1n9kUDQgB8eNStSyHT8RL',
        authorizationParams: {
          audience: "https://pigeon/api",
          redirect_uri: "http://localhost:4200"
        }
      }),
      provideHttpClient()
    ]
  }
)
  .catch((err) => console.error(err));
