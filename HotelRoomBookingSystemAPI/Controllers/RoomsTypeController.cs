using HotelRoomBookingSystemAPI.Models;
using HotelRoomBookingSystemAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HotelRoomBookingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    public class RoomsTypeController : Controller
    {
        private IHotelDBServices<Room> roomService = new HotelDBServices<Room>();
        private IHotelDBServices<RoomsType> roomsTypeService = new HotelDBServices<RoomsType>();
        public RoomsTypeController(IHotelDBServices<RoomsType> RoomsTypeService)
        {
            roomsTypeService = RoomsTypeService;
        }

        // GET: GuestController
        [HttpGet]
        public ActionResult GetBookings()
        {
            List<RoomsType> roomsTypeData = roomsTypeService.GetAllRows();
            
            return Ok(roomsTypeData);
        }

        // GET: GuestController/Details/5
        [HttpGet("Details")]
        public ActionResult Details(RoomsType roomType)
        {
            roomType = roomsTypeService.GetRowById(roomType.RoomsTypeId);
            return Ok(roomType);
        }

        // GET: GuestController/Create
        [HttpPost]
        public ActionResult Create([FromBody] RoomsType roomType)
        {
            if (roomsTypeService.AddRow(roomType) != null)
            {
                return Ok(roomType);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: GuestController/Create
        [HttpPut]
        public ActionResult Edit([FromBody] RoomsType roomType)
        {
            if (roomsTypeService.UpdateRow(roomType.RoomsTypeId, roomType))
            {
                return Ok(roomType);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        public ActionResult Delete([FromBody] RoomsType roomType)
        {
            try
            {
                roomsTypeService.DeleteRow(roomType);
                return Ok(roomType);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
