import { Component, ViewChild } from '@angular/core';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { MatChipInputEvent } from '@angular/material/chips';
import { FormGroup, FormBuilder } from '@angular/forms';
import { UserManagementService } from '../../services/user-management.service';
import { Observable, Subject } from 'rxjs';
import { User } from '../../models/user.model';

@Component({
  selector: 'profile-component',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent {
  myForm!: FormGroup;
  user!: User;

  constructor(public fb: FormBuilder, public userService: UserManagementService) { }

  ngOnInit(): void {
    this.reactiveForm();
    this.userService.getCurrentUser().subscribe(res => {
      this.user = res;
      this.myForm.patchValue({
        userName: this.user.userName,
        email: this.user.email,
        role: this.user.role
      })
    });
  }

  reactiveForm() {
    this.myForm = this.fb.group({
      userName: [''],
      email: [''],
      role: [''],
    });
    this.myForm.disable();
  }

  date(e) {
    var convertDate = new Date(e.target.value).toISOString().substring(0, 10);
    this.myForm.get('dob')?.setValue(convertDate, {
      onlyself: true,
    });
  }

  submitForm() {
    console.log(this.myForm.value);
  }
}
