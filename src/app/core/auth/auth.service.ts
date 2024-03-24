import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, of, switchMap, throwError } from 'rxjs';

@Injectable()
export class AuthService {

    constructor(private _httpClinet: HttpClient) { }

    // setter and getter for access token
    set accessToken(token: string) {
        localStorage.setItem("token", token);
    }
    get accessToken(): string {
        return localStorage.getItem("token") ?? '';
    }

    /**
     * this method takes sign in model and call the api for sign in
     * @param credentials 
     * @returns the token of the user
     */
    public SignIn(credentials: { email: string; password: string }): Observable<any> {
        return this._httpClinet.post("https://localhost:7291/api/authorization/Login", credentials)
            .pipe(
                switchMap((response: any) => {
                    // set the access token
                    this.accessToken = response.token;
                    // Return a new observable with the response
                    return of(response);
                })
            )
    }
    /**
     * this method takes sing up model and call the api for registeration 
     * @param SignUpModel 
     * @returns the token of the new user
     */
    public SingUp(SignUpModel): Observable<any> {
        return this._httpClinet.post("https://localhost:7291/api/authorization/Register", SignUpModel)
            .pipe(
                switchMap((response: any) => {
                    console.log(response.token);
                    // set the access token
                    this.accessToken = response.token;
                    // Return a new observable with the response
                    return of(response);
                })
            );
    }
    /**
     * Sign out
     */
    signOut(): Observable<any>
    {
        // Remove the access token from the local storage
        localStorage.removeItem('token');

        // Set the authenticated flag to false
        //this._authenticated = false;

        // Return the observable
        return of(true);
    }

    /**
     * Check the authentication status
     */
    check(): Observable<boolean>
    {
        // Check if the user is logged in
        // if ( this._authenticated )
        // {
        //     return of(true);
        // }

        // Check the access token availability
        if ( !this.accessToken )
        {
            return of(false);
        }

        // Check the access token expire date
        // if ( AuthUtils.isTokenExpired(this.accessToken) )
        // {
        //     return of(false);
        // }

        // If the access token exists and it didn't expire, sign in using it
        //return this.signInUsingToken();
        return of(true);
    }
}
