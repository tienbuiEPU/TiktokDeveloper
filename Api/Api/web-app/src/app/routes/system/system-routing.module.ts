import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FunctionComponent } from './function/function.component';
import { RoleComponent } from './role/role.component';
import { UserComponent } from './user/user.component';

const routes: Routes = [
  { path: 'function', component: FunctionComponent, data: { title: 'Danh sách chức năng hệ thống' } },
  { path: 'role', component: RoleComponent, data: { title: 'Danh sách nhóm quyền hệ thống' } },
  { path: 'user', component: UserComponent, data: { title: 'Danh sách tài khoản hệ thống' } }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SystemRoutingModule { }
