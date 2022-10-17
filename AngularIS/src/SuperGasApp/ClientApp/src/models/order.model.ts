import { Customer } from "./customer.model";

export class Order {
  static id: string;

  constructor(id?: number, annotations?: string, customerId?: string, date?: Date, customer?: Customer) {
    this.id = id;
    this.annotations = annotations;
    this.customerId = customerId;
    this.date = date;
    this.customer = customer;
  }

  public id?: number;
  public annotations?: string;
  public customerId?: string;
  public date?: Date;
  public customer?: Customer;
  public index?: number;
}
