import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { NzDrawerRef } from 'ng-zorro-antd/drawer';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { STChange, STColumn, STComponent, STData } from '@delon/abc/st';
import { NzSafeAny } from 'ng-zorro-antd/core/types';
import { NzDrawerService } from 'ng-zorro-antd/drawer';
import { NzMessageService } from 'ng-zorro-antd/message';
import { TypeAttributeRepository } from 'src/app/infrastructure/repositories/type-attribute.repository';
import { AddOrUpdateTypeAttributeItemComponent } from 'src/app/routes/category/type-attribute-item/add-or-update/add-or-update-type-attribute-item.component';
import { _ } from 'ajv';

@Component({
  selector: 'app-categories-add-or-update-type-attribute',
  templateUrl: './add-or-update-type-attribute.component.html'
})
export class AddOrUpdateTypeAttributeComponent implements OnInit {
  @ViewChild('tableItemRef') private tableItemRef!: STComponent;
  validateForm!: FormGroup;
  loading: boolean = false;

  @Input() record: NzSafeAny;
  typeAttributeItems: any;
  invalidTypeAttributeItem = true;

  constructor(
    private drawerRef: NzDrawerRef<string>,
    private fb: FormBuilder,
    private dataRepository: TypeAttributeRepository,
    private drawerService: NzDrawerService,
    private message: NzMessageService // private modalSrv: NzModalService
  ) {}

  ngOnInit(): void {
    this.validateForm = this.fb.group({
      Id: [this.record ? this.record.Id : undefined],
      Code: [this.record ? this.record.Code : undefined, [Validators.required]],
      Name: [this.record ? this.record.Name : undefined, [Validators.required]],
      Note: [this.record ? this.record.Note : undefined, []],
      typeAttributeItems: [
        this.record
          ? this.record.typeAttributeItems.map((item: any, index: number) => {
              item.index = index + 1;
              return item;
            })
          : [],
        []
      ]
    });

    this.typeAttributeItems = [...this.validateForm.value.typeAttributeItems];
    this.invalidTypeAttributeItem = this.validateForm.value.Id ? false : true;
  }

