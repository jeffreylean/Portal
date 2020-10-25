using System;
using System.Linq;
using Portal.Models;
using Portal.Context;
using System.Linq;
using System.Collections.Generic;

namespace Portal.Services
{
    public interface ICalendarService
    {
        public void InsertNewEvent(CalendarInfo data);
        public List<CalendarInfo> GetEventList();
        public void UpdateEvent(CalendarInfo data);
        public void DeleteEvent(CalendarInfo data);
    }

    public class CalendarService : ICalendarService
    {
        private readonly PortalDbContext _dbContext;
        public CalendarService(PortalDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void InsertNewEvent(CalendarInfo data)
        {
            _dbContext.CalendarInfos.Add(data);
            _dbContext.SaveChanges();

        }
        public List<CalendarInfo> GetEventList()
        {
            //using(var dbcontext = new PortalDbContext())
            //{
            //    var data = dbcontext.CalendarInfos.Where(s => s.Event=="test").ToList();
            //    return data;
            //}
            var data = _dbContext.CalendarInfos.Select(s => s).ToList();
            return data;
        }
        public void UpdateEvent(CalendarInfo data)
        {
             _dbContext.CalendarInfos.Update(data);
            _dbContext.SaveChanges();
        }
        public void DeleteEvent(CalendarInfo data)
        {
            _dbContext.CalendarInfos.Remove(data);
            _dbContext.SaveChanges();
        }
    }
}
