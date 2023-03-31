using System;
using System.Collections.Generic;

#nullable disable

namespace HotelRoomBookingSystemAPI.Models
{
    public partial class RoomsType
    {
        public RoomsType()
        {
            Rooms = new HashSet<Room>();
        }

        public int RoomsTypeId { get; set; }
        public string RoomsType1 { get; set; }
        public string Description { get; set; }
        public decimal RoomsRate { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
