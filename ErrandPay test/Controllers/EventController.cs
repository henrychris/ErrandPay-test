using ErrandPay_test.Models;
using ErrandPay_test.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ErrandPay_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;

        public EventController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        // GET: api/<ValuesController>
        [HttpGet("/GetEvents")]
        public IEnumerable<Event> GetEvents()
        {
            return _eventRepository.GetEvents();
        }

        [HttpGet("/GetEventByName/{eventName}")]
        public Event GetEventByName(string eventName)
        {
            return _eventRepository.GetEventByName(eventName);
        }

        [HttpGet("/JoinEvent/{eventName}")]
        public IActionResult JoinEvent( string eventName)
        {
            // add checks of course. but make it wrk first
            var result = _eventRepository.JoinEvent(eventName);

            return result ? Ok(result) : BadRequest(result);
            // change isAttending to True
        }

        [HttpGet("/LeaveEvent/{eventName}")]
        public IActionResult LeaveEvent( string eventName)
        {
            // add checks of course. but make it work first
            var result = _eventRepository.LeaveEvent(eventName);

            return result ? Ok(result) : BadRequest(result);
            // change isAttending to True
        }

        // Create Event
        [HttpPost("/CreateEvent")]
        public IActionResult CreateEvent(EventDTO eventdto)
        {
            var eventDTO = new Event
            {
                Name = eventdto.Name,
                Price = eventdto.Price,
                Date = DateTime.Parse(eventdto.Date.ToString())
            };

            var result = _eventRepository.Add(eventDTO);

            return result ? Ok(result) : BadRequest(result);
        }

        // GET api/<ValuesController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // Get all events, if logged in

        // create event, if logged in

        // DELETE api/<ValuesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
