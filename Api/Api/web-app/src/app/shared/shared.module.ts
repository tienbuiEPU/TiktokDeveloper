import { NgModule, Type } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AlainThemeModule } from '@delon/theme';
import { DelonACLModule } from '@delon/acl';
import { DelonFormModule } from '@delon/form';

import { SHARED_DELON_MODULES } from './shared-delon.module';
import { SHARED_ZORRO_MODULES } from './shared-zorro.module';
import { DeleteCellRenderer } from './custom_editor/delete-cell-renderer';
import { AgGridModule } from 'ag-grid-angular';
// eslint-disable-next-line import/no-unassigned-import
import 'ag-grid-enterprise';
import { SelectAgGrid } from './custom_editor/select-ag-grid';
import { DetailImportEximComponent } from './components/exims/detail-import.component';
import { AutocompleAgGrid } from './custom_editor/autocomplete/autocomplete-editor';
import { CheckboxEditor } from './custom_editor/checkbox.component';
import { PDFViewerComponent } from './components/pdf-viewer/pdf-viewer.component';

// #region third libs

const THIRDMODULES: Array<Type<void>> = [AgGridModule,];

// #endregion

// #region your componets & directives

const COMPONENTS: Array<Type<void>> = [
  DeleteCellRenderer,
  SelectAgGrid,
  AutocompleAgGrid,
  DetailImportEximComponent,
  CheckboxEditor,
  PDFViewerComponent
];
const DIRECTIVES: Array<Type<void>> = [];

// #endregion

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    ReactiveFormsModule,
    AlainThemeModule.forChild(),
    DelonACLModule,
    DelonFormModule,
    ...SHARED_DELON_MODULES,
    ...SHARED_ZORRO_MODULES,
    // third libs
    ...THIRDMODULES
  ],
  declarations: [
    // your components
    ...COMPONENTS,
    ...DIRECTIVES
  ],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    AlainThemeModule,
    DelonACLModule,
    DelonFormModule,
    ...SHARED_DELON_MODULES,
    ...SHARED_ZORRO_MODULES,
    // third libs
    ...THIRDMODULES,
    // your components
    ...COMPONENTS,
    ...DIRECTIVES
  ]
})
export class SharedModule { }
