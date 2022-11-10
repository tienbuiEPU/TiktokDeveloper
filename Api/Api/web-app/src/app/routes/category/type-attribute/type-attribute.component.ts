import { Component, OnInit, ViewChild } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { NzModalService } from 'ng-zorro-antd/modal';
import GetByPageModel from 'src/app/core/models/get-by-page-model';
import QueryModel from 'src/app/core/models/query-model';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzDrawerService } from 'ng-zorro-antd/drawer';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { STChange, STColumn, STComponent } from '@delon/abc/st';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AddOrUpdateTypeAttributeComponent } from './add-or-update/add-or-update-type-attribute.component';
import { TypeAttributeRepository } from 'src/app/infrastructure/repositories/type-attribute.repository';

@Component({
  selector: 'app-type-attribute',
  templateUrl: './type-attribute.component.html',
})
export class TypeAttributeComponent implements OnInit {
  @ViewChild('tableRef', { static: false }) tableRef!: STComponent;

  validateForm!: FormGroup;
  paging: GetByPageModel = new GetByPageModel();
  query: QueryModel = new QueryModel();

  data: any[] = [];
  dataAll: any[] = [];
  loading = false;
  selectAll?: any[] = [];

  columns: STColumn[] = [
    { title: '', index: 'Id', type: 'checkbox' },
    { title: 'Tên loại hình', index: 'Name' },
    { title: 'Mã loại hình', index: 'Code' },
    { title: 'Cập nhật', index: 'IsUpdate', type: 'yn' },
    { title: 'Delete', index: 'IsDelete', type: 'yn' },
    { title: 'Loại hình cha', index: 'TypeAttributeParentId' },
    {
      title: 'Thao tác',
      width: 100,
      className: 'text-center',
      buttons: [
        {
          icon: 'edit',
          iif: i => !i.edit,
          click: record => this.addOrUpdate(record)
        },
        {
          icon: 'delete',
          type: 'del',
          pop: {
            title: 'Bạn có chắc chắn muốn xoá?',
            okType: 'danger',
            icon: 'star'
          },
          click: record => this.delete(record)
        }
      ]
    }
  ];

  // status = [
  //   { index: 0, text: 'Mặc định', value: false, type: 'default', checked: false },
  //   {
  //     index: 1,
  //     text: 'Đang xử lý',
  //     value: false,
  //     type: 'processing',
  //     checked: false
  //   },
  //   { index: 2, text: 'Thành công', value: false, type: 'success', checked: false },
  //   { index: 3, text: 'Xảy ra lỗi', value: false, type: 'error', checked: false }
  // ];
  constructor(
    private fb: FormBuilder,
    private modalSrv: NzModalService,
    private typeAttributeRepository: TypeAttributeRepository,
    private message: NzMessageService,
    private drawerService: NzDrawerService,
    private nzNotificationService: NzNotificationService
  ) { }

  ngOnInit(): void {
    this.validateForm = this.fb.group({});
    this.getData();
  }

  tableRefChange(e: STChange): void {
    switch (e.type) {
      case 'pi':
        this.paging.page = e.pi;
        this.getData();
        break;
      case 'dblClick':
        this.addOrUpdate(e.dblClick?.item);
        break;
      case 'checkbox':
        this.selectAll = e.checkbox;
        break;
    }
  }

  reset(): void {
    this.query = new QueryModel();
    this.paging.page = 1;
    this.getData();

  }

  searchData() {
    this.getData();
  }

  async getData() {
    this.paging.query = '1=1';
    this.paging.order_by = "CreatedAt Desc";

    if (this.query.txtSearch != undefined && this.query.txtSearch != '') {
      if (this.query.txtSearch.trim() != '')
        this.paging.query += ` and (Name.Contains("${this.query.txtSearch}")`
          + ` or Code.Contains("${this.query.txtSearch}"))`;
    }
    // if(this.query.type != undefined){
    //   this.paging.query += ` and LoaiNv=${this.query.type}`
    // }
    try {
      this.loading = true;
      const resp = await this.typeAttributeRepository.getByPage(this.paging);

      if (resp.meta?.error_code == 200) {
        this.data = resp.data;
        this.paging.item_count = resp.metadata;
      } else {
        this.modalSrv.error({
          nzTitle: 'Không lấy được dữ liệu.'
        });
      }
    } catch (error) {
      throw error;
    } finally {
      this.loading = false;
    }
  }

  addOrUpdate(record?: any): void {
    const drawerRef = this.drawerService.create<AddOrUpdateTypeAttributeComponent>({
      nzTitle: record ? `Sửa: ${record.Name}` : 'Thêm mới',
      // record.khoa_chinh
      nzWidth: '65vw',
      nzContent: AddOrUpdateTypeAttributeComponent,
      nzContentParams: {
        record,

      }
    });

    drawerRef.afterClose.subscribe((data: any) => {
      if (data) {
        let msg = data.Id ? `Sửa ${data.Name} thành công!` : `Thêm mới ${data.Name} thành công!`;
        this.message.success(msg);
        this.getData();
      }
    });
  }

  async delete(data: any) {
    const resp = await this.typeAttributeRepository.delete(data);
    if (resp.meta?.error_code == 200) {
      this.message.create('success', `Xóa ${data.Name} thành công!`);
      this.getData();
    } else {
      this.message.create('error', resp.meta?.error_message ? resp.meta?.error_message : '');
    }
  }

  async deletes() {
    // if (this.selectAll?.length) {
    //   const selectDel = this.selectAll.map(x => x.TypeAttributeId);
    //   const resp = await this.dataRepository.deletes(selectDel);
    //   if (resp.meta?.error_code == 200) {
    //     this.message.create('success', `Xóa thành công!`);
    //     this.getData();
    //   } else {
    //     this.message.create('error', resp.meta?.error_message ? resp.meta?.error_message : '');
    //   }
    // }
  }

  onBack() {
    window.history.back();
  }


}
