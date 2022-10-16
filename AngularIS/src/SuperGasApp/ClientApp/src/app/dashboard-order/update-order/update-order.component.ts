import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../../../models/user.model';
import { CustomerDbService } from '../../../services/customer-db.service';
import { OrderDbService } from '../../../services/order-db.service';
import { UserManagementService } from '../../../services/user-management.service';

@Component({
  selector: 'app-update-order',
  templateUrl: './update-order.component.html',
  styleUrls: ['./update-order.component.scss']
})
export class UpdateOrderComponent implements OnInit {
  myForm!: FormGroup;

  constructor(
    public fb: FormBuilder,
    public orderService: OrderDbService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.reactiveForm();
    this.orderService.orderObtain(parseInt(this.route.snapshot.paramMap.get('id')!)).subscribe(res => {
      this.myForm.patchValue({
        annotations: res.annotations,
        customersId: res.customersId,
        date: res.date
      })
    });
  }

  reactiveForm() {
    this.myForm = this.fb.group({
      annotations: [null],
      customersId: [null, Validators.required],
      date: [null, Validators.required],
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
      this.orderService.orderModify(this.myForm.value).subscribe(res => {
        this.router.navigate(["/order"]);
      });
    }
  }
  public myError = (controlName: string, errorName: string) => {
    return this.myForm.controls[controlName].hasError(errorName);
  }
}
