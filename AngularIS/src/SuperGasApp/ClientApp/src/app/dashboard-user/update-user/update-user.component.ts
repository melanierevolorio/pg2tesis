import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Role, User } from '../../../models/user.model';
import { UserManagementService } from '../../../services/user-management.service';

@Component({
  selector: 'app-update-user',
  templateUrl: './update-user.component.html',
  styleUrls: ['./update-user.component.scss']
})
export class UpdateUserComponent implements OnInit {
  myForm!: FormGroup;
  roles$: Observable<Role[]> = new Observable<Role[]>();

  constructor(
    public fb: FormBuilder,
    public userService: UserManagementService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.roles$ = this.userService.getRoles();

    this.reactiveForm();
    this.userService.getUser(this.route.snapshot.paramMap.get('id')!).subscribe(res => {
      this.myForm.patchValue({
        userName: res.userName,
        email: res.email,
        role: res.role
      })
    });
  }

  reactiveForm() {
    this.myForm = this.fb.group({
      userName: [null, Validators.required],
      email: [null, [Validators.required, Validators.email]],
      role: [null, Validators.required],
    });
  }

  date(e) {
    var convertDate = new Date(e.target.value).toISOString().substring(0, 10);
    this.myForm.get('dob')?.setValue(convertDate, {
      onlyself: true,
    });
  }

  submitForm() {
    let valid = true;
    console.log(this.myForm.value);
    this.myForm.value.id = this.route.snapshot.paramMap.get('id')!;
    Object.keys(this.myForm.controls).forEach(key => {
      // Get errors of every form control
      if (this.myForm.get(key)!.errors != null) {
        valid = false;
      }
    });

    if (valid) {
      this.userService.patchUser(this.myForm.value).subscribe(res => {
        this.router.navigate(["/user"]);
      });
    }
  }
  public myError = (controlName: string, errorName: string) => {
    return this.myForm.controls[controlName].hasError(errorName);
  }
}
