using System;
using System.Collections.Generic;

#nullable disable

namespace HotelRoomBookingSystemAPI.Models
{
    public partial class Payment
    {
        public int PaymentsId { get; set; }
        public int BookingsId { get; set; }
        public decimal AmountsPaid { get; set; }

        public virtual Booking Bookings { get; set; }
    }
}
