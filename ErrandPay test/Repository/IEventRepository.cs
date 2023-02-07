using ErrandPay_test.Models;

namespace ErrandPay_test.Repository
{
    public interface IEventRepository
    {
        bool Add(Event eventAttr);

        IEnumerable<Event> GetEvents();
        Event GetEventByName(string eventName);
        bool JoinEvent(string name);
        bool LeaveEvent(string name);
        void Update(int id);
        bool Delete(int id);
    }
}
