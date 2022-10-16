export class Order {
  static id: string;

  constructor(id?: number, annotations?: string, customersId?: string, date?: Date) {
    this.id = id;
    this.annotations = annotations;
    this.customersId = customersId;
    this.date = date;
  }

  public id?: number;
  public annotations?: string;
  public customersId?: string;
  public date?: Date;
  public index?: number;
}
