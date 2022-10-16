export class Customer {

  constructor(id?: number, name?: string, phoneNumber?: string, address?: string) {
    this.id = id;
    this.name = name;
    this.phoneNumber = phoneNumber;
    this.address = address;
  }

  public id?: number;
  public name?: string;
  public phoneNumber?: string;
  public address?: string;
  public index?: number;
}


