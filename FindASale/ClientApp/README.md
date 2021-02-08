Front End Structure:

-src
 -app
   +--customer-form
    | |--customer-form.component.ts + .html | .css
    |   +--customer-assigned-salesperson
    |     |--customer-assigned-salesperson.component.ts + .html | .css
    |
    +-- shared (providers + models)
      |--customer-form.service.ts
        |--customer-form-model-.model.ts

        Note: no state management on client side implemented currently.


Todo: Bind select to the form correctly