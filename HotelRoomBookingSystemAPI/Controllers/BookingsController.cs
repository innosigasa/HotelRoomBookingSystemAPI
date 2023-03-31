using HotelRoomBookingSystemAPI.DataAccess;
using HotelRoomBookingSystemAPI.Models;
using HotelRoomBookingSystemAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HotelRoomBookingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    public class BookingsController : Controller
    {
        private IHotelDBServices<Booking> bookingService;
        private IHotelDBServices<Payment> paymentService = new HotelDBServices<Payment>();
        private IHotelDBServices<Room> roomService = new HotelDBServices<Room>();
        private IHotelDBServices<RoomsType> roomsTypeService = new HotelDBServices<RoomsType>();
        private IHotelDBServices<Guest> guestService = new HotelDBServices<Guest>();

        private HotelDbContext HotelDbContext = new HotelDbContext();
        public BookingsController(IHotelDBServices<Booking> BookingService)
        {
            bookingService = BookingService;
        }

        // GET: GuestController
        [HttpGet]
        public ActionResult GetBookings()
        {
            List<Booking> bookingsData = bookingService.GetAllRows();
            var paymentData = paymentService.GetAllRows();
            for(int i = 0; i < bookingsData.Count; i++) 
            {
                List<Payment> paymentList = paymentData.FindAll(b=> b.BookingsId == bookingsData[i].BookingsId);
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
            List<Payment> paymentList = paymentService.GetAllRows().FindAll(b => b.BookingsId == booking.BookingsId);
            booking.Payments = paymentList;
            booking.Rooms = roomService.GetRowById(booking.RoomsId);
            booking.Guest = guestService.GetRowById(booking.GuestId);
            return Ok(booking);
        }

        // GET: GuestController/Create
        [HttpPost]
        public ActionResult Create([FromBody] Booking booking)
        {
            if(bookingService.AddRow(booking) != null)
            {
                int numberOfDays = (int)(int)(booking.DateTo - booking.DateFrom).TotalDays;
                Room roomIs = roomService.GetRowById(booking.RoomsId);
                RoomsType roomsTypeIs = roomsTypeService.GetRowById(roomIs.RoomsTypeId);
                Payment payment = new Payment()
                {
                    AmountsPaid = (decimal)(numberOfDays * roomsTypeIs.RoomsRate),
                    BookingsId = booking.BookingsId
                };
                paymentService.AddRow(payment);
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
            if (bookingService.UpdateRow(booking.BookingsId,booking))
            {
                List<Payment> paymentsList = paymentService.GetAllRows();
                int numberOfDays = (int)(int)(booking.DateTo - booking.DateFrom).TotalDays;
                Room roomIs = roomService.GetRowById(booking.RoomsId);
                RoomsType roomsTypeIs = roomsTypeService.GetRowById(roomIs.RoomsTypeId);
                Payment payment = paymentsList.Find(x=> x.BookingsId == booking.BookingsId);
                if (payment != null)
                {
                    payment.AmountsPaid = numberOfDays * roomsTypeIs.RoomsRate;
                    paymentService.UpdateRow(payment.PaymentsId, payment);
                }
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
