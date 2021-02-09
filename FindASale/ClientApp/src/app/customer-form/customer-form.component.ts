import { Component, OnInit } from "@angular/core";
import { FormArray, FormBuilder, FormGroup, Validators } from "@angular/forms";

import { CustomerFormService } from "../shared/customer-form.service";

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

  constructor(private fb: FormBuilder, private service: CustomerFormService) {}

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
      res => {
        console.log(res);
      },
      err => {
        console.log(err);
      }
    )
  }
}

//   onSubmit(form: NgForm): void {
//     console.log(form);
//     this.service.postForm().subscribe(
//       res => {
//         console.log(res);
//       },
//       err => {
//         console.log(err);
//       }
//     );
//   }
// }
