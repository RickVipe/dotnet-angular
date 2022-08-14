import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { User } from './user.model';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  private auxUser: User = {
    id: '',
    firstName: '',
    lastName: '',
    email: ''
  };

  private users: any[] = [];
  private usersUpdated = new Subject<{ users: User[] }>();

  private userDeleted = new Subject<{ answer: any, status: any }>();
  private usersInsOrUp = new Subject<{ answer: any, status: any }>();

  form = this.fb.group({
    id: [''],
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    email: ['', [Validators.required, Validators.pattern("^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[a-z]{2,4}$")]],
  });

  constructor(private http: HttpClient, private router: Router, private fb: FormBuilder) { }

  populateform(m: User) {
    this.form.controls['id'].setValue(m.id);
    this.form.controls['firstName'].setValue(m.firstName);
    this.form.controls['lastName'].setValue(m.lastName);
    this.form.controls['email'].setValue(m.email);
  }

  initializeForm() {
    this.form.controls['id'].setValue('');
    this.form.controls['firstName'].setValue('');
    this.form.controls['lastName'].setValue('');
    this.form.controls['email'].setValue('');
  }

  getAllUsers() {
    //const queryParams = `?apicall=productsAll`;
    this.http.get<User[]>(
      'http://localhost:3000/api/')
      .subscribe(Data => {
        this.users = Data;
        this.usersUpdated.next({
          users: [...this.users]
        });
      });
  }

  getUsersUpdateListener() {
    return this.usersUpdated.asObservable();
  }

  insertNewUser() {
    this.http.post<User>(
      'http://localhost:3000/api/add', {
        firstName: this.form.controls['firstName'].value,
        lastName: this.form.controls['lastName'].value,
        email: this.form.controls['email'].value
      }, { observe: 'response' })
      .subscribe(response => {
        this.usersInsOrUp.next({
          answer: response,
          status: response.status,
        });
      }, error => {
        //console.log("Error: ", error);
        this.usersInsOrUp.next({
          answer: error,
          status: error.status,
        });
      });

  }

  updateUser() {
    let user_id = this.form.controls['id'].value;
    this.http.put<User>(
      'http://localhost:3000/api/' + user_id, {
        firstName: this.form.controls['firstName'].value,
        lastName: this.form.controls['lastName'].value,
        email: this.form.controls['email'].value
      }, { observe: 'response' })
      .subscribe(response => {
        this.usersInsOrUp.next({
          answer: response,
          status: response.status,
        });
      }, error => {
        this.usersInsOrUp.next({
          answer: error,
          status: error.status,
        });
      });

  }

  getUserInsOrUpListener() {
    return this.usersInsOrUp.asObservable();
  }

  deleteUser(id: any) {
    return this.http.delete<User>(
      'http://localhost:3000/api/' + id, { observe: 'response' })
      .subscribe(response => {
        this.userDeleted.next({
          answer: response,
          status: response.status,
        });
      }, error => {
        this.userDeleted.next({
          answer: error,
          status: error.status,
        });
      });
  }

  getUserDeletedListener() {
    return this.userDeleted.asObservable();
  }

  setFluentValidationErrors(form: FormGroup, err: any): void {
    Object.entries(err).forEach(([key, value]) => {
      const control = form.get(key.replace(/^[A-Z]./, ($1) => $1.toLowerCase()));
      if (!!control) {
        control.setErrors({
          fluentValidationError: (value as Array<string>).join('\r\n')
        });
      }
    });

  }

}
