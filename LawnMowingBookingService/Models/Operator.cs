using System;
using System.Collections.Generic;

namespace LawnMowingBookingService.Models;

public partial class Operator
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public int? AssignedMachineId { get; set; }

    public virtual Machine? AssignedMachine { get; set; }

    public virtual ICollection<Machine> Machines { get; set; } = new List<Machine>();
}
