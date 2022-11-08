import { HttpClient, HttpEvent, HttpHeaders, HttpParams, HttpRequest, HttpResponseBase } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';
import { lastValueFrom, Observable } from 'rxjs';
import ResponseModel from 'src/app/core/models/reponse-model';

import gatewayConfig from './api-gateway-config';

interface IRequestParameter {
  url?: string;
  body?: any | null;
  headers?: any;
  options?: {
    headers?:
    | HttpHeaders
    | {
      [header: string]: string | string[];
    };
    observe?: string;
    params?:
    | HttpParams
    | {
      [param: string]: string | string[];
    };
    reportProgress?: boolean;
    responseType?: string;
    withCredentials?: boolean;
  };
}

@Injectable({
  providedIn: 'root',
})
export class BaseHttpClient {
  constructor(protected http: HttpClient,
    @Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
  ) { }

  baseRequest(method: string, params?: IRequestParameter, stringifyBody: boolean = true): Promise<any> {
    const url = params?.url;
    const headers = params?.headers;
    const token = this.tokenService.get()?.token;
    const bearer = token ? { Authorization: `Bearer ${token}` } : undefined;

    return lastValueFrom(this.http
      .request(method, url ?? "",
        {
          ...params,
          body: stringifyBody && params?.body ? JSON.stringify(params?.body) : params?.body,
          headers: {
            'Content-Type': 'application/json; charset=utf-8',
            ...headers,
            ...bearer,
          },
        }));
  }

  baseUploadRequest(params?: IRequestParameter): Promise<any> {
    const url = params?.url?.startsWith("http") ? params?.url : gatewayConfig.api_gateway + params?.url;
    const headers = params?.headers;
    const token = this.tokenService.get()?.token;
    const bearer = token ? { Authorization: `Bearer ${token}` } : undefined;

    const uploadReq = new HttpRequest('POST', url, params?.body, {
      headers: new HttpHeaders({
        ...headers,
        ...bearer
      }),
      reportProgress: true,
    });

    return lastValueFrom(this.http
      .request(uploadReq));
  }


  public getRequest(params?: IRequestParameter): Promise<ResponseModel> {
    return this.baseRequest("GET", params);
  }

  public postRequest(params?: IRequestParameter, stringifyBody: boolean = true): Promise<ResponseModel> {
    return this.baseRequest("POST", params, stringifyBody);
  }

  public putRequest(params?: IRequestParameter, stringifyBody: boolean = true): Promise<ResponseModel> {
    return this.baseRequest("PUT", params, stringifyBody);
  }

  public deleteRequest(params?: IRequestParameter, stringifyBody: boolean = true): Promise<ResponseModel> {
    return this.baseRequest("DELETE", params, stringifyBody);
  }

  public uploadRequest(params?: IRequestParameter): Promise<ResponseModel> {
    return this.baseUploadRequest(params).then((result) => {
      return result.body;
    });
  }

  public downloadRequest(params?: IRequestParameter): Promise<string> {
    return new Promise<string>((resolve, reject) => {
      try {
        const url = params?.url?.startsWith("http") ? params?.url : gatewayConfig.api_gateway + params?.url;
        const headers = params?.headers;
        const token = this.tokenService.get()?.token;
        const bearer = token ? { Authorization: `Bearer ${token}` } : undefined;

        const httpOptions = {
          headers: {
            'Content-Type': 'application/json; charset=utf-8',
            ...bearer,
            ...headers,
          },
          responseType: 'blob' as 'json'
        };

        this.http.get(url, httpOptions).subscribe((blob: any) => {
          const file = new Blob([blob], { type: 'application/pdf' });
          const fileURL = URL.createObjectURL(file);

          resolve(fileURL);
        });
      } catch (error) {
        reject(error);
      }
    })
  }
}
