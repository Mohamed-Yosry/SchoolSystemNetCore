import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SignInComponent } from './modules/auth/sign-in/sign-in.component';
import { SignUpComponent } from './modules/auth/sign-up/sign-up.component';
import { Authguard } from './core/auth/guards/authguard';

const routes: Routes = [
  {
    path: "sign-in",
    component: SignInComponent,
    title: "Sign-in"
  },
  {
    path: "",
    component: SignInComponent,
    title: "Sign-in"
  },
  {
    path: "sign-up",
    component: SignUpComponent,
    title: "Sign-up"
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
