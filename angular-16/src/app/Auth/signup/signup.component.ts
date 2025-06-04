import { Component,OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/Services/user/user.service';

import { CommonService } from 'src/app/utilities/common.service';


@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent {
  signupForm!:FormGroup
  showPassword: boolean = true;
  showconfirmPassword: boolean = true;
  constructor(private fb:FormBuilder,private _service:UserService,private _route:Router,private _commonService:CommonService){

  }
  ngOnInit(){
 this.SetValidation();
  }

SetValidation(){
  this.signupForm=this.fb.group({
    Firstname:['',Validators.required],
    Lastname:['',Validators.required],
    Username:['',Validators.required],
    Email: ['', [Validators.required, Validators.email]],
    Password:['',Validators.required,Validators.minLength(6)],
    Confirmpassword:['',Validators.required]

  })
}
Signup() {
 if(this.signupForm.valid)
 {
  this._service.Signup(this.signupForm.value).subscribe({
    
    next: () => {
      
     this._commonService.showToast("Your account has been created successfully verify email", '', 'success');
      this._route.navigateByUrl('/login');
    },
    error: (err) => {
      console.error("Signup Error:", err);
      this._commonService.showToast("Your account has not created ", '', 'success');
    }
  });
}
this.signupForm.markAllAsTouched();
}


togglePassword(){
this.showPassword = !this.showPassword; // Toggle state
const passwordInput = document.getElementById('password') as HTMLInputElement;
passwordInput.type = this.showPassword ? 'password' : 'text'; // Change input type
}
toggleConfirmPassword(){
  this.showconfirmPassword = !this.showconfirmPassword; // Toggle state
  const passwordInput = document.getElementById('confirmPassword') as HTMLInputElement;
  passwordInput.type = this.showconfirmPassword ? 'password' : 'text'; // Change input type
  }
}
