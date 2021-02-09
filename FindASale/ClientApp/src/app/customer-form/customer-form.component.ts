import { Component, OnInit } from "@angular/core";
import { FormArray, FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";

import { CustomerFormService } from "../shared/customer-form.service";
import { Result } from "../shared/result-model";

@Component({
  selector: "app-customer-form",
  templateUrl: "./customer-form.component.html",
  styleUrls: ["./customer-form.component.css"],
})
export class CustomerFormComponent implements OnInit {
  customerForm: FormGroup;

  // Car Types
  CarTypes: any = [
    "Family cars",
    "Sports cars",
    "Tradie vehicles",
    "Not looking for anything specific",
  ];

  constructor(private fb: FormBuilder, private service: CustomerFormService, private router: Router) {}

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
    console.log('inside component ', this.customerForm);
    this.service.postForm(this.customerForm.value).subscribe(
      (res: Result) => {
        console.log(res);
        this.service.result = res;
        this.router.navigate(['/assigned']);
      },
      err => {
        console.log(err);
      }
    )
  }
}