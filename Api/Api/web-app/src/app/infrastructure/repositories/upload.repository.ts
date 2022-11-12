import { Injectable } from '@angular/core';
import { BaseHttpClient } from '../http/base-http-client';

@Injectable({
  providedIn: 'root',
})
export class UploadRepository {
  public baseUrl = "/api/upload";

  constructor(private baseHttpClient: BaseHttpClient) { }

  public async uploadImage(param: any) {
    return this.baseHttpClient.uploadRequest({
      url: `${this.baseUrl}/uploadImage`,
      body: param
    });
  }

  public async uploadFile(param: any) {
    return this.baseHttpClient.uploadRequest({
      url: `${this.baseUrl}/uploadFile`,
      body: param
    });
  }
}
