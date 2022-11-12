import { Component, Input, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { NzDrawerRef } from 'ng-zorro-antd/drawer';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NzMessageService } from 'ng-zorro-antd/message';
import * as CryptoJS from 'crypto-js';
import { NzUploadFile, NzUploadXHRArgs } from 'ng-zorro-antd/upload';
import { NzSafeAny } from 'ng-zorro-antd/core/types';
import GetByPageModel from 'src/app/core/models/get-by-page-model';
import { NzModalService } from 'ng-zorro-antd/modal';
import { UserRepository } from 'src/app/infrastructure/repositories/user.repository';
import { UploadRepository } from 'src/app/infrastructure/repositories/upload.repository';

@Component({
  selector: 'app-setting-user-info',
  templateUrl: './setting-user-info.component.html'
})
export class SettingUserInfoComponent implements OnInit {
  @Input() record: NzSafeAny;
  @Input() srcImg?: string;

  validateForm!: FormGroup;
  loading: boolean = false;

  constructor(
    private drawerRef: NzDrawerRef<string>,
    private fb: FormBuilder,
    private userRepository: UserRepository,
    private uploadRepository: UploadRepository
  ) { }

  ngOnInit(): void {

    this.validateForm = this.fb.group({
      Id: [this.record ? this.record.Id : undefined, []],
      UserName: [this.record ? this.record.UserName : undefined, [Validators.required, Validators.minLength(4)]],
      FullName: [this.record ? this.record.FullName : undefined, [Validators.required, Validators.minLength(6)]],
      Phone: [this.record ? this.record.Phone : undefined, [Validators.required, Validators.pattern(/(84|0[3|5|7|  9])+([0-9]{8})\b/g)]],
      Email: [this.record ? this.record.Email : undefined, [Validators.email]],
      Address: [this.record ? this.record.Address : undefined, []],
      Avatar: [this.record ? this.record.Avata : undefined, []]
    });
  }

  async submitForm() {
    this.loading = true;
    let data = { ...this.validateForm.value };

    const resp = await this.userRepository.changeInfoUser(data);

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


  beforeUpload = (file: NzUploadFile): boolean => {
    this.handleChange(file);
    return false;
  };

  async handleChange(file: any) {
    const formData = new FormData();
    formData.append(file.name, file);

    const resp = await this.uploadRepository.uploadImage(formData);
    this.validateForm.value.Avatar = resp?.data.toString();
  }

}
