using HotelRoomBookingSystemAPI.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HotelRoomBookingSystemAPI.Services
{
    public class HotelDBServices<T> : IHotelDBServices<T> where T : class
    {
        private DataAccessContext context = new DataAccessContext();
        public T AddRow(T entity)
        {
            try
            {
                context.Add(entity);
                context.SaveChanges();
                return entity;
            }
            catch
            {
                return null;
            }
        }

        public void DeleteRow(T entity)
        {
            context.Remove(entity);
            context.SaveChanges();
        }

        public List<T> GetAllRows()
        {
            return context.Set<T>().ToList();
        }

        public T GetRowById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public bool UpdateRow(int id , T entity)
        {
            var oldEntity = context.Set<T>().Find(id);
            if(oldEntity != null)
            {
                context.Entry(oldEntity).State = EntityState.Detached;
                context.Update(entity);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
