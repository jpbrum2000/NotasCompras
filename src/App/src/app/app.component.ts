import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {
  title = 'Aprovacao de Notas de Compra - Micro Universo';
  isCollapsed = true;

  isLoggedIn$: Observable<boolean>;
  userName: Observable<string>;
  constructor(private authService: AuthService) { }

  ngOnInit() {
    this.isLoggedIn$ = this.authService.isLoggedIn;
    this.userName = this.authService.getUserName();
  }

  CollapseMenu() {
    this.isCollapsed = !this.isCollapsed;
  }

  logoff() {
    this.authService.logout();
  }
}
