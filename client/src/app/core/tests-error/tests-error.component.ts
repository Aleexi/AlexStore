import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-tests-error',
  templateUrl: './tests-error.component.html',
  styleUrls: ['./tests-error.component.scss']
})
export class TestsErrorComponent {
  baseUrl: string = environment.apiUrl;
  validationErrors: string[] = [];

  constructor(private http: HttpClient) {}

  /* Methods to generate errors */
  get404Error() {
    this.http.get(this.baseUrl + 'products/100').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }

  get500Error() {
    this.http.get(this.baseUrl + 'test/servererror').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }

  get400Error() {
    this.http.get(this.baseUrl + 'test/badrequest').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }

  get400ValidationError() {
    this.http.get(this.baseUrl + 'test/badrequest/ALEXANDER').subscribe({
      next: response => console.log(response),
      error: error => {
        console.log(error),
        this.validationErrors = error.errors;
    }
  })
}
}
