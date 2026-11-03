import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class BookingService {
  private api = inject(ApiService);

  getMyBookings(): Observable<any[]> {
    return this.api.get<any[]>('bookings/my-bookings');
  }

  cancelBooking(id: number): Observable<any> {
    return this.api.put<any>(`bookings/${id}/cancel`, {});
  }
}