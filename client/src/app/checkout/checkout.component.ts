import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AccountService } from '../account/account.service';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent implements OnInit {
  constructor(private formBuilder: FormBuilder, private accountService: AccountService) {}

  ngOnInit(): void {
    this.getAddressValues();
  }

  getAddressValues() {
    
    this.accountService.getAddressFromApi().subscribe({
      next: address => {
        if (address) {
          this.checkoutForm.get('addressForm')?.patchValue(address);
        }
      }
    })
  }

  checkoutForm = this.formBuilder.group({
    addressForm: this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      street: ['', Validators.required],
      city: ['', Validators.required],
      zipCode: ['', Validators.required]
    }),
    delieveryForm: this.formBuilder.group({
      delieveryMethod: ['', Validators.required]
    }),
    paymentForm: this.formBuilder.group({
      nameOfCard: ['', Validators.required]
    })
  })
}
