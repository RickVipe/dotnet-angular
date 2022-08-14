import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { FormGroupDirective } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { User } from './user.model';
import { UsersService } from './users.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy{

  isLoading = false;
  isLoadingUser = false;
  errorText = '';
  private uUpSub!: Subscription;
  private userInsOrUp!: Subscription;
  private userDeleted!: Subscription;
  users: User[] = [];
  title = "Web Client for Users .net6 API";
  DataSource: MatTableDataSource<any> = new MatTableDataSource();
  displayedColumns: string[] = ['firstName', 'lastName', 'email', 'actions'];
  formMessage = "Add New user";
  fd !: FormGroupDirective;

  constructor(public service: UsersService, private ref: ChangeDetectorRef) { }

  ngOnInit(){
    this.isLoading = true;
    this.service.getAllUsers();
    this.uUpSub = this.service.getUsersUpdateListener()
      .subscribe((Data: {users: User[]}) => {
        this.isLoading = false;
        this.users = Data.users;
        this.DataSource = new MatTableDataSource(this.users);
        this.ref.detectChanges();
      });

    this.userInsOrUp = this.service.getUserInsOrUpListener()
      .subscribe((Data: {answer: any, status: any}) => {
        if(Data.status == 409){
          this.errorText = Data.answer.error.detail;
        }else if(Data.status == 404){
          this.errorText = Data.answer.error.detail;
        }else if(Data.status == 400){
          this.errorText = Data.answer.error.errors[0].errorMessage;
        }else{
          this.service.getAllUsers();
          this.errorText = '';
          this.service.initializeForm();
          this.fd.resetForm();
        }
        this.isLoadingUser = false;
        this.formMessage = "Add New user";
      });

      this.userInsOrUp = this.service.getUserDeletedListener()
      .subscribe((Data: {answer: any, status: any}) => {
        if(Data.status == 409){
          this.errorText = Data.answer.error.detail;
        }else if(Data.status == 404){
          this.errorText = Data.answer.error.detail;
        }else if(Data.status == 400){
          this.errorText = Data.answer.error.errors[0].errorMessage;
        }else{
          this.service.getAllUsers();
          this.errorText = '';
          this.service.initializeForm();
          this.fd.resetForm();
        }
        this.isLoadingUser = false;
        this.formMessage = "Add New user";
      });
  }

  onEdit(row: User){
    this.service.populateform(row);
    this.service.form.markAsPristine();
    this.isLoadingUser = false;
    this.formMessage = 'Update this User: ' + row.firstName;
  }

  onDelete(row: any){
    if(confirm('Confirm that you want to delete this user.')){
      this.service.deleteUser(row.id);
    }
  }

  onSubmitFormNewUser(fd: FormGroupDirective){
    this.fd = fd;
    if(this.service.form.valid){
      this.isLoadingUser = true;
      if(this.service.form.get('id')?.value){
        console.log("Update");
        this.service.updateUser();
      }else{
        console.log("New");
        this.service.insertNewUser();
      }
    }
  }

  ngOnDestroy(){
    this.uUpSub.unsubscribe();
    this.userInsOrUp.unsubscribe();
  }

}
