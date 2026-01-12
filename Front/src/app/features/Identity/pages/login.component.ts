import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../services/auth.service';
import { LoginModel } from '../models/login.model';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  standalone: true,
  selector: 'app-login',
  imports: [FormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  model: LoginModel = {
    email: '',
    password: ''
  };

  successMessage?: string;
  errorMessage?: string;
  submitted = false;

  constructor(
    private authService: AuthService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

badCredentials = false;

login() {
  this.badCredentials = false;

  this.authService.login(this.model).subscribe({
    next: (res) => {
      localStorage.setItem('jwt', res.token); // 🔑
      this.router.navigate(['/patients']);
    },
    error: () => {
      this.badCredentials = true;
      this.cdr.detectChanges();
    }
  });
}
  goBack() {
    this.router.navigate(['/patients']);
  }
}
