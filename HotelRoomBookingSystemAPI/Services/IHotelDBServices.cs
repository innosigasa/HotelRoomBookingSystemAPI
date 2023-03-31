using System.Collections.Generic;

namespace HotelRoomBookingSystemAPI.Services
{
    public interface IHotelDBServices<T>  where T : class
    {
        T AddRow(T entity);
        bool UpdateRow(int id,T entity);
        void DeleteRow(T entity);
        List<T> GetAllRows();
        T GetRowById(int id);
    }
}
