import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { DemoStoreService } from '../../core/services/demo-store.service';

@Component({
  standalone: true,
  imports: [RouterLink],
  template: `
    <section class="hero-section">
      <div class="hero-overlay"></div>
      <div class="container hero-content">
        <span class="eyebrow"><i class="bi bi-stars me-2"></i>THE BIG SCREEN EXPERIENCE</span>
        <h1>Every story deserves<br><span>a big screen.</span></h1>
        <p class="hero-copy">Find your next favourite movie. Choose your seats.<br class="d-none d-md-block"> Make memories that last beyond the credits.</p>
        <a class="btn btn-primary btn-lg rounded-pill px-4" href="#movies">Explore movies <i class="bi bi-arrow-down ms-2"></i></a>
        <div class="hero-meta"><span><i class="bi bi-shield-check"></i> Secure booking</span><span><i class="bi bi-lightning-charge"></i> Instant confirmation</span><span><i class="bi bi-ticket-perforated"></i> Easy cancellations</span></div>
      </div>
      <div class="hero-glow"></div>
    </section>
    <section class="container section-space" id="movies">
      <div class="section-heading d-flex flex-wrap justify-content-between align-items-end gap-3">
        <div><span class="eyebrow">PICK YOUR EXPERIENCE</span><h2>Now showing</h2><p class="text-muted mb-0">The movies everyone's talking about.</p></div>
        <div class="filter-pills"><button class="filter-pill active">All movies</button><button class="filter-pill">Tamil</button><button class="filter-pill">English</button><button class="filter-pill">Hindi</button></div>
      </div>
      <div class="row g-4 mt-2">
        @for (movie of store.movies; track movie.movieId) {
          <div class="col-6 col-md-4 col-xl-3">
            <article class="movie-card h-100">
              <a [routerLink]="['/movie', movie.movieId]" class="poster-wrap">
                <img [src]="movie.poster" [alt]="movie.title" (error)="$any($event.target).src='https://placehold.co/500x750/211d31/ffffff?text=CineBook'">
                <span class="poster-badge">{{ movie.status }}</span>
                <span class="poster-action"><i class="bi bi-arrow-up-right"></i></span>
              </a>
              <div class="movie-info">
                <div class="d-flex justify-content-between align-items-start gap-2"><h3>{{ movie.title }}</h3><span class="rating"><i class="bi bi-star-fill"></i> 8.4</span></div>
                <p>{{ movie.genre }} · {{ movie.language }}</p>
                <a [routerLink]="['/movie', movie.movieId]" class="movie-link">Book tickets <i class="bi bi-arrow-right"></i></a>
              </div>
            </article>
          </div>
        }
      </div>
    </section>
    <section class="container section-space pt-0">
      <div class="promo-strip">
        <div><span class="eyebrow">YOUR MOVIE NIGHT, SORTED</span><h2>Good seats. Great stories.<br>Zero hassle.</h2><p>Book your tickets in just a few simple steps.</p></div>
        <div class="promo-icon"><i class="bi bi-ticket-perforated-fill"></i></div>
      </div>
    </section>
  `
})
export class HomeComponent {
  readonly store = inject(DemoStoreService);
}
