import { NgModule, Type } from '@angular/core';
import { SharedModule } from '@shared';

import { CategoriesRoutingModule } from './categories-routing.module';
import { NzPageHeaderModule } from 'ng-zorro-antd/page-header';
import { NzTreeSelectModule } from 'ng-zorro-antd/tree-select';
import { NzUploadModule } from 'ng-zorro-antd/upload';
import { DatePipe } from '@angular/common';

import { CategoriesDepartmentComponent } from './department/department.component';
import { CategoriesAddOrUpdateDepartmentComponent } from './department/components/add-or-update-department/add-or-update-department.component';
import { CategoriesAddOrUpdateEmployeeComponent } from './employee/components/add-or-update-employee/add-or-update-employee.component';
import { CategoriesEmployeeComponent } from './employee/employee.component';
import { CategoriesAddOrUpdateHospitalRegisterComponent } from './hospital-register/components/add-or-update-hospital-register/add-or-update-hospital-register.component';
import { CategoriesGroupCategoryComponent } from './group-category/group-category.component';
import { CategoriesAddOrUpdateGroupCategoryComponent } from './group-category/components/add-or-update-group-category/add-or-update-group-category.component';
import { CategoriesTypeAttributeComponent } from './type-attribute/type-attribute.component';
import { CategoriesAddOrUpdateTypeAttributeComponent } from './type-attribute/components/add-or-update-type-attribute/add-or-update-type-attribute.component';
import { CategoriesAddOrUpdateTypeAttributeItemComponent } from './type-attribute/components/add-or-update-type-attribute-item/add-or-update-type-attribute-item.component';
import { CategoriesEquipmentComponent } from './equipment/equipment.component';
import { CategoriesAddOrUpdateEquipmentComponent } from './equipment/components/add-or-update/add-or-update.component';
import { CategoriesDrugPackComponent } from './drug-pack/drug-pack.component';
import { CategoriesAddOrUpdateDrugPackComponent } from './drug-pack/components/add-or-update-drug-pack/add-or-update-drug-pack.component';
import { CategoriesAddOrUpdateDrugPackItemComponent } from './drug-pack/components/add-or-update-drug-pack-item/add-or-update-drug-pack-item.component';

const COMPONENTS: Array<Type<void>> = [
  CategoriesAddOrUpdateEmployeeComponent,
  CategoriesDepartmentComponent,
  CategoriesAddOrUpdateDepartmentComponent,
  CategoriesAddOrUpdateHospitalRegisterComponent,
  CategoriesGroupCategoryComponent,
  CategoriesAddOrUpdateGroupCategoryComponent,
  CategoriesTypeAttributeComponent,
  CategoriesAddOrUpdateTypeAttributeComponent,
  CategoriesAddOrUpdateTypeAttributeItemComponent,
  CategoriesEquipmentComponent,
  CategoriesAddOrUpdateEquipmentComponent,
  CategoriesEmployeeComponent,
  CategoriesDrugPackComponent,
  CategoriesAddOrUpdateDrugPackComponent,
  CategoriesAddOrUpdateDrugPackItemComponent
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
