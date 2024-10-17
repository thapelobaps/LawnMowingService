using System;
using System.Collections.Generic;

namespace LawnMowingBookingService.Models;

public partial class Machine
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool? IsAvailable { get; set; }

    public int? OperatorId { get; set; }

    public virtual ICollection<BookingConflict> BookingConflicts { get; set; } = new List<BookingConflict>();

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Operator? Operator { get; set; }

    public virtual ICollection<Operator> Operators { get; set; } = new List<Operator>();
}
