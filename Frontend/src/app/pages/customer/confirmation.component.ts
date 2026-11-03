import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { DemoStoreService } from '../../core/services/demo-store.service';

@Component({
  standalone: true,
  imports: [RouterLink],
  template: `
    <section class="container section-space confirmation-wrap">
      <div class="confirmation-card">
        <div class="success-icon"><i class="bi bi-check-lg"></i></div>
        <span class="eyebrow">BOOKING COMPLETE</span><h1>You're going to the movies!</h1>
        <p class="text-muted">Your demo booking journey is complete. Your movie night is waiting.</p>
        <div class="ticket-card">
          <div class="ticket-top"><span class="brand-mark">cine<span>book</span></span><span class="ticket-status">CONFIRMED</span></div>
          <h2>{{ store.selectedMovie.title }}</h2><p>{{ store.selectedShow.theatre }}</p>
          <div class="ticket-grid"><div><small>DATE</small><strong>{{ store.selectedShow.date }}</strong></div><div><small>TIME</small><strong>{{ store.selectedShow.time }}</strong></div><div><small>SEATS</small><strong>{{ store.selectedSeats.join(', ') || 'B4, B5' }}</strong></div><div><small>AMOUNT</small><strong>₹{{ store.selectedSeats.length * store.selectedShow.price || 360 }}</strong></div></div>
          <div class="ticket-perforation"><span></span><span></span></div><div class="ticket-code">CB · 26 · DEMO · 001</div>
        </div>
        <a routerLink="/" class="btn btn-primary rounded-pill px-4 mt-4">Back to home <i class="bi bi-arrow-right ms-2"></i></a>
      </div>
    </section>
  `
})
export class ConfirmationComponent { readonly store = inject(DemoStoreService); }
