import { Component, OnInit } from '@angular/core';
import { AccountService } from '../account.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SharedService } from 'src/app/shared/shared.service';
import { Router } from '@angular/router';
import { LoginComponent } from '../login/login.component';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm:FormGroup = new FormGroup({});
  submitted = false;
  errorMessages:string[]=[];
  constructor(private accountServices:AccountService , private formBuilder:FormBuilder
     , private notifactionServices:SharedService,
       private router:Router){}

  ngOnInit(): void {
    this.initForm();
  }

  initForm(){
    this.registerForm = this.formBuilder.group({

      firstName:['',[Validators.required,Validators.minLength(3)]],
      lastName:['',[Validators.required]],
      userName:['',[Validators.required]],
      Email:['',[Validators.required]],
      password:['',[Validators.required]],
    })
  }

  register(){

    this.submitted = true;
    this.errorMessages =[];
     if(this.registerForm.valid){
    this.accountServices.register(this.registerForm.value).subscribe({
      next:(res :any) => {
        this.notifactionServices.showNotifaction(true,res.value.title ,res.value.message);
        this.router.navigateByUrl('/account/login');
        console.log(res);
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
