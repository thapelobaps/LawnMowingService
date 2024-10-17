using System;
using System.Collections.Generic;

namespace LawnMowingBookingService.Models;

public partial class BookingConflict
{
    public int Id { get; set; }

    public int? OriginalBookingId { get; set; }

    public int? ReassignedMachineId { get; set; }

    public int? ConflictManagerId { get; set; }

    public DateOnly? ConflictResolvedDate { get; set; }

    public virtual ConflictManager? ConflictManager { get; set; }

    public virtual Booking? OriginalBooking { get; set; }

    public virtual Machine? ReassignedMachine { get; set; }
}
