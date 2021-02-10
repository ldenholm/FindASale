import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";

//import { ToastrService } from 'ngx-toastr';

import { CustomerFormService } from "../shared/customer-form.service";
import { Group } from "../shared/group-model";
import { Result } from "../shared/result-model";

@Component({
  selector: "app-customer-form",
  templateUrl: "./customer-form.component.html",
  styleUrls: ["./customer-form.component.css"],
})
export class CustomerFormComponent implements OnInit {
  customerForm: FormGroup;
  groups: string[] = [];

  CarTypes: Group[] = [
    {name: "Sports cars", symbol: 'B'},
    {name: "Family cars", symbol: 'C'},
    {name: "Tradie vehicles", symbol: 'D'},
    {name: "Not looking for anything specific", symbol: null}
  ]

  constructor(private fb: FormBuilder, private service: CustomerFormService, private router: Router, /*private toastr: ToastrService*/) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm(): void {
    // grab form and initialize it to the formgroup
    this.customerForm = this.fb.group({
      speaksGreek: [false, Validators.required],
      carType: [null, Validators.required],
    })
  }

  onSubmit(): void {
    this.buildGroups();
    this.service.postForm(this.groups).subscribe(
      (res: Result) => {
        console.log(res);
        this.service.result = res;
        this.resetGroups();
        this.router.navigate(['/assigned']);
      },
      err => {
        console.log(err);
        this.resetGroups();
      }
    )
  }

  buildGroups() {
    if (this.customerForm.controls['speaksGreek'].value) {
      this.groups.push('A');
    }
    this.groups.push(this.customerForm.controls['carType'].value);

    console.log('inside buildGroups func ', this.groups)
  }

  resetGroups() {
    this.groups = [];
  }
}