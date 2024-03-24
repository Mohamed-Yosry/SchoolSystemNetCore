import { NgModule } from "@angular/core";
import { SignInComponent } from "./sign-in.component";
import { BrowserModule } from "@angular/platform-browser";
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ReactiveFormsModule } from "@angular/forms";
import { AuthModule } from "src/app/core/auth/auth.module";
import { MatCardModule } from '@angular/material/card';
import {RouterModule} from '@angular/router';
import { authSignInRoutes } from "./sing-in.routing";

@NgModule({
    declarations: [
        SignInComponent
    ],
    imports: [
        ReactiveFormsModule,
        BrowserModule,
        MatButtonModule,
        MatCheckboxModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        MatProgressSpinnerModule,
        AuthModule,
        MatCardModule ,
        //RouterModule.forChild(authSignInRoutes),
        RouterModule,
    ],
    exports:[SignInComponent]
})
export class SignInModule {
}