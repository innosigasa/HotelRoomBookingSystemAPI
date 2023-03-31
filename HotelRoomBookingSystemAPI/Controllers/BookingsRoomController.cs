using HotelRoomBookingSystemAPI.Models;
using HotelRoomBookingSystemAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HotelRoomBookingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    public class BookingsRoomController : Controller
    {

        private IHotelDBServices<Booking> bookingService;
        private IHotelDBServices<Payment> paymentService = new HotelDBServices<Payment>();
        private IHotelDBServices<Room> roomService = new HotelDBServices<Room>();
        private IHotelDBServices<RoomsType> roomsTypeService = new HotelDBServices<RoomsType>();
        private IHotelDBServices<Guest> guestService = new HotelDBServices<Guest>();

        public BookingsRoomController(IHotelDBServices<Booking> BookingService)
        {
            bookingService = BookingService;
        }

        // GET: GuestController
        [HttpGet]
        public ActionResult GetBookings()
        {
            List<Booking> bookingsData = bookingService.GetAllRows();
            var paymentData = paymentService.GetAllRows();
            for (int i = 0; i < bookingsData.Count; i++)
            {
                List<Payment> paymentList = paymentData.FindAll(b => b.BookingsId == bookingsData[i].BookingsId);
                bookingsData[i].Payments = paymentList;
                bookingsData[i].Rooms = roomService.GetRowById(bookingsData[i].RoomsId);
                bookingsData[i].Guest = guestService.GetRowById(bookingsData[i].GuestId);
            }
            return Ok(bookingsData);
        }

        // GET: GuestController/Details/5
        [HttpGet("Details")]
        public ActionResult Details(Booking booking)
        {
            booking = bookingService.GetRowById(booking.BookingsId);
            List<Booking> bookingList = bookingService.GetAllRows();
            List<Room> roomList = roomService.GetAllRows();
            foreach(Booking myBooking in bookingList)
            {
                if(booking.DateFrom.Ticks>myBooking.DateFrom.Ticks && booking.DateFrom.Ticks < myBooking.DateTo.Ticks)
                {
                    var item = roomList.Find(x => x.RoomsId == myBooking.RoomsId);
                    roomList.Remove(item);
                }
            }
            
            return Ok(roomList);
        }

        // GET: GuestController/Create
        [HttpPost]
        public ActionResult Create([FromBody] Booking booking)
        {
            if (bookingService.AddRow(booking) != null)
            {
                return Ok(booking);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: GuestController/Create
        [HttpPut]
        public ActionResult Edit([FromBody] Booking booking)
        {
            if (bookingService.UpdateRow(booking.BookingsId, booking))
            {
                return Ok(booking);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        public ActionResult Delete([FromBody] Booking booking)
        {
            try
            {
                bookingService.DeleteRow(booking);
                return Ok(booking);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
