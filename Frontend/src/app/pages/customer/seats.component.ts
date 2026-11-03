import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { DemoStoreService } from '../../core/services/demo-store.service';

@Component({
  standalone: true,
  template: `
    <section class="container section-space">
      <a class="back-link" (click)="back()"><i class="bi bi-arrow-left"></i> Back to showtimes</a>
      <div class="booking-header mt-4"><div><span class="eyebrow">STEP 2 OF 3</span><h1>Select your seats</h1><p class="text-muted mb-0">{{ store.selectedMovie.title }} · {{ store.selectedShow.theatre }} · {{ store.selectedShow.time }}</p></div><span class="step-chip"> <i class="bi bi-calendar3 me-2"></i>{{ store.selectedShow.date }}</span></div>
      <div class="seat-layout mt-4">
        <div class="seat-main">
          <div class="screen-visual"><div class="screen-line"></div><span>SCREEN THIS WAY</span></div>
          <div class="seat-legend"><span><i class="seat-dot available"></i> Available</span><span><i class="seat-dot selected"></i> Selected</span><span><i class="seat-dot booked"></i> Occupied</span></div>
          @for (row of rows; track row) {
            <div class="seat-row"><span class="row-label">{{ row }}</span>
              @for (seat of seatNumbers; track seat) {
                <button class="seat" [class.selected]="isSelected(row + seat)" [class.occupied]="isOccupied(row + seat)" [disabled]="isOccupied(row + seat)" (click)="toggleSeat(row + seat)">{{ seat }}</button>
              }
              <span class="row-label">{{ row }}</span>
            </div>
          }
          <p class="text-center text-muted small mt-4">Seats are held for a limited time once selected.</p>
        </div>
        <aside class="booking-summary">
          <span class="eyebrow">YOUR ORDER</span><h3>Booking summary</h3>
          <div class="summary-movie"><div class="summary-thumb"><i class="bi bi-film"></i></div><div><strong>{{ store.selectedMovie.title }}</strong><small>{{ store.selectedShow.theatre }}</small></div></div>
          <div class="summary-line"><span>Showtime</span><strong>{{ store.selectedShow.time }}</strong></div>
          <div class="summary-line"><span>Selected seats</span><strong>{{ store.selectedSeats.length || '—' }}</strong></div>
          <div class="selected-seat-list">{{ store.selectedSeats.length ? store.selectedSeats.join(', ') : 'Choose your seats to continue' }}</div>
          <div class="summary-total"><span>Total amount</span><strong>₹{{ total }}</strong></div>
          <button class="btn btn-primary w-100 rounded-pill py-3" [disabled]="!store.selectedSeats.length" (click)="continueBooking()">Continue <i class="bi bi-arrow-right ms-2"></i></button>
          <small class="text-muted d-block text-center mt-3">Taxes may apply at checkout.</small>
        </aside>
      </div>
    </section>
  `
})
export class SeatsComponent {
  readonly store = inject(DemoStoreService);
  private readonly router = inject(Router);
  rows = ['A', 'B', 'C', 'D', 'E', 'F'];
  seatNumbers = [1, 2, 3, 4, 5, 6, 7, 8];
  occupied = ['A3', 'A4', 'B6', 'C2', 'C7', 'D4', 'E5', 'F1'];
  get total(): number { return this.store.selectedSeats.length * this.store.selectedShow.price; }
  isSelected(id: string): boolean { return this.store.selectedSeats.includes(id); }
  isOccupied(id: string): boolean { return this.occupied.includes(id); }
  toggleSeat(id: string): void {
    this.store.selectedSeats = this.isSelected(id) ? this.store.selectedSeats.filter(s => s !== id) : [...this.store.selectedSeats, id];
  }
  back(): void { this.router.navigate(['/movie', this.store.selectedMovie.movieId]); }
  continueBooking(): void { this.router.navigate(['/auth'], { queryParams: { next: 'confirmation' } }); }
}
