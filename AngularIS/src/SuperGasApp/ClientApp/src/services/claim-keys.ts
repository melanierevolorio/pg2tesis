// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

import { Injectable } from '@angular/core';

@Injectable()
export class UserClaimKeys {
  public static readonly SID = 'sid';
  public static readonly SUB = 'sub';
  public static readonly AUTH_TIME = 'auth_time';
  public static readonly NAME = 'name';
  public static readonly EMAIL = 'email';
  public static readonly LOGOUT_URL = 'bff:logout_url';
  public static readonly ROLE = 'role';
  public static readonly PREFERRED_USERNAME = 'preferred_username';
}
