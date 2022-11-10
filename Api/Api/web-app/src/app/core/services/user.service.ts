import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ACLService } from '@delon/acl';
import { DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';
import { SettingsService } from '@delon/theme';
import * as CryptoJS from 'crypto-js';
import ResponseModel from "../models/reponse-model";
import { UserRepository } from '../../infrastructure/repositories/user.repository';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(
    private authenRepository: UserRepository,
    private settingService: SettingsService,
    @Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private aclService: ACLService,
    private router: Router,
  ) { }

  public registerAccount(param: any) {
    return this.authenRepository.registerAccount(param);
  }

  public async login(param: any) {
    const { password, username } = param;
    const passwordEncryp = CryptoJS.MD5(password).toString();

    const resp: ResponseModel = await this.authenRepository.login({
      username: username, password: passwordEncryp
    }) || {};
    const { meta, data } = resp;

    if (meta?.error_code == 200) {
      const { roleLevel, access_key, access_token, ...user } = data;

      this.settingService.setUser({ ...param, roleLevel, ...user, password: passwordEncryp });
      this.tokenService.set({
        token: data?.AccessToken,
        refresh_token: data?.refresh_token,
        expired: +new Date() + 1000 * 60 * 23 * 59
      });

      this.aclService.setRole([roleLevel + '']);
    }

    return { code: meta?.error_code == 200 ? 0 : -1 };
  }

  public async refreshTokenRequest() {
    const userJSON = localStorage.getItem('user');
    if (userJSON) {
      const user = JSON.parse(userJSON);
      const { username, password } = user;

      const resp: any = (await this.authenRepository.login({ email: username, password: password })) || {};

      if (resp?.data && resp?.data?.access_token) {
        return { token: resp?.data?.access_token, refresh_token: resp?.data?.refresh_token, expired: +new Date() + 1000 * 23 * 60 * 58 };
      } else {
        throw Error();
      }
    }
    return null;
  }

  public logout() {
    this.tokenService.clear();
    localStorage.removeItem('user');
    this.settingService.setUser({});
    this.aclService.setRole([]);
    // this.settingService.setUser(undefined);
    this.router.navigateByUrl('/');
  }
}
