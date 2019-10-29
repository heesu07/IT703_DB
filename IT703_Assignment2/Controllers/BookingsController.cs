using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IT703_Assignment2.Data;
using IT703_Assignment2.Models;

namespace IT703_Assignment2.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {

            var data = await _context.Bookings.Include(a => a.Rooms).ToListAsync();

            return View(data);
        }


        public async Task<IActionResult> Customer(string id)
        {
            if (id != null)
            {

                var book = await _context.Bookings.Include(a => a.Rooms).SingleOrDefaultAsync(a => a.ReferenceNum == id);
                if (book != null)
                {
                    ViewBag.result = "There are no Booking ReferenceNumber in Database";
                }

                var room = _context.Rooms.Single(a => a.RoomID == book.RoomID);
                ViewBag.num = room.RoomNum;
                ViewBag.type = room.RoomType;

                return View(book);
            }

            return View();
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .FirstOrDefaultAsync(m => m.ReferenceNum == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string chin, string chout, string roomid, bool input, string fn, string ln, string em, string ph, bool pk,
            string ad, string ref1)
        {
            if (ref1 != null)
            {


                var book1 = await _context.Bookings.SingleOrDefaultAsync(a => a.ReferenceNum == ref1);
                book1.Paid = true;
                var room1 = _context.Rooms.Single(a => a.RoomID == book1.RoomID);
                ViewBag.num = room1.RoomNum;
                ViewBag.type = room1.RoomType;
                _context.Bookings.Update(book1);
                await _context.SaveChangesAsync();
              //  return RedirectToAction(nameof(Index));
                return View(book1);

            }

            var roominfo = await _context.Rooms.SingleOrDefaultAsync(a => a.RoomID == roomid);
            var total = 0;
            var num = 0;
            if (roominfo.RoomType == roomType.Single)
            {
                total = 100;
                num = 2;
            }
            else if (roominfo.RoomType == roomType.Superior)
            {
                total = 150;
                num = 2;
            }
            else if (roominfo.RoomType == roomType.TwoBedRooms)
            {
                total = 200;
                num = 4;
            }

            Guid guid = Guid.NewGuid();
            string str = guid.ToString();



            var booking = new Booking
            {
                FirstName = fn,
                LastName = ln,
                Phone = ph,
                RoomID = roomid,
                Address = ad,
                Email = em,
                Paid = false,
                TotalFee = total,
                CheckIn = Convert.ToDateTime(chin),
                CheckOut = Convert.ToDateTime(chout),
                CreatedAt = DateTime.Now,
                ReferenceNum = str,
                NumGuest = num,
                ParkingLot = pk,


            };

            _context.Add(booking);
            await _context.SaveChangesAsync();
            //if (ModelState.IsValid)
            //{
            //    booking.CreatedAt = DateTime.Now;
            //    _context.Add(booking);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}

            var room = _context.Rooms.Single(a => a.RoomID == roomid);
            ViewBag.num = room.RoomNum;
            ViewBag.type = room.RoomType;

            var book = await _context.Bookings.Include(a => a.Rooms).SingleOrDefaultAsync(a => a.ReferenceNum == str);
            return View(book);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ReferenceNum,RoomID,CreatedAt,CheckIn,CheckOut,NumGuest,TotalFee,Paid,FirstName,MiddleName,LastName,Email,Phone,Address,City,Notes")] Booking booking)
        {
            if (id != booking.ReferenceNum)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.ReferenceNum))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .FirstOrDefaultAsync(m => m.ReferenceNum == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(string id)
        {
            return _context.Bookings.Any(e => e.ReferenceNum == id);
        }
    }
}
