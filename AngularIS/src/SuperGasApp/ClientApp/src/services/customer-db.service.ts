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


@Injectable()
export class CustomerDbService {
  constructor(private http: HttpClient) {
  }


  patchUser(patchUser: Customer): Observable<any> {
    return this.http.patch(BackendUrl.USERDATA + "/" + patchUser.id, patchUser);
  }

  customerInsert(customer: Customer): Observable<any> {
    return this.http.post(BackendUrl.USERDATA, customer);
  }

  customerRemove(id: number): Observable<any> {
    return this.http.delete(BackendUrl.USERDATA + "/" + id);
  }


  customerObtain(id: number): Observable<Customer> {
    return this.http.get<Customer>(BackendUrl.USERDATA + "/" + id);
  }

  customerObtainAll(): Observable<Customer[]> {
    return this.http.get<Customer[]>(BackendUrl.USERDATA);
  }
}
