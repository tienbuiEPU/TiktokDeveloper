import { NgModule, Type } from '@angular/core';
import { SharedModule } from '@shared';

// dashboard pages
import { DashboardComponent } from './dashboard/dashboard.component';
import { FunctionComponent } from './function/function.component';
import { LoginComponent } from './login/login.component';
import { RouteRoutingModule } from './routes-routing.module';

const COMPONENTS: Array<Type<void>> = [
  DashboardComponent,
  LoginComponent,
  FunctionComponent
];

@NgModule({
  imports: [SharedModule, RouteRoutingModule],
  declarations: COMPONENTS,
})
export class RoutesModule { }
