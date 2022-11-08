import { Injectable } from '@angular/core';
import GetByPageModel from 'src/app/core/models/get-by-page-model';
import { BaseHttpClient } from '../http/base-http-client';
import { objectToQueryString } from '../utils/object-to-query-string';

@Injectable({
    providedIn: 'root',
})
export class FunctionRepository {
    constructor(private baseHttpClient: BaseHttpClient) { }

    public getByPage(params: GetByPageModel) {
        return this.baseHttpClient.getRequest({
            url: "/Function/GetByPage" + objectToQueryString(params),
        });
    }

    public addNew(params: any[]) {
        var lsPromise: Array<Promise<any>> = [];
        params.forEach(item => {
            params.forEach(async item => {
                this.baseHttpClient.postRequest({
                    url: "/Function",
                    body: item,
                });
            })
        });

        return Promise.all(lsPromise);
    }

    public async update(params: any[]) {
        var lsPromise: Array<Promise<any>> = [];
        params.forEach(item => {
            lsPromise.push(this.baseHttpClient.putRequest({
                url: "/Function/" + item.FunctionId,
                body: item,
            }))
        });

        return Promise.all(lsPromise);
    }

    public deletes(params: any[]) {
        var lsPromise: Array<Promise<any>> = [];
        params.forEach(item => {
            this.baseHttpClient.deleteRequest({
                url: `/Function/${item.FunctionId}`,
            });
        });

        return Promise.all(lsPromise);
    }
}