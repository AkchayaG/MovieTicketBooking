import { Injectable } from '@angular/core';
import { DEMO_MOVIES } from '../data/demo-data';
import { Movie } from '../models/movie.model';

@Injectable({ providedIn: 'root' })
export class DemoStoreService {
  movies: Movie[] = [...DEMO_MOVIES];
  selectedMovie: Movie = this.movies[0];
  selectedShow = { showId: 101, theatre: 'PVR Cinemas, Brookefields', screen: 'Screen 2', time: '06:30 PM', date: 'Today', price: 180 };
  selectedSeats: string[] = [];
  bookings = [
    { id: 'CB-26001', customer: 'Arun Kumar', movie: 'The Last Horizon', seats: 'B4, B5', amount: 360, status: 'Confirmed' },
    { id: 'CB-26002', customer: 'Priya S', movie: 'Midnight in Mumbai', seats: 'C7', amount: 180, status: 'Confirmed' },
    { id: 'CB-26003', customer: 'Karthik R', movie: 'The Silent Code', seats: 'A2, A3', amount: 360, status: 'Cancelled' }
  ];
  addMovie(movie: Partial<Movie>): void {
    const nextId = Math.max(0, ...this.movies.map(item => item.movieId)) + 1;
    this.movies = [{ ...this.movies[0], ...movie, movieId: nextId } as Movie, ...this.movies];
  }
  removeMovie(id: number): void { this.movies = this.movies.filter(movie => movie.movieId !== id); }
}
