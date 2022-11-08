import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { NzDrawerRef } from 'ng-zorro-antd/drawer';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { STChange, STColumn, STComponent } from '@delon/abc/st';
import { NzSafeAny } from 'ng-zorro-antd/core/types';
import GetByPageModel from 'src/app/core/models/get-by-page-model';
import { NzModalService } from 'ng-zorro-antd/modal';
import { TypeAttributeRepository } from 'src/app/infrastructure/repositories/type-attribute.repository';
import QueryModel from 'src/app/core/models/query-model';

@Component({
  selector: 'app-categories-add-or-update-type-attribute-item',
  templateUrl: './add-or-update-type-attribute-item.component.html',
})
export class CategoriesAddOrUpdateTypeAttributeItemComponent implements OnInit {
    validateFormItem!: FormGroup;
    loading: boolean = false;

    @Input() record: NzSafeAny;

    constructor(
        private drawerRef: NzDrawerRef<string>, 
        private fb: FormBuilder,
        private dataRepository: TypeAttributeRepository, 
        private modalSrv: NzModalService
        ) { }

    ngOnInit(): void {
        this.validateFormItem = this.fb.group({
            TypeAttributeItemId: [this.record ? this.record.TypeAttributeItemId : undefined],
            Name: [this.record ? this.record.Name : undefined, [Validators.required]],
            Code: [this.record ?  this.record.Code :undefined, [Validators.required]],
            Location: [this.record ?  this.record.Location :undefined],
            TypeAttributeId: [this.record ?  this.record.TypeAttributeId :undefined],
            UserId: [this.record ?  this.record.UserId :undefined],
            CreatedAt: [this.record ?  this.record.CreatedAt :undefined],
            UpdatedAt: [this.record ?  this.record.UpdatedAt :undefined],
            Status: [this.record ?  this.record.Status :undefined]
        });
    }

    async submitItemForm() {
        this.loading = true;
        let data = { ...this.validateFormItem.value };
        data.Status = 1;
        this.drawerRef.close(data);
    }

    closeItem(): void {
        this.drawerRef.close();
    }

}


