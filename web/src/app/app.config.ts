import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideAuth0 } from '@auth0/auth0-angular';
import { provideHttpClient } from '@angular/common/http';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideAuth0({
      domain: "dev-6fhz7djtuwfudwow.us.auth0.com",
      clientId: "N907WnCzBmUpUjaFRBr0bxX154CYcKnR",
      authorizationParams: {
        redirect_uri: window.location.origin,
        audience: "https://pigeon/api"
      }
    }),
    provideHttpClient()
  ]
};
