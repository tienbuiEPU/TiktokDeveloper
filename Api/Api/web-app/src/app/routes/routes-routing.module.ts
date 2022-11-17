import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SimpleGuard } from '@delon/auth';
import { environment } from '@env/environment';

// layout
import { LayoutBasicComponent } from '../layout/basic/basic.component';
import { LayoutLoginComponent } from '../layout/login/login.component';
// dashboard pages
import { DashboardComponent } from './dashboard/dashboard.component';
import { LoginComponent } from './login/login.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutBasicComponent,
    canActivate: [SimpleGuard],
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: 'dashboard', component: DashboardComponent, data: { title: 'Tổng quan' } },
      { path: 'exception', loadChildren: () => import('./exception/exception.module').then(m => m.ExceptionModule) },
      { path: 'category', loadChildren: () => import('./category/category.module').then(m => m.CategoryModule) },
      { path: 'system', loadChildren: () => import('./system/system.module').then(m => m.SystemModule) },
      { path: 'account', loadChildren: () => import('./account/account.module').then(m => m.AccountModule) }
    ]
  },
  {
    path: '',
    component: LayoutLoginComponent,
    children: [{ path: 'login', component: LoginComponent, data: { title: 'Đăng nhập', breadcrumb: 'Đăng nhập' } }]
  },
  { path: '**', redirectTo: 'exception/404' }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
      useHash: environment.useHash,
      // NOTICE: If you use `reuse-tab` component and turn on keepingScroll you can set to `disabled`
      // Pls refer to https://ng-alain.com/components/reuse-tab
      scrollPositionRestoration: 'top'
    })
  ],
  exports: [RouterModule]
})
export class RouteRoutingModule {}
