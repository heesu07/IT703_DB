
using System.Diagnostics;
using IT703_Assignment2.Models;
using Microsoft.AspNetCore.Mvc;
using IT703_Assignment2.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

        //public async Task<IActionResult> Index()
        //{

        //if (TempData["chinn"] != null) 
        //{
        //   List<Room> rooms = new List<Room>();

        //       ViewBag.possible = TempData["possible"];            
        //       ViewBag.price = TempData["price"];


        //       ViewBag.chin = TempData["chinn"];
        //       ViewBag.chout = TempData["choutt"];
        //       ViewBag.adu = TempData["adult"];
        //       ViewBag.chi = TempData["children"];
        //       ViewBag.total = await _context.Rooms.CountAsync();

        //        return View();
        // }

        //     return View();
        //}


        public async Task<IActionResult> Index(IFormCollection collection)
        {
            if (collection.Count > 0)
            {
                var checkin = collection["checkin"];
                var checkout = collection["checkout"];
                var adult = collection["adult"];


                int total = int.Parse(adult); // + int.Parse(children);
                DateTime ci = Convert.ToDateTime(checkin);
                DateTime co = Convert.ToDateTime(checkout);
                ViewBag.chin = checkin;
                ViewBag.chout = checkout;
                ViewBag.adu = int.Parse(adult);


                if (ci >= co || total <= 0)
                    return RedirectToAction(nameof(Index));

                List<Room> reserveRooms = new List<Room>();
                var listAll = _context.Bookings.ToList();
                List<Booking> checkList = new List<Booking>();
                TimeSpan day = (co - ci);
                int totalday = day.Days;
                DateTime i, o;

                foreach (Booking booking in listAll)
                {
                    i = booking.CheckIn;
                    o = booking.CheckOut;
                    if (i <= ci && o >= co)
                    {
                        checkList.Add(booking);
                        continue;
                    }
                    else if (i >= ci && i < co)
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

                List<Room> twobed = new List<Room>();
                List<Room> onebed = new List<Room>();
                foreach (string id in ids)
                {
                    Room room = _context.Rooms.SingleOrDefault(r => r.RoomID == id);
                    if (room.RoomType == roomType.TwoBedRooms)
                    {
                        twobed.Add(room);
                    }
                    else
                    {
                        onebed.Add(room);
                    }
                }
                if (total <= 2)
                {
                    if (onebed.Count > 0)
                    {
                        reserveRooms = onebed;

                    }

                }
                else
                {
                    if (twobed.Count > 0)
                    {
                        reserveRooms = twobed;

                    }
                }

                if (reserveRooms.Count > 0)
                {
                    foreach (Room v in reserveRooms)
                    {
                        if (v.RoomType == roomType.Single)
                        {
                            v.Price = 100;
                        }
                        else if (v.RoomType == roomType.Superior)
                        {
                            v.Price = 150;
                        }
                        else if (v.RoomType == roomType.TwoBedRooms)
                        {
                            v.Price = 200;
                        }
                    }


                }

                if (reserveRooms == null)
                {
                    ViewBag.total = "0";
                    ViewBag.cc = int.Parse("0");
                    ViewBag.result = " Sorry There are No available Room";
                    return View(await _context.Rooms.ToListAsync());
                }

                ViewBag.cc = int.Parse("1");
                ViewBag.total = reserveRooms.Count;
                reserveRooms.Sort((x, y) => x.RoomNum.CompareTo(y.RoomNum));
                return View(reserveRooms);
            }
            return View(await _context.Rooms.ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
