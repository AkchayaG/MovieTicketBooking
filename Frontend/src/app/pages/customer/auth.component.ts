import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  standalone: true,
  imports: [FormsModule, RouterLink],
  template: `
    <section class="auth-section container">
      <div class="auth-card">

        <div class="auth-brand">
          <span class="brand-icon">
            <i class="bi bi-ticket-perforated-fill"></i>
          </span>
        </div>

        <span class="eyebrow">WELCOME TO CINEBOOK</span>

        <h1>
          {{ isRegister ? 'Create your account' : 'Welcome back' }}
        </h1>

        <p class="text-muted">
          {{ isRegister
            ? 'Join us and make every movie night special.'
            : 'Sign in to continue your movie experience.' }}
        </p>

        <form (ngSubmit)="submit()" #authForm="ngForm">

          <label class="form-label">Username</label>
          <input
            class="form-control form-control-lg"
            type="text"
            name="username"
            [(ngModel)]="username"
            required
            placeholder="Enter your username">

          @if (isRegister) {
            <label class="form-label mt-3">Email address</label>
            <input
              class="form-control form-control-lg"
              type="email"
              name="email"
              [(ngModel)]="email"
              required
              email
              placeholder="you@example.com">
          }

          <label class="form-label mt-3">Password</label>
          <input
            class="form-control form-control-lg"
            type="password"
            name="password"
            [(ngModel)]="password"
            required
            minlength="6"
            placeholder="Enter your password">

          @if (errorMessage) {
            <p class="text-danger mt-3">{{ errorMessage }}</p>
          }

          <button
            class="btn btn-primary btn-lg rounded-pill w-100 mt-4"
            type="submit"
            [disabled]="!authForm.valid || isLoading">

            {{ isLoading ? 'Please wait...' :
               (isRegister ? 'Create account' : 'Sign in') }}

            <i class="bi bi-arrow-right ms-2"></i>
          </button>

        </form>

        <div class="auth-switch">
          {{ isRegister ? 'Already have an account?' : 'New to CineBook?' }}

          <button type="button" (click)="toggleMode()">
            {{ isRegister ? 'Sign in' : 'Create account' }}
          </button>
        </div>

        <a routerLink="/" class="back-link d-block text-center mt-4">
          Continue browsing movies
        </a>

      </div>
    </section>
  `
})
export class AuthComponent {

  isRegister = false;

  username = '';
  email = '';
  password = '';

  errorMessage = '';
  isLoading = false;

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  toggleMode(): void {
    this.isRegister = !this.isRegister;
    this.errorMessage = '';
    this.username = '';
    this.email = '';
    this.password = '';
  }

  submit(): void {
    this.errorMessage = '';
    this.isLoading = true;

    if (this.isRegister) {

      this.authService.register({
        username: this.username,
        email: this.email,
        password: this.password
      }).subscribe({
        next: () => {
          this.isLoading = false;
          this.router.navigate(['/']);
        },
        error: (error) => {
          this.isLoading = false;
          this.errorMessage =
            error.error?.message || 'Registration failed. Try again.';
        }
      });

    } else {

      this.authService.login({
        username: this.username,
        password: this.password
      }).subscribe({
        next: (response) => {
          this.isLoading = false;

          if (response.role === 'Admin') {
            this.router.navigate(['/admin']);
          } else {
            this.router.navigate(['/']);
          }
        },
        error: (error) => {
          this.isLoading = false;
          this.errorMessage =
            error.error?.message || 'Invalid username or password.';
        }
      });

    }
  }
}