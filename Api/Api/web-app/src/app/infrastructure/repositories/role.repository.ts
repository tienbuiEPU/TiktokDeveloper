import { Injectable } from '@angular/core';
import { BaseHttpClient } from '../http/base-http-client';
import GetByPageModel from 'src/app/core/models/get-by-page-model';
import { objectToQueryString } from '../utils/object-to-query-string';

@Injectable({
  providedIn: 'root',
})
export class RoleRepository {
  public baseUrl = "/api/role";

  constructor(private baseHttpClient: BaseHttpClient) { }

  public getByPage(params: GetByPageModel) {
    return this.baseHttpClient.getRequest({
      url: `${this.baseUrl}/GetByPage${objectToQueryString(params)}`,
    });
  }

  public addNew(param: any) {
    return this.baseHttpClient.postRequest({
      url: `${this.baseUrl}`,
      body: param
    });
  }

  public async update(param: any) {
    return this.baseHttpClient.putRequest({
      url: `${this.baseUrl}/${param.Id}`,
      body: param
    });
  }

  public async delete(param: any) {
    return this.baseHttpClient.deleteRequest({
      url: `${this.baseUrl}/${param.Id}`
    });
  }
}
