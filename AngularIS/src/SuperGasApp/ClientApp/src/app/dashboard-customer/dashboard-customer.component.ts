import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Customer } from '../../models/customer.model';
import { CustomerDbService } from '../../services/customer-db.service';
import { ModalConfirmComponent } from '../modals/confirm/modal-confirm.component';

@Component({
  selector: 'dashboard-customer-component',
  templateUrl: './dashboard-customer.component.html',
  styleUrls: ['./dashboard-customer.component.scss']
})
export class CustomerDashboardComponent implements OnInit {
  customers: Customer[] = [];
  displayedColumns: string[] = ['actions', 'name', 'phoneNumber', 'address'];
  //dataSource = new MatTableDataSource([...ELEMENT_DATA, ...ELEMENT_DATA]);
  dataSource = new MatTableDataSource<Customer>();

  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: false }) set matPaginator(paginator: MatPaginator) {
    this.dataSource.paginator = paginator;
  }

  constructor(
    public customerDbService: CustomerDbService,
    private dialog: MatDialog,
    private router: Router
  ) { }

  openDialog(customer: Customer) {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;

    dialogConfig.data = {
      title: "¿Desea eliminar este cliente?",
     /* description: "Eliminar",*/
    };

    const dialogRef = this.dialog.open(ModalConfirmComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      data => {
        if (data) {
          this.customerDbService.customerRemove(customer.id!).subscribe(result => {
            this.dataSource.data.splice(customer.index!, 1);
            this.dataSource._updateChangeSubscription();
            this.dataSource.sort = this.sort;
            this.dataSource.paginator = this.matPaginator;
          });
        }
      }
    );
  }
  ngOnInit() {
    this.customerDbService.customerObtainAll().subscribe(result => {
      result.forEach(function (row, index) {
        row.index = index;
      });
      this.customers = result;
      this.dataSource.data = this.customers;
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.matPaginator;
    });
  }

  applyFilter(filterValue: Event) {
    let value = (event!.target as HTMLInputElement).value;

    value = value.trim(); // Remove whitespace
    value = value.toLowerCase(); // Datasource defaults to lowercase matches
    this.dataSource.filter = value;
  }

  delete(customer: Customer) {
    this.customerDbService.customerRemove(customer.id!).subscribe(result => {
      this.dataSource.data.splice(customer.index!, 1);
      this.dataSource._updateChangeSubscription();
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.matPaginator;
    });
  }

  edit(customer: Customer) {
    this.router.navigate(["/customer/edit", customer.id!]);
  }

  navigateInsertCustomer() {
    this.router.navigate(["/customer/add"]);
  }
}
