import { Component } from '@angular/core';
import { AbstractControl, AsyncValidatorFn, FormBuilder, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { Router } from '@angular/router';
import { debounceTime, finalize, map, switchMap, take } from 'rxjs';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  errors: string[] | null = null;

  // passwordComplexityRegExpression = "(?=^.{6,10}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\s).*$";

  constructor(private formBuilder: FormBuilder, private accountService: AccountService,
    private router: Router) {}

  registerForm = this.formBuilder.group({
    displayName: ['', Validators.required],
    email: ['', [Validators.required, Validators.email], [this.validateEmail()]],
    //password: ['', [Validators.required, Validators.pattern(this.passwordComplexityRegExpression)]],
    password: ['', [Validators.required]]
  })

  onSubmit() {
    this.accountService.register(this.registerForm.value).subscribe({
      next: () => this.router.navigateByUrl('/'),
      error: error => this.errors = error.errors
    })
  }

  validateEmail(): AsyncValidatorFn {
    return (control: AbstractControl) => {
      return control.valueChanges.pipe(debounceTime(2000), take(1), switchMap(() => {
        return this.accountService.checkIfEmailExists(control.value).pipe(
          map(result => result ? {emailExists: true} : null),
          finalize(() => control.markAsTouched())
        )
      }))
      
    }
  }

}
