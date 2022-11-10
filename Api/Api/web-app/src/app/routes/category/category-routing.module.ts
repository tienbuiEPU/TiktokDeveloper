import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { TypeAttributeComponent } from './type-attribute/type-attribute.component';

const routes: Routes = [
  { path: 'type-attribute', component: TypeAttributeComponent, data: { title: 'Loại hình' } }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CategoryRoutingModule { }
