import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { NzModalRef, NzModalService } from 'ng-zorro-antd/modal';
import GetByPageModel from 'src/app/core/models/get-by-page-model';
import QueryModel from 'src/app/core/models/query-model';
import { UserRepository } from 'src/app/infrastructure/repositories/user.repository';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzDrawerRef, NzDrawerService } from 'ng-zorro-antd/drawer';
import { AddOrUpdateUserComponent } from './add-or-update/add-or-update-user.component';
import gatewayConfig from 'src/app/infrastructure/http/api-gateway-config';
import { STChange, STColumn, STComponent } from '@delon/abc/st';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { ChangePassUserComponent } from './change-pass/change-pass-user.component';
import { EntityStatus } from 'src/app/shared/utils/enums';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html'
})
export class UserComponent implements OnInit {
  @ViewChild('tableRef', { static: false }) tableRef!: STComponent;

  paging: GetByPageModel = new GetByPageModel();
  query: QueryModel = new QueryModel();

  data: any[] = [];
  dataAll: any[] = [];
  loading = false;
  selectAll?: any[] = [];
  srcImg = gatewayConfig.domain_thumb_img;

  columns: STColumn[] = [
    // { title: '', index: 'Id', type: 'checkbox' },
    { title: 'Stt', type: 'no', width: 40 },
    { title: 'Avatar', index: 'Avatar', type: 'img', width: 60 },
    { title: 'Tên người dùng', index: 'FullName', sort: true },
    { title: 'Tài khoản', index: 'UserName', sort: true },
    { title: 'Số điện thoại', index: 'Phone', sort: true },
    { title: 'Email', index: 'Email', sort: true },
    { title: 'Địa chỉ', index: 'Address' },
    { title: 'Thời gian tạo', index: 'CreatedAt', type: 'date', width: 120, sort: true },
    { title: 'Thời gian sửa', index: 'UpdatedAt', type: 'date', width: 120 },
    {
      title: 'Thao tác',
      width: 140,
      className: 'text-center',
      buttons: [
        {
          icon: 'lock',
          tooltip: 'Khóa tài khoản',
          iif: i => i.Status == 1,
          type: 'del',
          pop: {
            title: 'Bạn có chắc chắn muốn khóa tài khoản này?',
            okType: 'danger',
            icon: 'lock'
          },
          click: record => this.changeStatus(record, EntityStatus.LOCK)
        },
        {
          icon: 'unlock',
          tooltip: 'Mở khóa tài khoản',
          iif: i => i.Status == 98,
          type: 'del',
          pop: {
            title: 'Bạn có chắc chắn muốn mở khóa tài khoản này?',
            okType: 'danger',
            icon: 'unlock'
          },
          click: record => this.changeStatus(record, EntityStatus.NORMAL)
        },
        {
          icon: 'key',
          tooltip: 'Đổi mật khẩu',
          iif: i => !i.edit,
          click: record => this.adminChangePass(record)
        },
        {
          icon: 'edit',
          iif: i => !i.edit,
          tooltip: 'Sửa',
          click: record => this.addOrUpdate(record)
        },
        {
          icon: 'delete',
          tooltip: 'Xóa',
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

  constructor(
    private modalSrv: NzModalService,
    private userRepository: UserRepository,
    private message: NzMessageService,
    private drawerService: NzDrawerService,
    private nzNotificationService: NzNotificationService
  ) { }

  ngOnInit(): void {
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
      case 'sort':
        this.paging.order_by = e.sort?.value ? `${e.sort?.column?.index?.toString()} ${e.sort?.value}` : new GetByPageModel().order_by;
        this.getData();
        break;
      default:
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

  async getData(param?: any) {
    this.paging.query = '1=1';

    if (this.query.txtSearch != undefined && this.query.txtSearch != '') {
      if (this.query.txtSearch.trim() != '')
        this.paging.query += ` and (UserName.Contains("${this.query.txtSearch}") or FullName.Contains("${this.query.txtSearch}") or Phone.Contains("${this.query.txtSearch}") or Email.Contains("${this.query.txtSearch}"))`;
    }

    try {
      this.loading = true;

      const resp = await this.userRepository.getByPage(this.paging);

      if (resp.meta?.error_code == 200) {
        this.data = resp.data.map((dataItem: any, index: number) => {
          if (dataItem.Avata) dataItem.Avatar = this.srcImg.concat(dataItem.Avata);
          return dataItem;
        });
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
    const drawerRef = this.drawerService.create<AddOrUpdateUserComponent>({
      nzTitle: record ? `Sửa thông tin tài khoản ${record.UserName}` : 'Thêm mới tài khoản',
      nzWidth: '65vw',
      nzContent: AddOrUpdateUserComponent,
      nzContentParams: {
        record,
        positions: [],
        departments: [],
        srcImg: this.srcImg
      }
    });

    drawerRef.afterClose.subscribe((data: any) => {
      if (data) {
        let msg = data.UserId ? `Sửa tài khoản ${data.UserName} thành công!` : `Thêm mới tài khoản ${data.UserName} thành công!`;
        this.message.create('success', msg);
        this.getData();
      }
    });
  }

  async delete(data: any) {
    const resp = await this.userRepository.delete(data);
    if (resp.meta?.error_code == 200) {
      this.message.create('success', `Xóa tài khoản ${data.FullName} thành công!`);
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

  async changeStatus(record: any, status: number) {
    let typeLock = status == 1 ? 'Mở khóa' : 'Khóa';
    const resp = await this.userRepository.changeStatus(record, status);
    if (resp.meta?.error_code == 200) {
      this.message.create('success', `${typeLock} tài khoản ${record.FullName} thành công!`);
      this.getData();
    }
  }

  adminChangePass(record: any) {
    this.modalSrv.create({
      nzTitle: `Thay đổi mật khẩu tài khoản "${record.FullName}"`,
      nzContent: ChangePassUserComponent,
      nzComponentParams: {
        record: record
      },
      nzOnOk: (res: any) => {
        this.message.create('success', `Thay đổi mật khẩu tài khoản ${record.FullName} thành công!`);
      }
    });
  }
}
