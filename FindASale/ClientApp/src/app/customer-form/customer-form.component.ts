import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { CustomerForm } from '../shared/customer-form-model';

import { CustomerFormService } from '../shared/customer-form.service';

@Component({
  selector: 'app-customer-form',
  templateUrl: './customer-form.component.html',
  styleUrls: ['./customer-form.component.css']
})
export class CustomerFormComponent implements OnInit {

  constructor(private service: CustomerFormService) { }

  ngOnInit() {
  }

  onSubmit(form: NgForm) {
    this.service.postForm().subscribe(
      res => {
        console.log(res);
      },
      err => {
        console.log(err);
      }
    );
  }
}