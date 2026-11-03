import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookingService } from '../../../core/services/booking.service';

@Component({
  selector: 'app-my-bookings',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './my-bookings.component.html'
})
export class MyBookingsComponent implements OnInit {

  private bookingService = inject(BookingService);

  bookings: any[] = [];
  loading = true;
  error = '';

  ngOnInit(): void {
    this.loadBookings();
  }

  loadBookings(): void {
    this.loading = true;

    this.bookingService.getMyBookings().subscribe({
      next: (data) => {
        this.bookings = data;
        this.loading = false;
      },
      error: (err) => {
        this.error = err.error?.message || 'Unable to load bookings.';
        this.loading = false;
      }
    });
  }

  cancelBooking(id: number): void {
    if (!confirm('Are you sure you want to cancel this booking?')) {
      return;
    }

    this.bookingService.cancelBooking(id).subscribe({
      next: () => {
        alert('Booking cancelled successfully.');
        this.loadBookings();
      },
      error: (err) => {
        alert(err.error?.message || 'Cancellation failed.');
      }
    });
  }
}