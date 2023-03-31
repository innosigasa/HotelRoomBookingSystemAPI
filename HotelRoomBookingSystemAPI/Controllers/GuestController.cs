using HotelRoomBookingSystemAPI.DataAccess;
using HotelRoomBookingSystemAPI.Models;
using HotelRoomBookingSystemAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace HotelRoomBookingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    public class GuestController : Controller
    {

        private IHotelDBServices<Guest> guestService;
        private IHotelDBServices<Booking> bookingService = new HotelDBServices<Booking>();
        private IHotelDBServices<Payment> paymentService = new HotelDBServices<Payment>();
        private IHotelDBServices<Room> roomService = new HotelDBServices<Room>();
        private IHotelDBServices<RoomsType> roomsTypeService = new HotelDBServices<RoomsType>();
        private HotelDbContext HotelDbContext = new HotelDbContext();
        public GuestController(IHotelDBServices<Guest> GuestService)
        {
            guestService = GuestService;
        }

        // GET: GuestController
        [HttpGet]
        public ActionResult GetGuests()
        {
            var guestData = guestService.GetAllRows();
            List<Booking> bookingList = bookingService.GetAllRows();
            try
            {
                for(int count =0;count<guestData.Count;count++) 
                {
                    List<Booking> nList = bookingList.FindAll(b => b.GuestId == guestData[count].GuestId);
                    foreach(Booking booking in nList)
                    {
                        booking.Payments.Add(paymentService.GetRowById(booking.BookingsId));
                        booking.Rooms = roomService.GetRowById(booking.RoomsId);
                        booking.Rooms.RoomsType = roomsTypeService.GetRowById(booking.Rooms.RoomsTypeId);
                    }
                    guestData[count].Bookings = nList;
                }
            }
            catch(NullReferenceException e)
            {
                System.Console.WriteLine(e.Message);
            }

            return Ok(guestData);
        }

        // GET: GuestController/Details/5
        [HttpGet("Details")]
        public ActionResult Details(Guest guest)
        {
            guest = guestService.GetRowById(guest.GuestId);
            
            return Ok(guest);
        }

        // GET: GuestController/Create
        [HttpPost]
        public ActionResult Create([FromBody] Guest guest)
        {
            if(guestService.AddRow(guest) != null)
            {
                return Ok(guest);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: GuestController/Create
        [HttpPut]
        public ActionResult Edit([FromBody] Guest guest)
        {
            if (guestService.UpdateRow(guest.GuestId,guest))
            {
                return Ok(guest);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        public ActionResult Delete([FromBody] Guest guest)
        {
            try
            {
                guestService.DeleteRow(guest);
                return Ok(guest);
            }
            catch
            {
                return NotFound();
            }
        }
        
    }
}
