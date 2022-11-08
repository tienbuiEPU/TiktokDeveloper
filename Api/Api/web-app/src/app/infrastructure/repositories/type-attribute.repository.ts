import { Injectable } from '@angular/core';
import { SettingsService } from '@delon/theme';
import GetByPageModel from 'src/app/core/models/get-by-page-model';
import { BaseHttpClient } from '../http/base-http-client';
import { objectToQueryString } from '../utils/object-to-query-string';

@Injectable({
    providedIn: 'root',
})
export class TypeAttributeRepository {
    public user: any;
    constructor(private baseHttpClient: BaseHttpClient, private settingsService: SettingsService) {
        this.user = this.settingsService.user;
    }

    public urlBase = "/typeAttribute";
    public urlBaseItem = "/typeAttributeItem";
    public getByPage(params: GetByPageModel) {
        return this.baseHttpClient.getRequest({
            url: this.urlBase + "/GetByPage" + objectToQueryString(params),
        });
    }

    public addNew(param: any) {
        param.UserId = this.user.userId;
        Object.keys(param).forEach(key => {
            if (param[key] === null) {
                delete param[key];
            }
        });
        return this.baseHttpClient.postRequest({
            url: this.urlBase,
            body: param
        });
    }

    public async update(param: any) {
        let url = this.urlBase + "/" + param.TypeAttributeId;
        param.UserId = this.user.userId;
        Object.keys(param).forEach(key => {
            if (param[key] === null) {
                delete param[key];
            }
        });
        return this.baseHttpClient.putRequest({
            url: url,
            body: param
        });
    }

    public delete(id: any) {
        let url = this.urlBase + "/" + id;
        return this.baseHttpClient.deleteRequest({
            url: url
        });
    }

    public deletes(params: any[]) {
        return this.baseHttpClient.putRequest({
            url: this.urlBase + "/deletes",
            body: params
        });
    }

    public getByPageNGHE_NGHIEP(params: GetByPageModel) {
        return this.baseHttpClient.getRequest({
            url: this.urlBaseItem + "/GetByPageFollowCode/NGHE_NGHIEP" + objectToQueryString(params),
        });
    }
    public getByPageQUOC_GIA(params: GetByPageModel) {
        return this.baseHttpClient.getRequest({
            url: this.urlBaseItem + "/GetByPageFollowCode/QUOC_GIA" + objectToQueryString(params),
        });
    }
    public getByPageDAN_TOC(params: GetByPageModel) {
        return this.baseHttpClient.getRequest({
            url: this.urlBaseItem + "/GetByPageFollowCode/DAN_TOC" + objectToQueryString(params),
        });
    }
    public getByPageCHUC_VU(params: GetByPageModel) {
        return this.baseHttpClient.getRequest({
            url: this.urlBaseItem + "/GetByPageFollowCode/CHUC_VU" + objectToQueryString(params),
        });
    }
    public getByPageCHUC_DANH(params: GetByPageModel) {
        return this.baseHttpClient.getRequest({
            url: this.urlBaseItem + "/GetByPageFollowCode/CHUC_DANH" + objectToQueryString(params),
        });
    }
    public getByPageMA_LOAI_KCB(params: GetByPageModel) {
        return this.baseHttpClient.getRequest({
            url: this.urlBaseItem + "/GetByPageFollowCode/MA_LOAI_KCB" + objectToQueryString(params),
        });
    }
    public getByPageTAI_NAN(params: GetByPageModel) {
        return this.baseHttpClient.getRequest({
            url: this.urlBaseItem + "/GetByPageFollowCode/TAI_NAN" + objectToQueryString(params),
        });
    }
    public getByPageDOI_TUONG(params: GetByPageModel) {
        return this.baseHttpClient.getRequest({
            url: this.urlBaseItem + "/GetByPageFollowCode/DOI_TUONG" + objectToQueryString(params),
        });
    }
    public getByPageNHOM_CHI_PHI(params: GetByPageModel) {
        return this.baseHttpClient.getRequest({
            url: this.urlBaseItem + "/GetByPageFollowCode/NHOM_CHI_PHI" + objectToQueryString(params),
        });
    }
    public getByPageKET_QUA_DTRI(params: GetByPageModel) {
        return this.baseHttpClient.getRequest({
            url: this.urlBaseItem + "/GetByPageFollowCode/KET_QUA_DTRI" + objectToQueryString(params),
        });
    }
    public getByPageTINH_TRANG_RV(params: GetByPageModel) {
        return this.baseHttpClient.getRequest({
            url: this.urlBaseItem + "/GetByPageFollowCode/TINH_TRANG_RV" + objectToQueryString(params),
        });
    }
    public getByPageDOI_TUONG_TAI_NAN(params: GetByPageModel) {
        return this.baseHttpClient.getRequest({
            url: this.urlBaseItem + "/GetByPageFollowCode/DOI_TUONG_TAI_NAN" + objectToQueryString(params),
        });
    }
    public getByPagePHUONG_TIEN(params: GetByPageModel) {
        return this.baseHttpClient.getRequest({
            url: this.urlBaseItem + "/GetByPageFollowCode/PHUONG_TIEN" + objectToQueryString(params),
        });
    }
    public getByPageLOAI_TAI_NAN(params: GetByPageModel) {
        return this.baseHttpClient.getRequest({
            url: this.urlBaseItem + "/GetByPageFollowCode/LOAI_TAI_NAN" + objectToQueryString(params),
        });
    }
    public getByPageDUONG_DUNG(params: GetByPageModel) {
        return this.baseHttpClient.getRequest({
            url: this.urlBaseItem + "/GetByPageFollowCode/DUONG_DUNG" + objectToQueryString(params),
        });
    }
    public getByPageLOAI_DICH_VU(params: GetByPageModel) {
        return this.baseHttpClient.getRequest({
            url: this.urlBaseItem + "/GetByPageFollowCode/LOAI_DICH_VU" + objectToQueryString(params),
        });
    }



}