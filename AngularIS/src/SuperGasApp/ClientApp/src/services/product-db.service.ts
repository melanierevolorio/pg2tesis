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
import { Product } from '../models/product.model';


@Injectable()
export class ProductDbService {
  constructor(private http: HttpClient) {
  }


  productModify(product: Product): Observable<any> {
    return this.http.patch(BackendUrl.PRODUCT + "/" + product.id, product);
  }

  productInsert(product: Product): Observable<any> {
    return this.http.post(BackendUrl.PRODUCT, product);
  }

  productRemove(id: number): Observable<any> {
    return this.http.delete(BackendUrl.PRODUCT + "/" + id);
  }


  productObtain(id: number): Observable<Product> {
    return this.http.get<Product>(BackendUrl.PRODUCT + "/" + id);
  }

  productObtainAll(): Observable<Product[]> {
    return this.http.get<Product[]>(BackendUrl.PRODUCT);
  }
}
