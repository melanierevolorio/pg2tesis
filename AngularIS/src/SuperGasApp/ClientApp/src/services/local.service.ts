import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LocalService {

  constructor() { }

  public saveData(key: string, value: string) {
    localStorage.setItem(key, value);
  }

  public saveJsonData(key: string, value) {
    localStorage.setItem(key, JSON.stringify(value));
  }

  public getJsonData<T>(key: string) {
    return JSON.parse(localStorage.getItem(key) ?? '{}') as T
  }

  public getData(key: string) {
    return localStorage.getItem(key)
  }
  public removeData(key: string) {
    localStorage.removeItem(key);
  }

  public clearData() {
    localStorage.clear();
  }
}
