import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { RoleService } from './role.service';


@NgModule({
    imports  : [
        HttpClientModule
    ],
    providers: [
        RoleService
    ]
})
export class RoleModule
{
}
