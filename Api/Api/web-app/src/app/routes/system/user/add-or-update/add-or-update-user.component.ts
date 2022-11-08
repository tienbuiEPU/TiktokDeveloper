import { Component, Input, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { NzDrawerRef } from 'ng-zorro-antd/drawer';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { UserRepository } from 'src/app/infrastructure/repositories/user.repository';
import { NzMessageService } from 'ng-zorro-antd/message';
import * as CryptoJS from 'crypto-js';
import { NzUploadFile, NzUploadXHRArgs } from 'ng-zorro-antd/upload';
import { NzSafeAny } from 'ng-zorro-antd/core/types';
import GetByPageModel from 'src/app/core/models/get-by-page-model';
import { NzModalService } from 'ng-zorro-antd/modal';

@Component({
  selector: 'app-add-or-update-user',
  templateUrl: './add-or-update-user.component.html'
})
export class AddOrUpdateUserComponent implements OnInit {
  @Input() record: NzSafeAny;
  @Input() positions: any[] = [];
  @Input() departments: any[] = [];
  @Input() srcImg?: string;

  nzScrollYValue = window.innerHeight - 450 + 'px';
  validateForm!: FormGroup;
  loading: boolean = false;
  fileList: NzUploadFile[] = [];
  rolesInput: any[] = [];

  constructor(
    private drawerRef: NzDrawerRef<string>,
    private fb: FormBuilder,
    private userRepository: UserRepository,
    private message: NzMessageService,
    private modalSrv: NzModalService
  ) {}

  ngOnInit(): void {
    this.getListRole();

    this.validateForm = this.fb.group({
      UserId: [this.record ? this.record.UserId : undefined, []],
      UserName: [this.record ? this.record.UserName : undefined, [Validators.required, Validators.minLength(4)]],
      Password: [this.record ? this.record.Password : undefined, !this.record ? [Validators.required, Validators.minLength(6)] : []],
      ConfirmPassword: [this.record ? this.record.Password : undefined, !this.record ? this.confirmValidator : []],
      FullName: [this.record ? this.record.FullName : undefined, [Validators.required, Validators.minLength(6)]],
      Phone: [this.record ? this.record.Phone : undefined, [Validators.required, Validators.pattern(/(84|0[3|5|7|  9])+([0-9]{8})\b/g)]],
      Email: [this.record ? this.record.Email : undefined, [Validators.email]],
      Address: [this.record ? this.record.Address : undefined, []],
      PositionId: [this.record ? this.record.PositionId : undefined, []],
      DepartmentId: [this.record ? this.record.DepartmentId : undefined, []],
      IsRoleGroup: [this.record ? this.record.DepartmentId : true, []],
      Avata: [this.record ? this.record.Avata : undefined, []],
      listRole: [this.record ? (this.record.listRole.length == 0 ? undefined : this.record.listRole) : undefined, [Validators.required]]
    });
  }

  confirmValidator = (control: FormControl): { [s: string]: boolean } => {
    if (!control.value) {
      return { error: true, required: true };
    } else if (control.value !== this.validateForm.value.Password) {
      return { confirm: true, error: true };
    }
    return {};
  };

  validateConfirmPassword(): void {
    setTimeout(() => {
      if (this.validateForm.value.PassWord) this.validateForm.value.PassWord.updateValueAndValidity();
    });
  }

  async submitForm() {
    this.loading = true;
    let data = { ...this.validateForm.value };
    data.IsRoleGroup = true;

    if (!data.UserId && data.Password) {
      data.Password = CryptoJS.MD5(data.Password).toString();
    }

    const resp = data.UserId
      ? await this.userRepository.update(data)
      : await this.userRepository.addNew(data);

    if (resp.meta?.error_code == 200) {
      this.loading = false;
      this.drawerRef.close(data);
    } else {
      this.loading = false;
    }
  }

  close(): void {
    this.drawerRef.close();
  }

  compareFn = (o1: any, o2: any) => (o1 && o2 ? o1.RoleId === o2.RoleId : o1 === o2);

  beforeUpload = (file: NzUploadFile): boolean => {
    this.handleChange(file);
    return false;
  };

  async handleChange(file: any) {
    const formData = new FormData();
    formData.append(file.name, file);

    //const resp = await this.baseRepository.uploadImage(formData);
    //this.validateForm.value.Avata = resp?.data.toString();
  }

  async getListRole() {
    //const paging: GetByPageModel = new GetByPageModel();
    //paging.select = 'RoleId,Name,Note';
    //paging.order_by = 'Name Asc';
    //paging.page_size = -1;

    //const resp = await this.baseRepository.getByPage('/role/GetByPage', paging);
    //if (resp.meta?.error_code == 200) {
    //  this.rolesInput = resp.data;
    //  this.rolesInput = resp.data.map((x: any) => {
    //    return { RoleId: x.RoleId, RoleName: x.Name, FullName: x.Name + (x.Note ? ` (${x.Note})` : ``) };
    //  });
    //} else {
    //  this.modalSrv.error({
    //    nzTitle: 'Không lấy được dữ liệu.'
    //  });
    //}
  }
}
