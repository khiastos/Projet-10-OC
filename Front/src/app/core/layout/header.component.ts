import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  standalone: true,
  templateUrl: './header.component.html'
})
export class HeaderComponent {
  constructor(private router: Router) {}

  get showLoginButton(): boolean {
    return this.router.url !== '/login';
  }

  goToLogin() {
    this.router.navigate(['/login']);
  }
}
