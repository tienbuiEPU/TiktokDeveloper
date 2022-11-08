import { Component, Input, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BaseRepository } from 'src/app/infrastructure/repositories/base.repository';
import { NzMessageService } from 'ng-zorro-antd/message';
import * as CryptoJS from 'crypto-js';
import { NzUploadFile, NzUploadXHRArgs } from 'ng-zorro-antd/upload';
import { NzSafeAny } from 'ng-zorro-antd/core/types';
import GetByPageModel from 'src/app/core/models/get-by-page-model';
import { NzModalService, NzModalRef } from 'ng-zorro-antd/modal';

@Component({
  selector: 'app-change-pass-user',
  template: `
    <div>
      <form nz-form [formGroup]="validateForm" (ngSubmit)="submitForm()">
        <nz-form-item>
          <nz-form-label [nzSpan]="10" nzFor="Password" nzRequired>Mật khẩu</nz-form-label>
          <nz-form-control [nzSpan]="14" nzHasFeedback [nzErrorTip]="passwordErrorTpl">
            <input nz-input autocomplete="off" type="password" formControlName="Password" (ngModelChange)="validateConfirmPassword()" />
            <ng-template #passwordErrorTpl let-control>
              <ng-container *ngIf="control.hasError('required')">Chưa nhập Mật khẩu!</ng-container>
              <ng-container *ngIf="control.hasError('minlength')">Mật khẩu từ 6 ký tự trở lên!</ng-container>
            </ng-template>
          </nz-form-control>
        </nz-form-item>
        <nz-form-item>
          <nz-form-label [nzSpan]="10" nzFor="ConfirmPassword" nzRequired>Xác nhận mật khẩu</nz-form-label>
          <nz-form-control [nzSpan]="14" nzHasFeedback [nzErrorTip]="passwordConfirmErrorTpl">
            <input nz-input type="password" formControlName="ConfirmPassword" />
            <ng-template #passwordConfirmErrorTpl let-control>
              <ng-container *ngIf="control.hasError('required')">Chưa nhập Mật khẩu xác nhận!</ng-container>
              <ng-container *ngIf="control.hasError('confirm')">Mật khẩu xác nhận không khớp!</ng-container>
            </ng-template>
          </nz-form-control>
        </nz-form-item>
      </form>
    </div>
    <div *nzModalFooter>
      <button nz-button nzType="default" (click)="close()">Hủy</button>
      <button nz-button nzType="primary" [disabled]="!validateForm.valid || loading" (click)="submitForm()">Lưu</button>
    </div>
  `
})
export class ChangePassUserComponent implements OnInit {
  @Input() record: NzSafeAny;

  validateForm!: FormGroup;
  loading: boolean = false;

  constructor(
    private fb: FormBuilder,
    private baseRepository: BaseRepository,
    private message: NzMessageService,
    private modalSrv: NzModalService,
    private modal: NzModalRef
  ) { }

  ngOnInit(): void {
    this.validateForm = this.fb.group({
      UserId: [this.record ? this.record.UserId : undefined, []],
      Password: [undefined, [Validators.required, Validators.minLength(6)]],
      ConfirmPassword: [undefined, this.confirmValidator]
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
    let data = { ...this.validateForm.value };

    if (data.Password) {
      data.PasswordNew = CryptoJS.MD5(data.Password).toString();
    }

    delete data['Password'];
    delete data['ConfirmPassword'];

    try {
      this.loading = true;
      const resp = await this.baseRepository.update('/User/adminChangePass/' + data.UserId, data);

      if (resp.meta?.error_code == 200) {
        this.modal.triggerOk();
      }
    } catch (error) {
      throw error;
    } finally {
      this.loading = false;
    }
  }

  close(): void {
    this.modal.close();
  }
}
