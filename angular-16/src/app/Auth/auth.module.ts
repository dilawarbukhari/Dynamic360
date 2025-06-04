import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthRoutingModule } from './auth-routing.module';
import { SignupComponent } from './signup/signup.component';
import { LoginComponent } from './login/login.component';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonService } from '../utilities/common.service';



@NgModule({
  declarations: [   SignupComponent,
    LoginComponent],
  imports: [
    CommonModule,

    AuthRoutingModule,
    ReactiveFormsModule
  ]
  ,
  providers:[CommonService]
})
export class AuthModule { }
