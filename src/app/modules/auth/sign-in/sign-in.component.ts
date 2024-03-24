import { OnInit, ViewEncapsulation } from '@angular/core';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/core/auth/auth.service';

@Component({
  selector: 'sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class SignInComponent implements OnInit {
  signInFormGroup: FormGroup;
  showAlert: boolean = false;
  alert: { type: any; message: string } = {
    type   : 'success',
    message: ''
};
  constructor(
    private _formBuilder: FormBuilder,
    private _authService: AuthService
  ) { }

  /**
   * On init
   * intilaize formgroup with formcontrols
   */
  ngOnInit(): void {
    this.signInFormGroup = this._formBuilder.group({
      email: ["asdasd@asdasd.com", [Validators.required, Validators.email]],
      password: ["$Ati!1234567890", [Validators.required]]
    });
  }
  /**
   * call the auth service to handle calling the API for the sign in
   */
  signIn(){
    // Return if the form is invalid
    if ( this.signInFormGroup.invalid )
        return;
    // Disable the form
    this.signInFormGroup.disable();

    this._authService.SignIn(this.signInFormGroup.value).subscribe(value => {
      // redirect to Home page
    },(error) => {
      this.showAlert = true;
      this.signInFormGroup.enable();
      this.alert.message = error.error.message;
    });
  }
}
