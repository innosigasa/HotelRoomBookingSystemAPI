using System;
using System.Collections.Generic;

#nullable disable

namespace HotelRoomBookingSystemAPI.Models
{
    public partial class Room
    {
        public Room()
        {
            Bookings = new HashSet<Booking>();
        }

        public int RoomsId { get; set; }
        public int RoomsNo { get; set; }
        public int RoomFloor { get; set; }
        public int RoomsTypeId { get; set; }

        public virtual RoomsType RoomsType { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
