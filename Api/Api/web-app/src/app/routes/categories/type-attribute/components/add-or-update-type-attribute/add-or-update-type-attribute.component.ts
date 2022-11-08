import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { NzDrawerRef } from 'ng-zorro-antd/drawer';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { STChange, STColumn, STComponent } from '@delon/abc/st';
import { NzSafeAny } from 'ng-zorro-antd/core/types';
import { NzDrawerService } from 'ng-zorro-antd/drawer';
import { NzMessageService } from 'ng-zorro-antd/message';
import GetByPageModel from 'src/app/core/models/get-by-page-model';
import { NzModalService } from 'ng-zorro-antd/modal';
import { TypeAttributeRepository } from 'src/app/infrastructure/repositories/type-attribute.repository';
import QueryModel from 'src/app/core/models/query-model';
import { CategoriesAddOrUpdateTypeAttributeItemComponent } from '../add-or-update-type-attribute-item/add-or-update-type-attribute-item.component';

@Component({
  selector: 'app-categories-add-or-update-type-attribute',
  templateUrl: './add-or-update-type-attribute.component.html',
})
export class CategoriesAddOrUpdateTypeAttributeComponent implements OnInit {
  validateForm!: FormGroup;
  loading: boolean = false;

  @Input() record: NzSafeAny;

  constructor(
    private drawerRef: NzDrawerRef<string>,
    private fb: FormBuilder,
    private dataRepository: TypeAttributeRepository,
    private drawerService: NzDrawerService,
    private message: NzMessageService,
    // private modalSrv: NzModalService
  ) { }

  ngOnInit(): void {
    console.log(this.record);
    this.validateForm = this.fb.group({
      TypeAttributeId: [this.record ? this.record.TypeAttributeId : undefined],
      Name: [this.record ? this.record.Name : undefined, [Validators.required]],
      Code: [this.record ? this.record.Code : undefined, [Validators.required]],
      IsUpdate: [this.record ? this.record.IsUpdate : undefined],
      IsDelete: [this.record ? this.record.IsDelete : undefined],
      TypeAttributeParentId: [this.record ? this.record.TypeAttributeParentId : undefined],
      UserId: [this.record ? this.record.UserId : undefined],
      CreatedAt: [this.record ? this.record.CreatedAt : undefined],
      UpdatedAt: [this.record ? this.record.UpdatedAt : undefined],
      Status: [this.record ? this.record.Status : undefined]
    });
    this.listItem = this.record ? this.record.listAttributeItem : [];
  }

  async submitForm() {
    this.loading = true;
    let data = { ...this.validateForm.value };
    //Tính toán, thay đổi dữ liệu trc khi đẩy vào api
    data.listAttributeItem = this.listItem;

    const resp = data.TypeAttributeId ? await this.dataRepository.update(data) : await this.dataRepository.addNew(data);
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

  // 
  //@ViewChild('tableItemRef', { static: false }) tableItemRef!: STComponent;
  //item: any[] = [];
  listItem: any[] = [];
  listItem2: any[] = [];

  columnsItem: STColumn[] = [
    { title: 'Mã', index: 'Code' },
    { title: 'Tên thuộc tính', index: 'Name' },
    { title: 'Số thứ tự', index: 'Location' },
    {
      title: 'Thao tác',
      width: 100,
      className: 'text-center',
      buttons: [
        {
          icon: 'edit',
          iif: i => !i.edit,
          click: record => this.openAddTypeAttributeItem(record)
        },
        {
          icon: 'delete',
          type: 'del',
          pop: {
            title: 'Bạn có chắc chắn muốn xoá?',
            okType: 'danger',
            icon: 'star'
          },
          click: record => this.deleteItem(record)
        }
      ]
    }
  ];

  openAddTypeAttributeItem(record?: any): void {
    const drawerItemRef = this.drawerService.create<CategoriesAddOrUpdateTypeAttributeItemComponent>({
      nzTitle: record ? `Sửa` : `Thêm mới`,
      // record.khoa_chinh
      nzWidth: '45vw',
      nzContent: CategoriesAddOrUpdateTypeAttributeItemComponent,
      nzContentParams: {
        record
      }
    });

    drawerItemRef.afterClose.subscribe(item => {
      //debugger
      if (item) {
        if (item.TypeAttributeItemId === null) {
          Object.keys(item).forEach(key => {
            if (item[key] === null) {
              delete item[key];
            }
          });

          this.listItem = [...this.listItem, item];
        }
        else {
          Object.keys(item).forEach(key => {
            if (item[key] === null) {
              delete item[key];
            }
          });
          let newListItems = [...this.listItem];
          let existItem = this.listItem.find(el => el.TypeAttributeItemId === item.TypeAttributeItemId);
          let index = newListItems.indexOf(existItem);
          newListItems[index] = item;
          this.listItem = newListItems;
        }
        let msg = item.TypeAttributeItemId ? `Sửa "${item.Name}" thành công!` : `Thêm mới "${item.Name}" thành công!`;
        this.message.success(msg);
        //console.log(this.listItem);
      }
    });
  }

  tableItemRefChange(e: STChange): void {
    switch (e.type) {
      case 'pi':
        break;
      case 'dblClick':
        this.openAddTypeAttributeItem();
        break;
    }
  }

  async deleteItem(data: any) {
    //console.log(this.selectAll);
    if (this.listItem?.length) {
      // const selectDel = this.listItem.map(x => x.Id);
      // const resp = await this.dataRepository.deletes(selectDel);
      // if (resp.meta?.error_code == 200) {
      //data.Status = 99;
      let newListItems = [...this.listItem];
      let existItem = this.listItem.find(el => el.TypeAttributeItemId === data.TypeAttributeItemId);
      let index = newListItems.indexOf(existItem);
      newListItems.splice(index, 1);
      this.listItem = newListItems;
      this.message.create('success', `Xóa thành công!`);
      // this.getData();
      // } else {
      //   this.message.create('error', resp.meta?.error_message ? resp.meta?.error_message : '');
      // }
    }
  }

}