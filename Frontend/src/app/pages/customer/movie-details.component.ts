import { Component, inject } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { DemoStoreService } from '../../core/services/demo-store.service';

@Component({
  standalone: true,
  imports: [RouterLink],
  template: `
    <section class="container section-space">
      <a routerLink="/" class="back-link"><i class="bi bi-arrow-left"></i> Back to movies</a>
      <div class="detail-layout mt-4">
        <div class="detail-poster"><img [src]="movie.poster" [alt]="movie.title"></div>
        <div class="detail-content">
          <span class="eyebrow">CINEMA RELEASE · {{ movie.status }}</span>
          <h1>{{ movie.title }}</h1>
          <div class="detail-tags"><span>{{ movie.rating }}</span><span>{{ movie.durationMinutes }} min</span><span>{{ movie.language }}</span><span>{{ movie.genre }}</span></div>
          <p class="detail-description">{{ movie.description }}</p>
          <div class="detail-facts"><div><small>Release date</small><strong>{{ movie.releaseDate }}</strong></div><div><small>Language</small><strong>{{ movie.language }}</strong></div><div><small>Duration</small><strong>{{ movie.durationMinutes }} minutes</strong></div></div>
          <h3 class="mt-5 mb-3">Choose a showtime</h3>
          <div class="theatre-card">
            <div class="d-flex justify-content-between flex-wrap gap-2"><div><strong>PVR Cinemas, Brookefields</strong><small class="d-block text-muted mt-1"><i class="bi bi-geo-alt"></i> Coimbatore · Screen 2</small></div><span class="text-success small"><i class="bi bi-circle-fill me-1"></i> Available</span></div>
            <div class="showtime-list mt-3">
              @for (time of times; track time) {
                <button class="showtime-btn" (click)="selectShow(time)">{{ time }}</button>
              }
            </div>
          </div>
          <p class="text-muted small mt-3"><i class="bi bi-info-circle me-1"></i> Demo showtimes for frontend preview.</p>
        </div>
      </div>
    </section>
  `
})
export class MovieDetailsComponent {
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  readonly store = inject(DemoStoreService);
  readonly times = ['10:30 AM', '01:45 PM', '06:30 PM', '09:45 PM'];
  movie = this.store.movies.find(m => m.movieId === Number(this.route.snapshot.paramMap.get('id'))) ?? this.store.movies[0];
  selectShow(time: string): void {
    this.store.selectedMovie = this.movie;
    this.store.selectedShow = { ...this.store.selectedShow, time };
    this.router.navigate(['/seats', this.store.selectedShow.showId]);
  }
}
