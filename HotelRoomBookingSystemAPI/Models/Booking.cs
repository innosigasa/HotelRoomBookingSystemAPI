using System;
using System.Collections.Generic;

#nullable disable

namespace HotelRoomBookingSystemAPI.Models
{
    public partial class Booking
    {
        public Booking()
        {
            Payments = new HashSet<Payment>();
        }

        public int BookingsId { get; set; }
        public int GuestId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int RoomsId { get; set; }

        public virtual Guest Guest { get; set; }
        public virtual Room Rooms { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
