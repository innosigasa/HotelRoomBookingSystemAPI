using System.Data.Common;
using System.Data.Entity;
using HotelRoomBookingSystemAPI.Models;

namespace HotelRoomBookingSystemAPI.DataAccess
{
    public class HotelDbContext:DbContext
    {
        public HotelDbContext()
            : base("ConnectionStrings")
        {

        }

        public HotelDbContext(DbConnection connection)
        {

        }

        public virtual DbSet<Booking> Booking { get; set; }
        public virtual DbSet<Guest> Guest { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<RoomsType> RoomsType { get; set; }


    }
}
