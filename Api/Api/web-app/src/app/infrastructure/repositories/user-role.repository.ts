import { Injectable } from '@angular/core';
import gatewayConfig from '../http/api-gateway-config';
import { BaseHttpClient } from '../http/base-http-client';

@Injectable({
  providedIn: 'root',
})
export class UserRoleRepository {
  constructor(private baseHttpClient: BaseHttpClient) { }

  public registerAccount(param: any) {
    screenLeft
    return this.baseHttpClient.postRequest({
      url: gatewayConfig.api_gateway,
      body: {
        param,
      },
    });
  }

  public login(param: any) {
    return this.baseHttpClient.postRequest({
      url: '/user/login',
      body: param,
    }, false);
  }

  public getAccountInfo() {
    return this.baseHttpClient.postRequest();
  }

  public refreshToken(param: any) {
    return this.baseHttpClient.postRequest({
      url: gatewayConfig.api_gateway,
      body: param,
    }, false);
  }
}
