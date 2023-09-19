import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor( private formBuilder:FormBuilder ,private accountServices: AccountService, private router:Router){}
  
  loginForm:FormGroup = new FormGroup({});
  submitted = false;
  errorMessages:string[]=[];
  
  ngOnInit(): void {
    this.initForm();
  }
  


  initForm(){
    this.loginForm = this.formBuilder.group({

      userName:['',Validators.required],
      password:['',Validators.required],
    })
  }

  login(){
    this.submitted = true;
    this.errorMessages =[];
    if(this.loginForm.valid){
      this.accountServices.login(this.loginForm.value).subscribe({
        next:(res :any) => {
         
          
        },
        error:err =>{
          if(err.error.error){
            this.errorMessages = err.error.error;
           
          }
          else{
            this.errorMessages.push(err.error);
          }
        }
      })
    }
  }
}
