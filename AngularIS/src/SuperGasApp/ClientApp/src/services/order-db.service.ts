import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { Observable, map, throwError, catchError } from 'rxjs';
import { AddUser, UpdateUser, User } from '../models/user.model';
import { LocalService } from '../services/local.service';
import { LocalKeys } from './local-keys';
import { BackendUrl } from './url-keys';
import { UserClaimKeys } from './claim-keys';
import { Customer } from '../models/customer.model';
import { Order } from '../models/order.model';


@Injectable()
export class OrderDbService {
  constructor(private http: HttpClient) {
  }


  orderModify(order: Order): Observable<any> {
    return this.http.patch(BackendUrl.ORDER + "/" + order.id, order);
  }

  orderInsert(order: Order): Observable<any> {
    return this.http.post(BackendUrl.ORDER, order);
  }

  orderRemove(id: number): Observable<any> {
    return this.http.delete(BackendUrl.ORDER + "/" + id);
  }


  orderObtain(id: number): Observable<Order> {
    return this.http.get<Order>(BackendUrl.ORDER + "/" + id);
  }

  orderObtainAll(): Observable<Order[]> {
    return this.http.get<Order[]>(BackendUrl.ORDER);
  }
}
