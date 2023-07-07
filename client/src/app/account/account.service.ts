import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../shared/models/user';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})

export class AccountService {
  baseUrl: string = environment.apiUrl;
  private currentUserSource = new BehaviorSubject<User | null>(null);
  private currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router) { }

  login (formValues : any) 
  {
    return this.http.post<User>(this.baseUrl + 'account/login', formValues).pipe(
      map(user => {
        /* Set JWToken we get back in localstorage from response */
        localStorage.setItem('JWToken', user.JWToken);

        /* Store user in observable */
        this.currentUserSource.next(user);
      })
    )
  }

  /* User stays logged in after regestering */
  register(formValues: any)
  {
    return this.http.post<User>(this.baseUrl + 'account/register', formValues).pipe(
      map(user => {
        /* Set JWToken we get back in localstorage from response */
        localStorage.setItem('JWToken',user.JWToken);

        /* Store user in observable */
        this.currentUserSource.next(user);
      })
    )
  }

  logout() 
  {
    this.currentUserSource.next(null);
    localStorage.removeItem('JWToken');
    this.router.navigateByUrl('/')

  }

  checkIfEmailExists(email: string) 
  {
    return this.http.get<boolean>(this.baseUrl + 'account/emailexists?=' + email);
  }
}
