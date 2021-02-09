import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { CustomerForm } from './customer-form-model';

@Injectable({
  providedIn: 'root'
})
export class CustomerFormService {

  constructor(private http: HttpClient) { }

  //formData: CustomerForm = new CustomerForm();

  postForm(formData: CustomerForm) {
    console.log('inside service: this.formData = ', formData)
    return this.http.post('https://localhost:44338/api/Salesperson', formData);
  }
}
