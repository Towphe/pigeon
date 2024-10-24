import { Injectable } from '@angular/core';
import {DateTime} from 'luxon';
import Cookies from 'js-cookie';

@Injectable({
  providedIn: 'root'
})
export class TokenHandlerService {
  worker:any = null; // temporarily null until implemented later on

  constructor() {
    if (this.worker !== null)
    {
      // implement token storage/access through web worker here
    }
    else{
      // cookie implementation
    }
  }

  saveToken(accessToken:string):void
  {
    Cookies.set("a0-at", accessToken, {
      expires: DateTime.now().plus({days: 14}).toJSDate(),
      sameSite: "strict",
      secure: true
    });
  }

  retrieveToken():string|null
  {
    const token = Cookies.get("a0-at");

    if (token === undefined)
    {
      return null;  
    }
    return token;
  }

  removeToken():void
  {
    Cookies.remove("a0-at");
  }

  isTokenPresent():boolean
  {
    if (Cookies.get("a0-at"))
    {
      return true;
    }
    return false;
  }

}
