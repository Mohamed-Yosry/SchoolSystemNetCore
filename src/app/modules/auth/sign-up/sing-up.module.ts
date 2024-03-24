import { NgModule } from "@angular/core";
import { SignUpComponent } from "./sign-up.component";
import { ReactiveFormsModule } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MatCheckboxModule } from "@angular/material/checkbox";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatIconModule } from "@angular/material/icon";
import { MatInputModule } from "@angular/material/input";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";
import { BrowserModule } from "@angular/platform-browser";
import { AuthModule } from "src/app/core/auth/auth.module";
import { RoleModule } from "src/app/core/role/role.module";
import {MatSelectModule} from '@angular/material/select';
//import {MatFormFieldModule} from '@angular/material/form-field';

@NgModule({
    declarations: [
        SignUpComponent
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
        RoleModule,
        MatSelectModule
    ],
    exports:[SignUpComponent]
}) 
export class SignUpModule{
}