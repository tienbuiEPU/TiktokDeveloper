import { NgModule, Type } from '@angular/core';
import { SharedModule } from '@shared';

import { CategoriesRoutingModule } from './category-routing.module';
import { NzPageHeaderModule } from 'ng-zorro-antd/page-header';
import { NzTreeSelectModule } from 'ng-zorro-antd/tree-select';
import { NzUploadModule } from 'ng-zorro-antd/upload';
import { DatePipe } from '@angular/common';

import { CategoriesTypeAttributeComponent } from './type-attribute/type-attribute.component';
import { AddOrUpdateTypeAttributeComponent } from './type-attribute/add-or-update/add-or-update-type-attribute.component';
import { AddOrUpdateTypeAttributeItemComponent } from './type-attribute-item/add-or-update/add-or-update-type-attribute-item.component';

const COMPONENTS: Array<Type<void>> = [
  CategoriesTypeAttributeComponent,
  AddOrUpdateTypeAttributeComponent,
  AddOrUpdateTypeAttributeItemComponent
];


@NgModule({
  imports: [
    SharedModule,
    CategoriesRoutingModule,
    NzPageHeaderModule,
    NzTreeSelectModule,
    NzUploadModule
  ],
  declarations: COMPONENTS,
  providers: [DatePipe]
})
export class CategoriesModule { }
