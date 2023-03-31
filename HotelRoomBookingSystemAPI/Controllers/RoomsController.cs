using HotelRoomBookingSystemAPI.DataAccess;
using HotelRoomBookingSystemAPI.Models;
using HotelRoomBookingSystemAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HotelRoomBookingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    public class RoomsController : Controller
    {
        private IHotelDBServices<Room> roomService = new HotelDBServices<Room>();
        private IHotelDBServices<RoomsType> roomsTypeService = new HotelDBServices<RoomsType>();
        public RoomsController(IHotelDBServices<Room> RoomsService)
        {
            roomService = RoomsService;
        }

        // GET: GuestController
        [HttpGet]
        public ActionResult GetBookings()
        {
            List<Room> roomsData = roomService.GetAllRows();
            List<RoomsType> roomsTypeData = roomsTypeService.GetAllRows();
            for (int i = 0; i < roomsData.Count; i++)
            {
                RoomsType roomTypeList = roomsTypeData.Find(rt=> rt.RoomsTypeId == roomsData[i].RoomsTypeId);
                roomsData[i].RoomsType = roomTypeList;
            }
            return Ok(roomsData);
        }

        // GET: GuestController/Details/5
        [HttpGet("Details")]
        public ActionResult Details(Room room)
        {
            room = roomService.GetRowById(room.RoomsId);
            room.RoomsType = roomsTypeService.GetRowById(room.RoomsTypeId);
            return Ok(room);
        }

        // GET: GuestController/Create
        [HttpPost]
        public ActionResult Create([FromBody] Room room)
        {
            if (roomService.AddRow(room) != null)
            {
                return Ok(room);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: GuestController/Create
        [HttpPut]
        public ActionResult Edit([FromBody] Room room)
        {
            if (roomService.UpdateRow(room.RoomsId, room))
            {
                return Ok(room);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        public ActionResult Delete([FromBody] Room room)
        {
            try
            {
                roomService.DeleteRow(room);
                return Ok(room);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
