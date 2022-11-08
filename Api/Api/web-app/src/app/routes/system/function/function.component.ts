import { Component, OnInit, ViewChild } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { NzModalService } from 'ng-zorro-antd/modal';
import GetByPageModel from 'src/app/core/models/get-by-page-model';
import QueryModel from 'src/app/core/models/query-model';
import { BaseRepository } from 'src/app/infrastructure/repositories/base.repository';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzDrawerService } from 'ng-zorro-antd/drawer';
import { AddOrUpdateFunctionComponent } from './components/add-or-update-function.component';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { STChange, STColumn, STComponent } from '@delon/abc/st';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-function',
  templateUrl: './function.component.html'
})
export class FunctionComponent implements OnInit {
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
    { title: 'STT', type: 'no', width: 40 },
    { title: 'Mã chức năng', index: 'Code' },
    { title: 'Tên chức năng', index: 'Name', width: '22%' },
    { title: 'Url', index: 'Url' },
    { title: 'Thứ tự', index: 'Location', },
    { title: 'Icon', index: 'Icon' },
    { title: 'Chức năng cha', index: 'functionParent.Name' },
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

  status = [
    { index: 0, text: 'Mặc định', value: false, type: 'default', checked: false },
    {
      index: 1,
      text: 'Đang xử lý',
      value: false,
      type: 'processing',
      checked: false
    },
    { index: 2, text: 'Thành công', value: false, type: 'success', checked: false },
    { index: 3, text: 'Xảy ra lỗi', value: false, type: 'error', checked: false }
  ];
  constructor(
    private fb: FormBuilder,
    private modalSrv: NzModalService,
    private baseRepository: BaseRepository,
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
    // this.query.status
    this.getData();
  }

  async getData() {
    this.paging.query = '1=1';

    if (this.query.txtSearch != undefined && this.query.txtSearch != '') {
      if (this.query.txtSearch.trim() != '')
        this.paging.query += ` and (Code.Contains("${this.query.txtSearch}") or Name.Contains("${this.query.txtSearch}"))`;
    }

    try {
      this.loading = true;
      const resp = await this.baseRepository.getByPage('/Function/GetByPage', this.paging);

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
    const drawerRef = this.drawerService.create<AddOrUpdateFunctionComponent>({
      nzTitle: record ? `Sửa thông tin chức năng: ${record.Name}` : 'Thêm mới chức năng',
      nzWidth: '65vw',
      nzContent: AddOrUpdateFunctionComponent,
      nzContentParams: { record }
    });

    drawerRef.afterClose.subscribe(data => {
      if (data) {
        let msg = data.FunctionId ? `Sửa chức năng ${data.Name} thành công!` : `Thêm mới chức năng ${data.Name} thành công!`;
        this.message.success(msg);
        this.getData();
      }
    });
  }

  async delete(data: any) {
    const resp = await this.baseRepository.delete('/Function/' + data.FunctionId);
    if (resp.meta?.error_code == 200) {
      this.message.create('success', `Xóa chức năng ${data.Name} thành công!`);
      this.getData();
    } else {
      this.message.create('error', resp.meta?.error_message ? resp.meta?.error_message : '');
    }
  }

  deletes() {
    console.log(this.selectAll);
    if (this.selectAll?.length) {
    }
    this.nzNotificationService.warning('Thông báo', 'Chưa có api xóa nhiều!');
  }

  onBack() {
    window.history.back();
  }
}
