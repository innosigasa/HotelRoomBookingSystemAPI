using System;
using System.Collections.Generic;

#nullable disable

namespace HotelRoomBookingSystemAPI.Models
{
    public partial class Guest
    {
        public Guest()
        {
            Bookings = new HashSet<Booking>();
        }

        public int GuestId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
