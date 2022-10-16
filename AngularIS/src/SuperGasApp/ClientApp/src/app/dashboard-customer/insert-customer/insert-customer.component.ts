import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../../../models/user.model';
import { CustomerDbService } from '../../../services/customer-db.service';
import { UserManagementService } from '../../../services/user-management.service';

@Component({
  selector: 'app-insert-customer',
  templateUrl: './insert-customer.component.html',
  styleUrls: ['./insert-customer.component.scss']
})
export class InsertCustomerComponent implements OnInit {
  myForm!: FormGroup;
  hide = true;

  constructor(
    public fb: FormBuilder,
    public customerService: CustomerDbService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.reactiveForm();
  }


  reactiveForm() {
    this.myForm = this.fb.group({
      name: [null, Validators.required],
      phoneNumber: [null, Validators.required],
      address: [null, Validators.required],
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
    Object.keys(this.myForm.controls).forEach(key => {
      // Get errors of every form control
      if (this.myForm.get(key)!.errors != null) {
        valid = false;
      }
    });

    if (valid) {
      this.customerService.customerInsert(this.myForm.value).subscribe(res => {
        this.router.navigate(["/customer"]);
      });
    }
  }
  public myError = (controlName: string, errorName: string) => {
    return this.myForm.controls[controlName].hasError(errorName);
  }

 
}
