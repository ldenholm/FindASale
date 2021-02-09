import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CustomerFormService } from '../shared/customer-form.service';

import { Result } from '../shared/result-model';

@Component({
  selector: 'app-assigned',
  templateUrl: './assigned.component.html',
  styleUrls: ['./assigned.component.css']
})
export class AssignedComponent implements OnInit {

  private data: Result;

  constructor(private service: CustomerFormService, private router: Router) { }

  ngOnInit() {
    this.data = this.service.result;
    console.log('inside ngOnInit in assigned component: ', this.data);
  }

  findAnother() {
    this.router.navigate(['findasalesperson']);
  }
}
