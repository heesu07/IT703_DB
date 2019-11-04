using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IT703_Assignment2.Data;
using IT703_Assignment2.Models;
using System.Collections.Generic;

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

            var data = await _context.Bookings.ToListAsync();

            return View(data);
        }

        // GET: Bookings
        public async Task<IActionResult> Index2()
        {

            var data = await _context.Bookings.Include(a => a.Rooms).ToListAsync();
            var room = await _context.Rooms.ToListAsync();


            //var room = from n in _context.Rooms
            //                  select n;

            //var data = from n in _context.Bookings
            //            select n;

            var tuple = new Tuple<List<Booking> , List<Room>>(data, room);



            return View(tuple);
        }


        public async Task<IActionResult> Customer(string id,int food,int num1, bool pay)
        {
            if (id != null)
            {
                int sum = 0;
                var book = await _context.Bookings.SingleOrDefaultAsync(a => a.ReferenceNum == id);

                if (book == null)
                {
                    ViewBag.result = "There are no Booking ReferenceNumber in Database";
                    return View();
                }

                if(food > 0 && num1 > 0)
                {
                    sum = food * num1;
                    book.restaurantFee += sum;
                    _context.Bookings.Update(book);
                }

                if (pay)
                {
                    book.Paid = true;
                    _context.Bookings.Update(book);
                }
              
                var room = _context.Rooms.Single(a => a.RoomID == book.RoomID);
                ViewBag.num = room.RoomNum;
                ViewBag.type = room.RoomType;
                ViewBag.total = book.restaurantFee + book.RoomFee;
                _context.SaveChanges();
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
            string ad, string ref1, int food, int num1, bool pay)
        {
            if (ref1 != null)
            {

                var book1 = await _context.Bookings.SingleOrDefaultAsync(a => a.ReferenceNum == ref1);
                int sum = 0;

                if (food >0 && num1 >0) 
                {
                    sum = food * num1;

                    book1.restaurantFee += sum;
                    var room1 = _context.Rooms.Single(a => a.RoomID == book1.RoomID);
                    ViewBag.num = room1.RoomNum;
                    ViewBag.type = room1.RoomType;
                    ViewBag.total = book1.restaurantFee + book1.RoomFee;
                    _context.Bookings.Update(book1);                   
                }

                if (pay)
                {

                    book1.Paid = true;
                    var room1 = _context.Rooms.Single(a => a.RoomID == book1.RoomID);
                    ViewBag.num = room1.RoomNum;
                    ViewBag.type = room1.RoomType;
                    ViewBag.total = book1.restaurantFee + book1.RoomFee;
                    _context.Bookings.Update(book1);                                    
                }

                await _context.SaveChangesAsync();
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
           
            var days = (Convert.ToDateTime(chout) - Convert.ToDateTime(chin)).Days;
            
              
            var booking = new Booking
            {
                FirstName = fn,
                LastName = ln,
                Phone = ph,
                RoomID = roomid,
                Address = ad,
                Email = em,
                Paid = false,
                RoomFee = total * days,
                CheckIn = Convert.ToDateTime(chin),
                CheckOut = Convert.ToDateTime(chout),
                //CheckStatus = CheckState.Reserve,
                CreatedAt = DateTime.Now,
                ReferenceNum = str,
                NumGuest = num,
                ParkingLot = pk,
                restaurantFee = 0,


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
            ViewBag.total = book.restaurantFee + book.RoomFee;
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
        public async Task<IActionResult> Edit(string id, [Bind("ReferenceNum,RoomID,CreatedAt,CheckIn,CheckOut,NumGuest,RoomFee,restaurantFee,Paid,FirstName,MiddleName,LastName,Email,Phone,Address,City,Notes")] Booking booking)
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
                return RedirectToAction(nameof(Index2));
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
            return RedirectToAction(nameof(Index2));
        }

        private bool BookingExists(string id)
        {
            return _context.Bookings.Any(e => e.ReferenceNum == id);
        }
    }
}
