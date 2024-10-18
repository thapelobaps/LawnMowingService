using LawnMowingBookingService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Linq;


namespace LawnMowingBookingService.Controllers
{
    public class BookingController : Controller
    {
        private readonly LawnMowingDbContext _context;

        public BookingController(LawnMowingDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult BookMachine()
        {
            var machines = _context.Machines.Where(m => m.IsAvailable ?? false).ToList();
            return View(machines);
        }

        [HttpPost]
        public async Task<IActionResult> BookMachine(int machineId, DateTime bookingDate)
        {
            var machine = await _context.Machines.FindAsync(machineId);
            if (machine != null)
            {
                var booking = new Booking
                {
                    MachineId = machineId,
                    CustomerId = int.Parse(User.FindFirst("UserId").Value), // Convert string to int
                    BookingDate = DateOnly.FromDateTime(bookingDate), // Convert DateTime to DateOnly
                    IsAcknowledged = false
                };
                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();

                return RedirectToAction("BookingSuccess");
            }
            return View();
        }

        public IActionResult BookingSuccess()
        {
            return View();
        }

        public async Task<IActionResult> AcknowledgeBooking(int bookingId)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking != null)
            {
                booking.IsAcknowledged = true;
                _context.Bookings.Update(booking);
                await _context.SaveChangesAsync();

                return View(booking);
            }
            return NotFound();
        }

        public IActionResult HandleConflict(int bookingId)
        {
            var booking = _context.Bookings.Find(bookingId);
            if (booking != null)
            {
                return View(booking);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> HandleConflict(int bookingId, DateTime newBookingDate)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking != null)
            {
                // Logic to find another available machine
                var newMachine = _context.Machines.FirstOrDefault(m => m.IsAvailable ?? false);
                if (newMachine != null)
                {
                    booking.MachineId = newMachine.Id;
                    booking.BookingDate = DateOnly.FromDateTime(newBookingDate); // Convert DateTime to DateOnly
                    _context.Bookings.Update(booking);
                    await _context.SaveChangesAsync();

                    return View("ConflictResolved", newMachine);
                }
            }
            return NotFound();
        }
    }
}
