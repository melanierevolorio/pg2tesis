import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../../../models/user.model';
import { UserManagementService } from '../../../services/user-management.service';

@Component({
  selector: 'app-update-user',
  templateUrl: './update-user.component.html',
  styleUrls: ['./update-user.component.scss']
})
export class UpdateUserComponent implements OnInit {
  myForm!: FormGroup;

  constructor(
    public fb: FormBuilder,
    public userService: UserManagementService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
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
      userName: ['', Validators.required],
      email: ['', Validators.required],
      role: ['', Validators.required],
    });
  }

  date(e) {
    var convertDate = new Date(e.target.value).toISOString().substring(0, 10);
    this.myForm.get('dob')?.setValue(convertDate, {
      onlyself: true,
    });
  }

  submitForm() {
    console.log(this.myForm.value);
    this.myForm.value.id = this.route.snapshot.paramMap.get('id')!;
    this.userService.patchUser(this.myForm.value).subscribe(res => {
      this.router.navigate(["/user"]);
    });
  }
}
