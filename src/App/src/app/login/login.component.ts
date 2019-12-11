import { Component, OnInit } from '@angular/core';
import {FormGroup, FormBuilder} from '@angular/forms';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  form: FormGroup;

  constructor(
    private fb: FormBuilder,         
    private authService: AuthService 
  ) { }

  ngOnInit() {
    this.form = this.fb.group({     
      login: [''],
      senha: ['']
    });
  }

  onSubmit() {
    this.authService.login(this.form.value);
  }

}
