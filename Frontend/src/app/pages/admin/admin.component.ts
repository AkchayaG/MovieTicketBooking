import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DemoStoreService } from '../../core/services/demo-store.service';

type Section = 'Overview' | 'Movies' | 'Theatres' | 'Screens' | 'Seats' | 'Shows' | 'Bookings';

@Component({
  standalone: true,
  imports: [FormsModule],
  template: `
    <div class="admin-shell">
      <aside class="admin-sidebar">
        <a class="brand-mark admin-brand" href="/admin">cine<span>book</span><small>ADMIN</small></a>
        <div class="admin-menu-label">WORKSPACE</div>
        @for (item of menu; track item.label) {
          <button class="admin-nav-item" [class.active]="active === item.label" (click)="active = item.label"><i class="bi {{ item.icon }}"></i>{{ item.label }}</button>
        }
        <div class="admin-sidebar-bottom"><div class="admin-user"><div class="avatar">AD</div><div><strong>Administrator</strong><small>Super Admin</small></div></div><a href="/" class="back-link mt-3"><i class="bi bi-box-arrow-left"></i> View customer site</a></div>
      </aside>
      <main class="admin-main">
        <div class="admin-topbar"><div><span class="text-muted small">Workspace / {{ active }}</span><h1>{{ active }}</h1></div><div class="admin-top-actions"><span class="admin-live"><i class="bi bi-circle-fill"></i> Demo mode</span><div class="avatar">AD</div></div></div>
        @if (active === 'Overview') {
          <div class="admin-welcome"><div><span class="eyebrow">TUESDAY, NOVEMBER 03</span><h2>Good morning, Admin 👋</h2><p>Here's what's happening with your cinema today.</p></div><button class="btn btn-primary rounded-pill px-3" (click)="active = 'Movies'"><i class="bi bi-plus-lg me-2"></i>Add a movie</button></div>
          <div class="row g-3 mb-4">
            @for (stat of stats; track stat.label) { <div class="col-6 col-xl-3"><div class="stat-card"><div class="stat-icon"><i class="bi {{ stat.icon }}"></i></div><small>{{ stat.label }}</small><strong>{{ stat.value }}</strong><span>{{ stat.note }}</span></div></div> }
          </div>
          <div class="admin-panel"><div class="panel-heading"><div><h3>Recent bookings</h3><p>Latest activity across your theatres</p></div><button class="btn btn-outline-light btn-sm" (click)="active = 'Bookings'">View all</button></div><div class="table-responsive"><table class="table admin-table"><thead><tr><th>Booking ID</th><th>Customer</th><th>Movie</th><th>Seats</th><th>Amount</th><th>Status</th></tr></thead><tbody>@for (b of store.bookings; track b.id) {<tr><td class="text-primary">{{ b.id }}</td><td>{{ b.customer }}</td><td>{{ b.movie }}</td><td>{{ b.seats }}</td><td>₹{{ b.amount }}</td><td><span class="status-pill" [class.cancelled]="b.status === 'Cancelled'">{{ b.status }}</span></td></tr>}</tbody></table></div></div>
        } @else if (active === 'Movies') {
          <div class="admin-panel">
            <div class="panel-heading"><div><h3>Movie catalogue</h3><p>Add and maintain the movies shown to customers.</p></div><button class="btn btn-primary btn-sm rounded-pill" (click)="showMovieForm = !showMovieForm"><i class="bi bi-plus-lg me-1"></i> Add movie</button></div>
            @if (showMovieForm) {
              <form class="admin-form row g-3 mb-4" (ngSubmit)="addMovie()">
                <div class="col-md-6"><label class="form-label">Movie title</label><input class="form-control" name="title" [(ngModel)]="movieForm.title" required></div>
                <div class="col-md-6"><label class="form-label">Genre</label><input class="form-control" name="genre" [(ngModel)]="movieForm.genre" required></div>
                <div class="col-md-6"><label class="form-label">Language</label><input class="form-control" name="language" [(ngModel)]="movieForm.language" required></div>
                <div class="col-md-6"><label class="form-label">Duration (minutes)</label><input class="form-control" type="number" name="duration" [(ngModel)]="movieForm.durationMinutes" required></div>
                <div class="col-12"><button class="btn btn-primary btn-sm">Save movie</button><button type="button" class="btn btn-outline-light btn-sm ms-2" (click)="showMovieForm = false">Cancel</button></div>
              </form>
            }
            <div class="table-responsive"><table class="table admin-table"><thead><tr><th>Movie</th><th>Genre</th><th>Language</th><th>Duration</th><th>Status</th><th>Action</th></tr></thead><tbody>@for (m of store.movies; track m.movieId) {<tr><td><strong>{{ m.title }}</strong></td><td>{{ m.genre }}</td><td>{{ m.language }}</td><td>{{ m.durationMinutes }} min</td><td><span class="status-pill">{{ m.status }}</span></td><td><button class="icon-action" title="Remove demo movie" (click)="store.removeMovie(m.movieId)"><i class="bi bi-trash3"></i></button></td></tr>}</tbody></table></div>
          </div>
        } @else if (active === 'Bookings') {
          <div class="admin-panel"><div class="panel-heading"><div><h3>All bookings</h3><p>Review booking records and their current status.</p></div></div><div class="table-responsive"><table class="table admin-table"><thead><tr><th>Booking ID</th><th>Customer</th><th>Movie</th><th>Seats</th><th>Amount</th><th>Status</th></tr></thead><tbody>@for (b of store.bookings; track b.id) {<tr><td>{{ b.id }}</td><td>{{ b.customer }}</td><td>{{ b.movie }}</td><td>{{ b.seats }}</td><td>₹{{ b.amount }}</td><td><span class="status-pill" [class.cancelled]="b.status === 'Cancelled'">{{ b.status }}</span></td></tr>}</tbody></table></div></div>
        } @else {
          <div class="admin-panel empty-admin"><div class="empty-icon"><i class="bi bi-cone-striped"></i></div><h3>{{ active }} management</h3><p>This management screen is prepared in the admin navigation. We'll connect its create, edit and delete forms to your existing {{ active.toLowerCase() }} API during backend integration.</p><div class="empty-note"><i class="bi bi-info-circle me-2"></i>Frontend preview – no data is being saved to SQL Server yet.</div></div>
        }
      </main>
    </div>
  `
})
export class AdminComponent {
  readonly store = inject(DemoStoreService);
  active: Section = 'Overview';
  showMovieForm = false;
  movieForm = { title: '', genre: '', language: 'Tamil', durationMinutes: 120 };
  menu = [
    { label: 'Overview' as Section, icon: 'bi-grid-1x2' },
    { label: 'Movies' as Section, icon: 'bi-film' },
    { label: 'Theatres' as Section, icon: 'bi-building' },
    { label: 'Screens' as Section, icon: 'bi-display' },
    { label: 'Seats' as Section, icon: 'bi-grid-3x3' },
    { label: 'Shows' as Section, icon: 'bi-calendar-event' },
    { label: 'Bookings' as Section, icon: 'bi-ticket-perforated' }
  ];
  stats = [
    { label: 'Total movies', value: '12', note: '+2 this month', icon: 'bi-film' },
    { label: 'Theatres', value: '08', note: 'Across 4 cities', icon: 'bi-building' },
    { label: 'Bookings today', value: '248', note: '+12.8% this week', icon: 'bi-ticket-perforated' },
    { label: 'Revenue today', value: '₹42.6K', note: 'Updated just now', icon: 'bi-currency-rupee' }
  ];
  addMovie(): void {
    if (!this.movieForm.title.trim()) return;
    this.store.addMovie({ ...this.movieForm, description: 'Added from the admin demo panel.', releaseDate: new Date().toISOString().slice(0, 10), poster: 'https://placehold.co/500x750/211d31/ffffff?text=New+Movie', rating: 'U/A', status: 'Now Showing' });
    this.movieForm = { title: '', genre: '', language: 'Tamil', durationMinutes: 120 };
    this.showMovieForm = false;
  }
}
