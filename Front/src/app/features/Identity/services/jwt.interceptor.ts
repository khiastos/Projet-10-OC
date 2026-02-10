import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';

// Intercepteur HTTP pour ajouter le token JWT à chaque requête sortante pour les API sécurisées
@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(
    private router: Router
  ) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {

    // Récupérer le token JWT depuis le localStorage
    const token = localStorage.getItem('jwt');

    // On initialise authReq avec la requête originale, si le token existe, on va la cloner et ajouter l'en-tête Authorization, sinon on continue avec la requête originale
    let authReq = req;

    // Si le token existe, cloner la requête et ajouter l'en-tête Authorization
    if (token) {
      // Clone la requête prq les requêtes HTTP sont immuables, on doit les cloner pour les modifier, donc on ajoute l'en-tête Authorization avec le token JWT
      const authReq = req.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
      });
      return next.handle(authReq).pipe(
        catchError(err => {
          // Si une erreur 401 Unauthorized est reçue, rediriger vers la page de connexion
          if (err.status === 401) {
            // Supprimer le token JWT du localStorage
            localStorage.removeItem('jwt');
            // Rediriger vers la page de connexion
            this.router.navigate(['/login']);
          }
          return throwError(() => err);
        })
      );
    }

    // Sinon, continuer avec la requête originale
    return next.handle(authReq);
  }
}
