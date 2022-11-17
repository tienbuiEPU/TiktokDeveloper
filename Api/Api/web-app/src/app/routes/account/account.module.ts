import { NgModule, Type } from '@angular/core';
import { SharedModule } from '@shared';
import { AccountRoutingModule } from './account-routing.module';
import { NzPageHeaderModule } from 'ng-zorro-antd/page-header';
import { NzTreeSelectModule } from 'ng-zorro-antd/tree-select';
import { NzUploadModule } from 'ng-zorro-antd/upload';

import { AccountSettingsComponent } from './account.component';

const COMPONENTS: Array<Type<void>> = [AccountSettingsComponent];

@NgModule({
  imports: [SharedModule, NzPageHeaderModule, NzTreeSelectModule, NzUploadModule, AccountRoutingModule],
  declarations: COMPONENTS
})
export class AccountModule {}
