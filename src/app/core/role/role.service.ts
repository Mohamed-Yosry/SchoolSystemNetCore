import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, switchMap } from 'rxjs';
import { RoleModel } from './role.model';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  constructor(private _httpClient: HttpClient) { }
  /**
   * get all roles
   * @returns list of all roles
   */
  public GetAllRoles(): Observable<any> {
    return this._httpClient.get("https://localhost:7291/api/role/GetAllRolesName")
      .pipe(
        switchMap((response: any) => {
          //let rolesMap = new Map();
          let roles = new Array();
          response.forEach(element => {
            var role = this.mapRoles(element);
            roles.push(role);
          });
          // Return a new observable with the response
          return of(roles);
        })
      )
  }

  private mapRoles(element: any): RoleModel {
    var role = new RoleModel();
    role.Id = element['id'];
    role.Name = element['name'];
    return role;
  }
}
