//import { Component, OnInit, AfterViewInit } from '@angular/core';
//import { User } from '../../models/user.model';
//import { AuthService } from '../../services/authentication.service';
//import { Observable} from 'rxjs';

//@Component({
//  selector: 'navbar-component',
//  templateUrl: './navbar.component.html',
//  styleUrls: ['./navbar.component.scss']
//})
//export class NavbarComponent implements OnInit {
//  user$: Observable<User> = new Observable<User>();

//  constructor(private auth: AuthService) {
//  }

//  ngOnInit(): void {
//    this.user$ = this.auth.getUser();
//  }
//}


import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../../models/user.model';
import { AuthService } from '../../services/authentication.service';

@Component({
  selector: 'navbar-component',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  user$: Observable<User> = new Observable<User>();
  sidenavWidth = 4;
  ngStyle: string;
    constructor(private auth: AuthService) {
  }

  ngOnInit(): void {
    this.user$ = this.auth.getUser();
  }

  increase() {
    this.sidenavWidth = 15;
    console.log('increase sidenav width');
  }
  decrease() {
    this.sidenavWidth = 4;
    console.log('decrease sidenav width');
  }
  // sidenavToggle() {
  //   this.ngStyle = 'this.sidenavWidth = 15';
  //   console.log('sidenav width incrases');
  // }

}
