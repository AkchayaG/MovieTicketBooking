import { Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService } from './api.service';

export interface LoginRequest {
  username: string;
  password: string;
}

export interface RegisterRequest {
  username: string;
  email: string;
  password: string;
}

export interface AuthResponse {
  token: string;
  username: string;
  role: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private api = inject(ApiService);

  login(data: LoginRequest): Observable<AuthResponse> {
    return this.api.post<AuthResponse>('Auth/login', data).pipe(
      tap(response => this.saveSession(response))
    );
  }

  register(data: RegisterRequest): Observable<AuthResponse> {
    return this.api.post<AuthResponse>('Auth/register', data).pipe(
      tap(response => this.saveSession(response))
    );
  }

  private saveSession(response: AuthResponse): void {
    sessionStorage.setItem('token', response.token);
    sessionStorage.setItem('username', response.username);
    sessionStorage.setItem('role', response.role);
  }

  getToken(): string | null {
    return sessionStorage.getItem('token');
  }

  getRole(): string | null {
    return sessionStorage.getItem('role');
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  isAdmin(): boolean {
    return this.getRole() === 'Admin';
  }

  logout(): void {
    sessionStorage.clear();
  }
}