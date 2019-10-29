
using System.Diagnostics;
using IT703_Assignment2.Models;
using Microsoft.AspNetCore.Mvc;
using IT703_Assignment2.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System;
using System.Collections.Generic;

namespace IT703_Assignment2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

             if (TempData["reserve"] != null) 
             {

                    ViewBag.possible = TempData["possible"];
                    ViewBag.reserve = TempData["reserve"];
                    //ViewBag.price = TempData["price"];
                    List<Room> rooms = TempData.Get<List<Room>>("reserve");
                    ViewBag.chin = TempData["chinn"];
                    ViewBag.chout = TempData["choutt"];
                    ViewBag.adu = TempData["adult"];
                    //ViewBag.chi = TempData["children"];
                    //ViewBag.total = await _context.Rooms.CountAsync();
                    //var room = await _context.Rooms.SingleOrDefaultAsync(a => a.RoomID == id);
                     return View(rooms);
             }

             return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IFormCollection collection)
        {
            var checkin = collection["checkin"];
            var checkout = collection["checkout"];
            var adult = collection["adult"];
            //var children = collection["children"];
            int total = int.Parse(adult); // + int.Parse(children);
            DateTime ci = Convert.ToDateTime(checkin);
            DateTime co = Convert.ToDateTime(checkout);
            TempData["chinn"] = checkin.ToString();
            TempData["choutt"] = checkout.ToString();
            TempData["adult"] = adult.ToString();
            //TempData["children"] = children.ToString();

            if(ci >= co || total <= 0)
                return RedirectToAction(nameof(Index));

            
            var listAll = _context.Bookings.ToList();
            List<Booking> checkList = new List<Booking>();
            TimeSpan day = (co - ci);
            int totalday = day.Days;            
            DateTime i, o;            

            foreach (Booking booking in listAll)
            {
                i = booking.CheckIn;
                o = booking.CheckOut;
                if(i<= ci && o >= co)
                {
                    checkList.Add(booking);
                    continue;
                }
                else if(i >= ci && i < co)
                {
                    checkList.Add(booking);
                    continue;
                }
                else if (o > ci && o <= co)
                {
                    checkList.Add(booking);
                    continue;
                }
            }

            var rooms = _context.Rooms.ToList();
            
            HashSet<String> ids = new HashSet<string>();
            foreach (Room r in rooms)
            {
                ids.Add(r.RoomID);
            }
            
            foreach (Booking b in checkList)
            {                
                ids.Remove(b.RoomID);                             
            }

            
            List<Room> vacantList = new List<Room>();
            foreach (string id in ids)
            {
                var r = _context.Rooms.SingleOrDefault(a => a.RoomID == id);
                vacantList.Add(r);
            }
            //vacantList.ToAsyncEnumerable();
            if (vacantList.Count > 0)
            {                
                TempData["possible"] = "true";
                //TempData["reserve"] = vacantList;
                TempData.Put("reserve", vacantList);

                return RedirectToAction(nameof(Index));
           
            }
            else 
            {
                TempData["possible"] = "false";
                TempData["reserve"] = null;

                return RedirectToAction(nameof(Index));
            }
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
        public async Task<IActionResult> Create([Bind("ReferenceNum,RoomID,CreatedAt,CheckIn,CheckOut,NumGuest,TotalFee,Paid,FirstName,MiddleName,LastName,Email,Phone,Address,City,Notes")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                booking.CreatedAt = DateTime.Now;
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(booking);
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
