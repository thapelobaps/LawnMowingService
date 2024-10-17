LawnMowingBookingService
│
├── Controllers
│   ├── AccountController.cs
│   ├── BookingController.cs
│   └── HomeController.cs
│
├── Models
│   ├── Booking.cs
│   ├── BookingConflict.cs
│   ├── ConflictManager.cs
│   ├── Customer.cs
│   ├── Machine.cs
│   └── Operator.cs
│
├── ViewModels
│   ├── LoginViewModel.cs
│   └── RegisterViewModel.cs
│
├── Views
│   ├── Account
│   │   ├── Login.cshtml
│   │   └── Register.cshtml
│   ├── Booking
│   │   ├── BookMachine.cshtml
│   │   ├── BookingSuccess.cshtml
│   │   ├── AcknowledgeBooking.cshtml
│   │   ├── HandleConflict.cshtml
│   │   └── ConflictResolved.cshtml
│   └── Home
│       └── Index.cshtml
│
└── Program.cs