  async submitForm() {
    this.loading = true;
    let data = { ...this.validateForm.value };

    //T??nh to??n, thay ?????i d??? li???u trc khi ?????y v??o api
    data.typeAttributeItems = this.tableItemRef._data;

    const resp = data.Id ? await this.dataRepository.update(data) : await this.dataRepository.addNew(data);
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

  listItem: any[] = [];

  columnsItem: STColumn[] = [
    { title: 'Stt', type: 'no', width: 40 },
    { renderTitle: 'codeTitle', render: 'codeTpl' },
    { renderTitle: 'nameTitle', render: 'nameTpl' },
    { title: 'V??? tr?? s???p x???p', render: 'locationTpl', className: 'text-center' },
    { title: '???????c s??? d???ng', render: 'activeTpl', className: 'text-center' },
    // { title: 'Ghi ch??', render: 'noteTpl' },
    {
      title: 'Thao t??c',
      width: 100,
      className: 'text-center',
      buttons: [
        {
          icon: 'edit',
          iif: i => !i.edit,
          click: record => this.updateTypeAttributeItem(record, true)
        },
        {
          icon: 'delete',
          iif: i => !i.edit,
          type: 'del',
          pop: {
            title: 'B???n c?? ch???c ch???n mu???n xo?? thu???c t??nh n??y?',
            okType: 'danger',
            icon: 'star'
          },
          click: record => this.deleteItem(record)
        },
        {
          text: `L??u`,
          iif: i => i.edit,
          type: 'link',
          click: record => {
            this.submit(record);
          }
        },
        {
          text: `H???y`,
          iif: i => i.edit,
          click: record => this.cancelUpdateTypeAttributeItem(record, false)
        }
      ]
    }
  ];

  // openAddTypeAttributeItem(record?: any): void {
  //   let index = !record ? undefined : this.listItem.findIndex(record);

  //   console.log(index);
  //   // let record = index ? this.listItem[index] : undefined;
  //   const drawerItemRef = this.drawerService.create<AddOrUpdateTypeAttributeItemComponent>({
  //     nzTitle: record ? `S???a thu???c t??nh: ${record.Name}` : `Th??m m???i thu???c t??nh`,
  //     // record.khoa_chinh
  //     nzWidth: '45vw',
  //     nzContent: AddOrUpdateTypeAttributeItemComponent,
  //     nzContentParams: {
  //       record
  //     }
  //   });

  //   drawerItemRef.afterClose.subscribe((item: any) => {
  //     //debugger
  //     if (item) {
  //       if (index == null) {
  //         Object.keys(item).forEach(key => {
  //           if (item[key] === null) {
  //             delete item[key];
  //           }
  //         });

  //         this.listItem = [...this.listItem, item];
  //       } else {
  //         Object.keys(item).forEach(key => {
  //           if (item[key] === null) {
  //             delete item[key];
  //           }
  //         });

  //         let newListItems = [...this.listItem];
  //         newListItems[index] = item;
  //         this.listItem = [...newListItems];
  //       }

  //       let msg = index ? `S???a thu???c t??nh "${item.Name}" th??nh c??ng!` : `Th??m m???i thu???c t??nh "${item.Name}" th??nh c??ng!`;
  //       this.message.success(msg);
  //     }
  //   });
  // }

  tableItemRefChange(e: STChange): void {
    switch (e.type) {
      case 'pi':
        break;
      case 'dblClick':
        // this.openAddTypeAttributeItem(undefined);
        break;
    }
  }

  async deleteItem(i: STData) {
    this.tableItemRef.removeRow(i);
    this.message.create('success', `X??a thu???c t??nh ${i['Name']} th??nh c??ng!`);

    this.checkTypeAttributeItemsIsValid();
  }

  private updateTypeAttributeItem(i: STData, edit: boolean): void {
    this.tableItemRef.setRow(i, { edit }, { refreshSchema: true });

    this.checkTypeAttributeItemsIsValid();
  }

  private cancelUpdateTypeAttributeItem(i: STData, edit: boolean): void {
    let item = this.typeAttributeItems.find((x: any) => x.index == i['index']);

    if (!item['Code'] || !item['Name']) {
      this.typeAttributeItems = this.typeAttributeItems.filter((x: any) => x != item);
      this.tableItemRef.removeRow(i);
    } else {
      item.edit = false;
      this.tableItemRef.setRow(i, Object.assign({}, item), { refreshSchema: true });
    }

    this.checkTypeAttributeItemsIsValid();
  }

  private submit(i: STData): void {
    if (!i['Code'] || !i['Name']) {
      this.tableItemRef.setRow(i, { submit: true }, { refreshSchema: true });
    } else {
      this.typeAttributeItems = this.typeAttributeItems.map((item: any) => {
        if (item.index == i['index']) {
          return Object.assign({}, i);
        } else return item;
      });

      this.updateTypeAttributeItem(i, false);
      this.message.success(`L??u th??ng tin thu???c t??nh ${i['Name']} th??nh c??ng!`);
    }

    this.checkTypeAttributeItemsIsValid();
  }

  addRow() {
    let row = {
      Id: undefined,
      Code: undefined,
      Name: undefined,
      Location: undefined,
      IsActive: true,
      Note: undefined,
      edit: true,
      index: this.typeAttributeItems.length + 1
    };

    this.tableItemRef.addRow(row);
    this.typeAttributeItems.push(Object.assign({}, row));
    this.checkTypeAttributeItemsIsValid();
  }

  checkTypeAttributeItemsIsValid() {
    if (this.tableItemRef._data.length == 0) this.invalidTypeAttributeItem = true;
    else {
      let isValid = this.tableItemRef._data.filter(x => x['edit'] == true);
      this.invalidTypeAttributeItem = isValid.length > 0 ? true : false;
    }
  }
}
