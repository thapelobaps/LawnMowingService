using LawnMowingBookingService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;


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
                    CustomerId = int.Parse(User.FindFirst("Id").Value), // Convert string to int
                    BookingDate = DateOnly.FromDateTime(bookingDate), // Convert DateTime to DateOnly
                    IsAcknowledged = true
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


        public IActionResult HandleConflict()
        {
            return View();
        }
        //[HttpGet] [HttpPost] 
        //public async Task<IActionResult> HandleConflict(int bookingId, DateTime newBookingDate)
        //{
        //    var booking = await _context.Bookings.Include(b => b.Machine).FirstOrDefaultAsync(b => b.Id == 5);

        //    if (booking != null)
        //    {
        //        var newMachine = await _context.Machines.FirstOrDefaultAsync(m => m.IsAvailable == false);

        //        if (newMachine != null)
        //        {
        //            booking.MachineId = newMachine.Id;
        //            booking.BookingDate = DateOnly.FromDateTime(newBookingDate);
        //            booking.IsAcknowledged = true; // Ensure this is set as per your logic

        //            // Log the values being updated
        //            Console.WriteLine($"Updating Booking: Id={booking.Id}, BookingDate={booking.BookingDate}, CustomerId={booking.CustomerId}, IsAcknowledged={booking.IsAcknowledged}, MachineId={booking.MachineId}");

        //            // Update the booking
        //            _context.Bookings.Update(booking);
        //            await _context.SaveChangesAsync();

        //            return RedirectToAction("ConflictResolved", new { machineId = newMachine.Id });
        //        }
        //    }

        //    return NotFound();
        //}

    }
}
