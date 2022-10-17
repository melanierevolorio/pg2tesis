import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Customer } from '../../models/customer.model';
import { Order } from '../../models/order.model';
import { CustomerDbService } from '../../services/customer-db.service';
import { OrderDbService } from '../../services/order-db.service';
import { ModalConfirmComponent } from '../modals/confirm/modal-confirm.component';

@Component({
  selector: 'dashboard-order-component',
  templateUrl: './dashboard-order.component.html',
  styleUrls: ['./dashboard-order.component.scss']
})
export class OrderDashboardComponent implements OnInit {
  orders: Order[] = [];
  displayedColumns: string[] = ['actions', 'annotations', 'customerId', 'customerAddress', 'date'];
  //dataSource = new MatTableDataSource([...ELEMENT_DATA, ...ELEMENT_DATA]);
  dataSource = new MatTableDataSource<Order>();

  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: false }) set matPaginator(paginator: MatPaginator) {
    this.dataSource.paginator = paginator;
  }

  constructor(
    public orderDbService: OrderDbService,
    private dialog: MatDialog,
    private router: Router
  ) { }

  openDialog(order: Order) {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;

    dialogConfig.data = {
      title: "¿Desea eliminar este pedido?",
      /*description: "Eliminar",*/
    };

    const dialogRef = this.dialog.open(ModalConfirmComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      data => {
        if (data) {
          this.orderDbService.orderRemove(order.id!).subscribe(result => {
            this.dataSource.data.splice(order.index!, 1);
            this.dataSource._updateChangeSubscription();
            this.dataSource.sort = this.sort;
            this.dataSource.paginator = this.matPaginator;
          });
        }
      }
    );
  }
  ngOnInit() {
    this.orderDbService.orderObtainAll().subscribe(result => {
      result.forEach(function (row, index) {
        row.index = index;
      });
      this.orders = result;
      this.dataSource.data = this.orders;
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

  delete(order: Order) {
    this.orderDbService.orderRemove(order.id!).subscribe(result => {
      this.dataSource.data.splice(order.index!, 1);
      this.dataSource._updateChangeSubscription();
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.matPaginator;
    });
  }

  edit(order: Order) {
    this.router.navigate(["/order/edit", order.id!]);
  }

  navigateInsertOrder() {
    this.router.navigate(["/order/add"]);
  }
}
