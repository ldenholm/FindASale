import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { CustomerForm } from './customer-form-model';
import { Result } from './result-model';
import { GroupDTO } from './group-model';

@Injectable({
  providedIn: 'root'
})
export class CustomerFormService {

  constructor(private http: HttpClient) { }
  // dto for post
  GroupsDTO: GroupDTO = new GroupDTO();

  // object to hold the response in:
  result: Result = new Result();

  postForm(formData: string[]) {
    this.GroupsDTO.Groups = formData;
    console.log('inside service: this.formData = ', this.GroupsDTO)
    return this.http.post('https://localhost:44338/api/Salesperson', this.GroupsDTO);
  }
}
