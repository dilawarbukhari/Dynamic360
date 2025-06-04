import { Component,OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, RouterConfigOptions } from '@angular/router';
import { AuthService } from 'src/app/Services/auth/auth.service';
import { UserService } from 'src/app/Services/user/user.service';

import { CommonService } from 'src/app/utilities/common.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  loginFormgroup!:FormGroup;

  


constructor( private _route:Router,private _commonSerivce:CommonService,private fb : FormBuilder,private _service:UserService,private _authservice:AuthService){

}
ngOnInit():void{
this.SetValidation();
}
SetValidation(){
  this.loginFormgroup=this.fb.group({
   email:['',[Validators.required,Validators.email]] ,
   password:['',[Validators.required, Validators.minLength(6)]]
  })
}
login(){
  if(this.loginFormgroup.valid){
  this._service.login(this.loginFormgroup.value).subscribe((response)=>{
 debugger
 var message = response.message;
    this._authservice.setSession(response);
   this._commonSerivce.showToast(message, '', 'success')
   this._route.navigateByUrl('/app/dashboard')
  },(error) => {
    this._commonSerivce.showToast("Login Error Try Again", '', 'error')
  });
}
this.loginFormgroup.markAllAsTouched();
}


  Route(){
this._route.navigateByUrl('/signup')
  }
}
