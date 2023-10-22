Tech test booking system. Have invited Ryan and Thomas as instructed.  

Implementation complete.

# Implmented items

- Overall framework: all repositories and services are fully interface supporting to allow for future mocking 
- DB Context: Migrations with seed data complete. (Would not usually put full user seed data in code)
- Repositories and interfaces for user and booking
- Bookings view: restricted to admin only through @Authorize attribute
- Booking view: Validation protected form used for both making a booking and editing an existing booking
- Login page: Login system without IdentityServer provided for the example here.
- Admin restrictions: I didn't think you wanted to see me implement IdentityServer so created a raw authentication system that is access via JS and API
- Confirm booking: Functionality for admins to confirm and delete bookings implemented at the list level.

# Accessing the system

User details as follows:

- Email address: dj@djvaleting.com
- Password: UP8UHoq&qa!$r^V2
