export interface Movie {
  movieId: number;
  title: string;
  genre: string;
  language: string;
  durationMinutes: number;
  releaseDate: string;
  description: string;
  rating: string;
  poster: string;
  status: 'Now Showing' | 'Coming Soon';
}
