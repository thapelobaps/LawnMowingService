using System.ComponentModel.DataAnnotations;

namespace LawnMowingBookingService.Models
{
    public partial class Booking
    {
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; } // Make it required if necessary

        [Required]
        public int MachineId { get; set; } // Make it required if necessary

        [Required]
        public DateOnly BookingDate { get; set; } // Make it required if necessary

        public bool IsAcknowledged { get; set; } // Default to false if not specified

        public virtual ICollection<BookingConflict> BookingConflicts { get; set; } = new List<BookingConflict>();

        public virtual Customer Customer { get; set; }

        public virtual Machine Machine { get; set; }
    }
}

