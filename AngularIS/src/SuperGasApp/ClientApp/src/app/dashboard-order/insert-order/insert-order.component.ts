import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Customer } from '../../../models/customer.model';
import { User } from '../../../models/user.model';
import { CustomerDbService } from '../../../services/customer-db.service';
import { OrderDbService } from '../../../services/order-db.service';
import { UserManagementService } from '../../../services/user-management.service';

@Component({
  selector: 'app-insert-order',
  templateUrl: './insert-order.component.html',
  styleUrls: ['./insert-order.component.scss']
})
export class InsertOrderComponent implements OnInit {
  myForm!: FormGroup;
  customers$: Observable<Customer[]> = new Observable<Customer[]>();
  hide = true;

  constructor(
    public fb: FormBuilder,
    public orderService: OrderDbService,
    private router: Router,
    public customerService: CustomerDbService
  ) { }

  ngOnInit(): void {
    this.reactiveForm();
    //this.customerService.customerObtainAll().subscribe(res => {
    //  console.log(res);
    //  this.myForm.patchValue({
    //    customerId: res,
    //  })
    //});
    this.customers$ = this.customerService.customerObtainAll();
  }


  reactiveForm() {
    this.myForm = this.fb.group({
      annotations: [null],
      customerId: [null, Validators.required],
      date: [null, Validators.required],
    });
  }


  date(e) {
    var convertDate = new Date(e.target.value);
    let convertedDate = convertDate.toJSON();
    this.myForm.get('date')?.setValue(convertedDate, {
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
      this.orderService.orderInsert(this.myForm.value).subscribe(res => {
        this.router.navigate(["/order"]);
      });
    }
  }
  public myError = (controlName: string, errorName: string) => {
    return this.myForm.controls[controlName].hasError(errorName);
  }

 
}
