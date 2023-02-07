using ErrandPay_test.Models;

namespace ErrandPay_test.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _appDbContext;

        public EventRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public bool Add(Event eventAttr)
        {
            if (eventAttr == null)
            {
                return false;
            }
            if (_appDbContext.Events.Any(c => c.Name == eventAttr.Name))
            {
                return false;
            }

            _appDbContext.Events.Add(eventAttr);
            _appDbContext.SaveChanges();
            return true;
        }

        public bool JoinEvent(string name)
        {
            var eventAttr = _appDbContext.Events.FirstOrDefault(c => c.Name == name);
            if (eventAttr != null)
            {
                eventAttr.IsAttending = true;
                _appDbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public bool LeaveEvent(string name)
        {
            var eventAttr = _appDbContext.Events.FirstOrDefault(c => c.Name == name);
            if (eventAttr != null)
            {
                eventAttr.IsAttending = false;
                _appDbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            var events = _appDbContext.Events.Where(c => c.Id == id);
            if (events != null)
            {
                _appDbContext.Remove(id);
                return true;
            }

            return false;
        }

        public IEnumerable<Event> GetEvents()
        {
            var events = _appDbContext.Events.ToList();
            return events;
        }

        public void Update(int id)
        {
            throw new NotImplementedException();
        }

        public Event GetEventByName(string eventName)
        {
            var eventAttr = _appDbContext.Events.FirstOrDefault(c => c.Name == eventName);
            // ! null repository needed.
            return eventAttr == null ? null : eventAttr;
        }

        // Join
    }
}
