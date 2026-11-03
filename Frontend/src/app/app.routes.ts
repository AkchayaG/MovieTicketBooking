import { Routes } from '@angular/router';

import { HomeComponent } from './pages/customer/home.component';
import { MovieDetailsComponent } from './pages/customer/movie-details.component';
import { SeatsComponent } from './pages/customer/seats.component';
import { AuthComponent } from './pages/customer/auth.component';
import { ConfirmationComponent } from './pages/customer/confirmation.component';
import { MyBookingsComponent } from './pages/customer/my-bookings/my-bookings.component';
import { AdminComponent } from './pages/admin/admin.component';
import { adminGuard } from './core/admin.guard';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'movie/:id', component: MovieDetailsComponent },
  { path: 'seats/:showId', component: SeatsComponent },
  { path: 'auth', component: AuthComponent },
  { path: 'confirmation', component: ConfirmationComponent },
  { path: 'my-bookings', component: MyBookingsComponent },
  { path: 'admin', component: AdminComponent, canActivate: [adminGuard] },
  { path: '**', redirectTo: '' }
];