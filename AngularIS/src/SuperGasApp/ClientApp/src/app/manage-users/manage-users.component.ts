import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { User } from '../../models/user.model';
import { UserManagementService } from '../../services/user-management.service';

@Component({
  selector: 'manage-users-component',
  templateUrl: './manage-users.component.html',
  styleUrls: ['./manage-users.component.scss']
})
export class ManageUsersComponent implements OnInit {
  users: User[] = [];
  displayedColumns: string[] = ['id', 'userName', 'email', 'role', 'actions'];
  //dataSource = new MatTableDataSource([...ELEMENT_DATA, ...ELEMENT_DATA]);
  dataSource = new MatTableDataSource<User>();

  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: false }) set matPaginator(paginator: MatPaginator) {
    this.dataSource.paginator = paginator;
  }

  constructor(public userService: UserManagementService) { }

  ngOnInit() {
    this.userService.getAllUsers().subscribe(result => {
      result.forEach(function (row, index) {
        row.index = index;
      });
      this.users = result;
      this.dataSource.data = this.users;
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

  delete(user: User) {
    this.userService.deleteUser(user.id!).subscribe(result => {
      this.dataSource.data.splice(user.index!, 1);
      this.dataSource._updateChangeSubscription();
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.matPaginator;
    });
  }
}
