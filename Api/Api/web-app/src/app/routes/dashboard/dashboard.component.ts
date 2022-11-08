import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  // changeDetection: ChangeDetectionStrategy.OnPush
})
export class DashboardComponent implements OnInit {
  profileForm!: FormGroup;

  rowHeight = 60;
  nzPageSize: number = 10;

  constructor(private fb: FormBuilder) { }

  ngOnInit(): void {
    this.nzPageSize = Math.max(Math.floor((window.innerHeight - 260) / this.rowHeight), 3);
    this.profileForm = this.fb.group({
      search_text: [''],
      rangePicker: [[]],
    });
  }


  async onSubmit() { }
}
