import { Injectable } from '@angular/core';

@Injectable()
export class BackendUrl {
  public static readonly USERDATA = '/remote/user';
  public static readonly PRODUCT = '/api/product';
  public static readonly CUSTOMER = '/api/customer';
  public static readonly ORDER = '/api/order';
}
