# Movie Ticket Booking – Angular Frontend

A responsive movie-ticket-booking UI built with Angular 19, Bootstrap 5 and Bootstrap Icons.

## Included screens

### Customer
- Home / Now Showing movie catalogue
- Movie details and showtime selection
- Interactive seat selection and booking summary
- Sign in / Create account UI
- Booking confirmation screen

### Admin
- Dashboard overview
- Manage Movies
- Manage Theatres
- Manage Screens
- Manage Seats
- Manage Shows
- Manage Bookings

## Requirements
- Node.js 20.11.1+ (Node 22 LTS recommended)
- npm
- Visual Studio Code

## Run in VS Code
1. Extract this ZIP.
2. Open the `MovieTicketBooking-Frontend` folder in VS Code.
3. Open Terminal → New Terminal.
4. Run `npm install`.
5. Run `npm start`.
6. Open `http://localhost:4200`.

## Current stage: frontend UI first
This package currently uses local demo data so the screens can be tested independently.
It is intentionally **not connected to the ASP.NET Core backend yet**. API integration is the next step.
Do not change your backend project while testing this frontend.

## Backend integration notes
The backend base URL observed in the supplied project is:
- `https://localhost:7205`
- `http://localhost:5101`

The API service placeholder is in `src/app/core/services/api.service.ts`.
During integration we will:
1. Confirm the backend is running and Swagger endpoints.
2. Configure CORS in the backend for `http://localhost:4200`.
3. Map the actual response/request DTOs to Angular models.
4. Replace demo data with HttpClient calls.
5. Connect JWT login, role-based admin routes, movie/show/seat/booking flows.

The admin pages in this UI are for layout and workflow preview only. Real admin access must be enforced by the backend using authorization.
