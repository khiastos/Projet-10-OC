import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../features/Identity/services/auth.service';

@Component({
  selector: 'app-header',
  standalone: true,
  templateUrl: './header.component.html'
})
export class HeaderComponent {
  constructor(
    public authService: AuthService,
    private router: Router
  ) {}

  onAuthClick() {
    if (this.authService.isAuthenticated()) {
      // Déconnexion
      this.authService.logout();
      this.router.navigate(['/login']);
    } else {
      // Connexion
      this.router.navigate(['/login']);
    }
  }
}

