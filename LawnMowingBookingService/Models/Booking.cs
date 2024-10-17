using System;
using System.Collections.Generic;

namespace LawnMowingBookingService.Models;

public partial class Booking
{
    public int Id { get; set; }

    public int? CustomerId { get; set; }

    public int? MachineId { get; set; }

    public DateOnly? BookingDate { get; set; }

    public bool? IsAcknowledged { get; set; }

    public virtual ICollection<BookingConflict> BookingConflicts { get; set; } = new List<BookingConflict>();

    public virtual Customer? Customer { get; set; }

    public virtual Machine? Machine { get; set; }

    
}
