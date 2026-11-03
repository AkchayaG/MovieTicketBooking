import { Component, inject } from '@angular/core';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { AuthService } from './core/services/auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive],
  template: `
    <header class="site-header">
      <nav class="navbar navbar-expand-lg container py-3">

        <a class="navbar-brand brand-mark" routerLink="/">
          <span class="brand-icon">
            <i class="bi bi-ticket-perforated-fill"></i>
          </span>
          cine<span>book</span>
        </a>

        <button class="navbar-toggler border-0 text-white"
                type="button"
                data-bs-toggle="collapse"
                data-bs-target="#mainNav">
          <i class="bi bi-list fs-2"></i>
        </button>

        <div class="collapse navbar-collapse" id="mainNav">

          <div class="navbar-nav mx-auto gap-lg-4">

            <a class="nav-link"
               routerLink="/"
               routerLinkActive="active"
               [routerLinkActiveOptions]="{exact:true}">
              Home
            </a>

            <a class="nav-link" routerLink="/" fragment="movies">
              Movies
            </a>

            @if (isCustomer) {
              <a class="nav-link" routerLink="/confirmation">
                My Bookings
              </a>
            }

          </div>

          <div class="d-flex align-items-center gap-2 mt-3 mt-lg-0">

            @if (isAdmin) {
              <span class="text-white small">
                <i class="bi bi-person-circle me-1"></i>
                {{ username }}
              </span>

              <a class="btn btn-outline-light btn-sm rounded-pill px-3"
                 routerLink="/admin">
                <i class="bi bi-grid me-1"></i>
                Admin Dashboard
              </a>
            }

            @else if (isCustomer) {
              <span class="text-white small">
                <i class="bi bi-person-circle me-1"></i>
                {{ username }}
              </span>
            }

            @if (isLoggedIn) {
              <button
                class="btn btn-primary btn-sm rounded-pill px-3"
                (click)="logout()">
                Logout
                <i class="bi bi-box-arrow-right ms-1"></i>
              </button>
            }

            @else {
              <a class="btn btn-primary btn-sm rounded-pill px-3"
                 routerLink="/auth">
                Sign in
                <i class="bi bi-arrow-right ms-1"></i>
              </a>
            }

          </div>
        </div>
      </nav>
    </header>

    <main>
      <router-outlet />
    </main>

    <footer class="site-footer">
      <div class="container d-flex flex-column flex-md-row justify-content-between gap-2">
        <div class="brand-mark">cine<span>book</span></div>

        <span class="text-muted small">
          Your seat to the best stories. © 2026 CineBook.
        </span>

        <span class="text-muted small">
          Made for movie lovers
          <i class="bi bi-heart-fill text-primary"></i>
        </span>
      </div>
    </footer>
  `
})
export class AppComponent {

  private authService = inject(AuthService);
  private router = inject(Router);

  get isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }

  get isAdmin(): boolean {
    return this.authService.isAdmin();
  }

  get isCustomer(): boolean {
    return this.isLoggedIn && !this.isAdmin;
  }

  get username(): string {
    return sessionStorage.getItem('username') || '';
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/']);
  }
}