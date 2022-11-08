import { ACLService } from '@delon/acl';
import { Component } from '@angular/core';
import { SettingsService, User } from '@delon/theme';
import { LayoutDefaultOptions } from '@delon/theme/layout-default';
import { environment } from '@env/environment';
import { Router } from '@angular/router';

@Component({
  selector: 'layout-basic',
  styleUrls: [`basic.component.less`],
  templateUrl: `basic.component.html`,
})
export class LayoutBasicComponent {
  options: LayoutDefaultOptions = {
    logoExpanded: `./assets/logo-full.svg`,
    logoCollapsed: `./assets/logo-full.svg`,
  };
  searchToggleStatus = false;
  showSettingDrawer = !environment.production;
  isCollapsed = false;
  menuData: any[] = [];

  get user(): User {
    return this.settings.user;
  }

  constructor(private settings: SettingsService, private router: Router, private aclService: ACLService) {
    this.menuData = settings.user["listMenus"];
  }

  isSelected(route: string): boolean {
    if (this.router.url.includes(route)) {
      return true;
    }
    return false;
  }
}
