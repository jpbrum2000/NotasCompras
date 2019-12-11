import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { User } from './model/user';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private loggedIn = new BehaviorSubject<boolean>(false);
  private userName = new BehaviorSubject<string>("");
  private user: User;

  getUser() {
    return this.user;
  }
  getUserName() {
    return this.userName.asObservable();
  }
  get isLoggedIn() {
    return this.loggedIn.asObservable();

  }

  constructor(private router: Router, private  httpClient:HttpClient) { }

  login(userForm: User){
    
    this.httpClient.post<User>("http://localhost:5000/AutenticaUsuario",
    {
      "Login":  userForm.login,
      "Senha":  userForm.senha
    })
    .subscribe(
      data  => {
        let response: any = data;
        if (userForm.login ===  response.login && userForm.senha === response.senha ) {
          this.user = response;
          this.loggedIn.next(true);
          this.userName.next(this.user.login);
          this.router.navigate(['/aprovacao-notas-compra']);
        }
      },
      error  => {
        console.log("Error", error);
      }
    );
    
  }

  logout() {
    this.loggedIn.next(false);
    this.userName.next("");
    this.user = null;
    this.router.navigate(['/login']);
  }
}
