import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Customer } from '../../models/customer.model';
import { Product } from '../../models/product.model';
import { CustomerDbService } from '../../services/customer-db.service';
import { ProductDbService } from '../../services/product-db.service';
import { ModalConfirmComponent } from '../modals/confirm/modal-confirm.component';

@Component({
  selector: 'dashboard-product-component',
  templateUrl: './dashboard-product.component.html',
  styleUrls: ['./dashboard-product.component.scss']
})
export class ProductDashboardComponent implements OnInit {
  products: Product[] = [];
  displayedColumns: string[] = ['actions', 'name', 'price'];
  //dataSource = new MatTableDataSource([...ELEMENT_DATA, ...ELEMENT_DATA]);
  dataSource = new MatTableDataSource<Product>();

  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: false }) set matPaginator(paginator: MatPaginator) {
    this.dataSource.paginator = paginator;
  }

  constructor(
    public productDbService: ProductDbService,
    private dialog: MatDialog,
    private router: Router
  ) { }

  openDialog(product: Product) {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;

    dialogConfig.data = {
      title: "¿Desea eliminar este producto?",
      /*description: "Eliminar",*/
    };

    const dialogRef = this.dialog.open(ModalConfirmComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      data => {
        if (data) {
          this.productDbService.productRemove(product.id!).subscribe(result => {
            this.dataSource.data.splice(product.index!, 1);
            this.dataSource._updateChangeSubscription();
            this.dataSource.sort = this.sort;
            this.dataSource.paginator = this.matPaginator;
          });
        }
      }
    );
  }
  ngOnInit() {
    this.productDbService.productObtainAll().subscribe(result => {
      result.forEach(function (row, index) {
        row.index = index;
      });
      this.products = result;
      this.dataSource.data = this.products;
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

  delete(product: Product) {
    this.productDbService.productRemove(product.id!).subscribe(result => {
      this.dataSource.data.splice(product.index!, 1);
      this.dataSource._updateChangeSubscription();
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.matPaginator;
    });
  }

  edit(product: Product) {
    this.router.navigate(["/product/edit", product.id!]);
  }

  navigateInsertProduct() {
    this.router.navigate(["/product/add"]);
  }
}
