export class User {

  constructor(id?: string, userName?: string, email?: string, role?: string) {
    this.id = id;
    this.userName = userName;
    this.email = email;
    this.role = role;
  }

  public id?: string;
  public userName?: string;
  public email?: string;
  public role?: string;
  public index?: number;
}

export class UpdateUser {
  constructor(id?: string, role?: string, email?: string, userName?: string) {
    this.id = id;
    this.userName = userName;
    this.email = email;
    this.role = role;
  }
  public id?: string;
  public role?: string;
  public email?: string;
  public userName?: string;
}

export class AddUser {
  constructor(role?: string, email?: string, userName?: string) {
    this.userName = userName;
    this.email = email;
    this.role = role;
  }
  public role?: string;
  public email?: string;
  public userName?: string;
}

export class UserEditModel {

  constructor(id?: string, userName?: string, email?: string, role?: string) {
    this.id = id;
    this.userName = userName;
    this.email = email;
    this.role = role;
  }

  public id?: string;
  public userName?: string;
  public email?: string;
  public role?: string;
}
