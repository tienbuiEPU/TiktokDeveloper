import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from 'src/app/core/services/user.service';

@Component({
  templateUrl: './login.component.html',
  styleUrls: [`login.compnent.less`],
})
export class LoginComponent implements OnInit {
  validateForm!: FormGroup;
  processing: boolean = false;

  constructor(private fb: FormBuilder, private router: Router, private userService: UserService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.validateForm = this.fb.group({
      username: [null, [Validators.required, Validators.maxLength(30)]],
      password: [null, [Validators.required, Validators.maxLength(50)]],
      remember: [true],
    });
  }

  async submitForm() {
    try {
      this.processing = true;
      for (const i in this.validateForm.controls) {
        this.validateForm.controls[i].markAsDirty();
        this.validateForm.controls[i].updateValueAndValidity();
      }

      if (this.validateForm.valid) {
        const resp = await this.userService.login(this.validateForm.value);
        console.log(resp);

        if (resp?.code === 0) {
          let url = this.route.snapshot.queryParams["url"];
          console.log(url);
          this.router.navigateByUrl(url ?? '/');
        }
      }
    } catch (error) {
      console.log(error);
    }
    this.processing = false;
  }
}
