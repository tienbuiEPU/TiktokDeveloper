import { Component, Input, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { NzDrawerRef } from 'ng-zorro-antd/drawer';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BaseRepository } from 'src/app/infrastructure/repositories/base.repository';
import { dataToTreeNode } from 'src/app/shared/utils/data-to-tree-node';
import { NzSafeAny } from 'ng-zorro-antd/core/types';
import GetByPageModel from 'src/app/core/models/get-by-page-model';
import { NzModalService } from 'ng-zorro-antd/modal';

@Component({
    selector: 'app-add-or-update-function',
    templateUrl: './add-or-update-function.component.html'
})

export class AddOrUpdateFunctionComponent implements OnInit {
    validateForm!: FormGroup;
    loading: boolean = false;
    dataAll: any[] = [];
    parentId?: number;

    @Input() record: NzSafeAny;

    constructor(private drawerRef: NzDrawerRef<string>, private fb: FormBuilder,
        private baseRepository: BaseRepository, private modalSrv: NzModalService) { }

    ngOnInit(): void {
        this.getAllData();

        this.validateForm = this.fb.group({
            FunctionId: [this.record ? this.record.FunctionId : undefined],
            Code: [this.record ? this.record.Code : undefined, [Validators.required]],
            Name: [this.record ? this.record.Name : undefined, [Validators.required]],
            Url: [this.record ? this.record.Url : undefined, [Validators.required]],
            Location: [this.record ? this.record.Location : undefined, [Validators.min(1)]],
            Icon: [this.record ? this.record.Icon : undefined],
            IsFunctionSpecial: [this.record ? this.record.IsFunctionSpecial : undefined],
            Note: [this.record ? this.record.Note : undefined],
            FunctionParentId: [this.record ? this.record.FunctionParentId : undefined],
            UserId: []
        });

    }


    async getAllData() {
        const paging: GetByPageModel = new GetByPageModel();
        paging.select = 'FunctionId, Name, FunctionParentId';
        paging.order_by = 'Name Asc';
        paging.page_size = -1;

        const resp = await this.baseRepository.getByPage('/Function/GetByPage', paging);
        if (resp.meta?.error_code == 200) {
            this.dataAll = dataToTreeNode(resp.data, "FunctionId", "Name", "FunctionParentId", undefined, 0, false);
        } else {
            this.modalSrv.error({
                nzTitle: 'Không lấy được dữ liệu.'
            });
        }
    }

    async submitForm() {
        this.loading = true;
        let data = { ...this.validateForm.value };
        data.FunctionParentId = data.FunctionParentId ? data.FunctionParentId : 0;

        const resp = data.FunctionId ? await this.baseRepository.update('/Function/' + data.FunctionId, data) : await this.baseRepository.addNew('/Function', data);
        if (resp.meta?.error_code == 200) {
            this.loading = false;
            this.drawerRef.close(data);
        }
        else {
            this.loading = false;
        }
    }

    close(): void {
        this.drawerRef.close();
    }
}