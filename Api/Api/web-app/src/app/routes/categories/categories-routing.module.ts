import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CategoriesCountryComponent } from './country/country.component';
import { CategoriesEmployeeComponent } from './employee/employee.component';
import { CategoriesDepartmentComponent } from './department/department.component';
import { CategoriesAddressComponent } from './address/address.component';
import { CategoriesProvinceComponent } from './province/province.component';
import { CategoriesSickComponent } from './sick/sick.component';
import { CategoriesCardIssuerComponent } from './card-issuer/card-issuer.component';
import { CategoriesHospitalRegisterComponent } from './hospital-register/hospital-register.component';
import { CategoriesTestIndexComponent } from './test-index/test-index.component';
import { CategoriesPlaceClsComponent } from './place-cls/place-cls.component';
import { CategoriesGroupCategoryComponent } from './group-category/group-category.component';
import { CategoriesMedicalRecordComponent } from './medical-record/medical-record.component';
import { CategoriesVendorComponent } from './vendor/vendor.component';
import { CategoriesTypeAttributeComponent } from './type-attribute/type-attribute.component';
import { CategoriesTechnicalServicesComponent } from './technical-services/technical-services.component';
import { CategoriesMedicineSuppliesComponent } from './medicine-supplies/medicine-supplies.component';
import { CategoriesEquipmentComponent } from './equipment/equipment.component';
import { CategoriesCustomerComponent } from './customer/customer.component';
import { CategoriesBhytCardComponent } from './bhyt-card/bhyt-card.component';
import { CategoriesDrugPackComponent } from './drug-pack/drug-pack.component';
import { CategoriesSurgeryPackageComponent } from './surgery-package/surgery-package.component';

const routes: Routes = [
  { path: 'country', component: CategoriesCountryComponent, data: { title: 'Danh sách quốc gia' } },
  { path: 'employee', component: CategoriesEmployeeComponent, data: { title: 'Danh sách nhân viên' } },
  { path: 'department', component: CategoriesDepartmentComponent, data: { title: 'Danh sách khoa kho' } },
  { path: 'address', component: CategoriesAddressComponent, data: { title: 'Danh sách địa chỉ' } },
  { path: 'province', component: CategoriesProvinceComponent, data: { title: 'Danh sách tỉnh thành phố' } },
  { path: 'sick', component: CategoriesSickComponent, data: { title: 'Danh sách bệnh ICD10' } },
  { path: 'card-issuer', component: CategoriesCardIssuerComponent, data: { title: 'Nơi phát hành' } },
  { path: 'hospital-register', component: CategoriesHospitalRegisterComponent, data: { title: 'Nơi khám/chữa bệnh' } },
  { path: 'group-category', component: CategoriesGroupCategoryComponent, data: { title: 'Nhóm thuốc' } },
  { path: 'place-cls', component: CategoriesPlaceClsComponent, data: { title: 'Nơi thực hiện cận lâm sàng' } },
  { path: 'medical-record', component: CategoriesMedicalRecordComponent, data: { title: 'Bệnh án - Mẫu in' } },
  { path: 'type-attribute', component: CategoriesTypeAttributeComponent, data: { title: 'Loại hình' } },
  { path: 'vendor', component: CategoriesVendorComponent, data: { title: 'Đơn vị giao nhận' } },
  { path: 'province', component: CategoriesProvinceComponent, data: { title: 'Danh sách tỉnh thành phố' } },
  { path: 'sick', component: CategoriesSickComponent, data: { title: 'Danh sách bệnh ICD10' } },
  { path: 'card-issuer', component: CategoriesCardIssuerComponent, data: { title: 'Nơi phát hành' } },
  { path: 'hospital-register', component: CategoriesHospitalRegisterComponent, data: { title: 'Nơi khám/chữa bệnh' } },
  { path: 'group-category', component: CategoriesGroupCategoryComponent, data: { title: 'Nhóm thuốc' } },
  { path: 'place-cls', component: CategoriesPlaceClsComponent, data: { title: 'Nơi thực hiện cận lâm sàng' } },
  { path: 'medical-record', component: CategoriesMedicalRecordComponent, data: { title: 'Bệnh án - Mẫu in' } },
  { path: 'type-attribute', component: CategoriesTypeAttributeComponent, data: { title: 'Loại hình' } },
  { path: 'vendor', component: CategoriesVendorComponent, data: { title: 'Đơn vị giao nhận' } },
  { path: 'drug-service-pack', component: CategoriesDrugPackComponent, data: { title: 'Gói dịch vụ thuốc' } },
  { path: 'surgery-package', component: CategoriesSurgeryPackageComponent, data: { title: 'Gói phẫu thuật' } },
  { path: 'test-index', component: CategoriesTestIndexComponent },
  { path: 'technical-services', component: CategoriesTechnicalServicesComponent },
  { path: 'medicine-supplies', component: CategoriesMedicineSuppliesComponent },
  { path: 'equipment', component: CategoriesEquipmentComponent },
  { path: 'customer', component: CategoriesCustomerComponent },
  { path: 'bhyt-card', component: CategoriesBhytCardComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CategoriesRoutingModule { }
