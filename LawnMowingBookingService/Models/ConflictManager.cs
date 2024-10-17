using System;
using System.Collections.Generic;

namespace LawnMowingBookingService.Models;

public partial class ConflictManager
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual ICollection<BookingConflict> BookingConflicts { get; set; } = new List<BookingConflict>();
}
