import { HttpInterceptorFn } from '@angular/common/http';

// Permet d'ajouter le token JWT à chaque requête HTTP sortante
export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  // Récupère le token JWT depuis le stockage local
  const token = localStorage.getItem('jwt');

  // Si le token existe, ajoute-le aux en-têtes de la requête
  if (token) {
    req = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
  }

  return next(req);
};
