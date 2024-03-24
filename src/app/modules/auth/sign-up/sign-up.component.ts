import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, RequiredValidator, Validators } from '@angular/forms';
import { AuthService } from 'src/app/core/auth/auth.service';
import { SignUpModule } from './sing-up.module';
import { SignUpModel } from './models/sign-up.model';
import { RoleService } from 'src/app/core/role/role.service';
import { RoleModel } from 'src/app/core/role/role.model';
import { MatchValidator } from 'src/shared/CustomVlaidators/match.validator';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
  signUpNgForm: FormGroup;
  showAlert: boolean = false;
  alert: { message: string } = {
    message: ''
  };
  roles: Array<RoleModel>;
  selectedRole = "";  

  public constructor(
    private _formBuilder: FormBuilder,
    private _authService: AuthService,
    private _roleService: RoleService
  ) { }

  /**
   * 
   */
  ngOnInit(): void {
    // get all roles
    this.GetAllRoles();
    // set the form
    this.signUpNgForm = this._formBuilder.group({
      email : ["",[Validators.required,Validators.email]],
      password: ["$Ati!1234567890",[Validators.required]],
      confirmPassword: ["$Ati!1234567890",[Validators.required]],
      firstName: ["test",[Validators.required]],
      lastName: ["test",[Validators.required]],
      userName: ["",[Validators.required]],
      role: ["",Validators.required],
    },
      {validator: MatchValidator.mustMatch("password","confirmPassword")}
    )
  }
  SignUp(){
    if (this.signUpNgForm.invalid)
      return;
    
    // Disable the form
    this.signUpNgForm.disable();
    var credentials = this.MapToSignUpModel();
    this._authService.SingUp(credentials).subscribe(value => {
      console.log(value);
      // redirect to Home page
    },(error) => {
      this.showAlert = true;
      this.signUpNgForm.enable();
      this.alert.message = error.error.message;
    });
  }

  GetAllRoles(){
    this._roleService.GetAllRoles()
    .subscribe((response)=>{
      this.roles = response;
    },(error) =>{
      // Disable the form
      this.signUpNgForm.disable();
    })
  }
  /**
   * maps the signup form to signup model
   * @returns 
   */
  private MapToSignUpModel(): SignUpModel{
    let signUpModel  = new SignUpModel();
    signUpModel.email = this.signUpNgForm.value.email;
    signUpModel.userName = this.signUpNgForm.value.userName;
    signUpModel.lastName = this.signUpNgForm.value.lastName;
    signUpModel.firstName = this.signUpNgForm.value.firstName;
    signUpModel.password = this.signUpNgForm.value.password;
    signUpModel.role = this.signUpNgForm.value.role;
    return signUpModel;
  }

  get passwordMatchError() {
    //console.log(this.signUpNgForm.get('confirmPassword'))
    console.log(this.signUpNgForm.value.passwords.getError('mismatch'));
    return (
      this.signUpNgForm.value.passwords.getError('mismatch') &&
      this.signUpNgForm.value.passwords.get('confirmPassword')?.touched
    );
  }
}
