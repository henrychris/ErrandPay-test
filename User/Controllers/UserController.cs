using ErrandPay_test.DTO;
using ErrandPay_test.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using User.Models;
using UserAPI.Repository;

namespace User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly HttpClient _client;

        public List<Event> EmptyList = new List<Event>();

        public UserController(IUserRepository userRepository, HttpClient client)
        {
            _userRepository = userRepository;
            _client = client;
        }

        [HttpPost("/SignUp")]
        public IActionResult SignUp([FromBody] UserDTO user)
        {
            var userDTO = new UserObj
            {
                Name = user.Name,
                Email = user.Email.ToLower(),
                Password = user.Password
            };

            var result = _userRepository.Add(userDTO);

            if (result) return Ok(result);
            return BadRequest(result);

        }

        // get events, it works.
        [HttpGet("/SeeEvents")]
        public async Task<List<Event>> GetEvents()
        {
            //_client.BaseAddress = new Uri("https://localhost:7055/");
            var response = await _client.GetAsync("GetEvents");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<Event>>(await response.Content.ReadAsStringAsync());
            }

            return EmptyList;
        }

        [HttpGet("/RequestToJoinEvent/{eventName}/{email}")]
        public async Task<IActionResult> JoinEvent(string eventName, string email)
        {
            var user = _userRepository.FindByEmail(email);

            var getEventResponse = await _client.GetAsync($"GetEventByName/{eventName}");
            if (getEventResponse.IsSuccessStatusCode)
            {
                var eventResponse = JsonConvert.DeserializeObject<Event>(await getEventResponse.Content.ReadAsStringAsync());
                var payResponse = _userRepository.PayForEvent(eventResponse, user);

                return payResponse == true ? await JoinEvent(eventName) : BadRequest(payResponse);
            }
            else return BadRequest(getEventResponse);
        }

        private async Task<IActionResult> JoinEvent(string eventName)
        {
            var response = await _client.GetAsync($"JoinEvent/{eventName}");

            if (response.IsSuccessStatusCode)
            {
                return Ok("Success");
            }
            return BadRequest("Failed.");
        }


        [HttpGet("/RequestToLeaveEvent/{eventName}/{email}")]
        public async Task<IActionResult> LeaveEvent(string email, string eventName)
        {
            var user = _userRepository.FindByEmail(email);

            var getEventResponse = await _client.GetAsync($"GetEventByName/{eventName}");
            if (getEventResponse.IsSuccessStatusCode)
            {
                var eventResponse = JsonConvert.DeserializeObject<Event>(await getEventResponse.Content.ReadAsStringAsync());
                var payResponse = _userRepository.FundWallet(user, eventResponse.Price);

                return payResponse.Item1 == true ? await LeaveEvent(eventName) : BadRequest(payResponse);
            }
            else return BadRequest(getEventResponse);
        }

        private async Task<IActionResult> LeaveEvent(string eventName)
        {
            var response = await _client.GetAsync($"LeaveEvent/{eventName}");
            if (response.IsSuccessStatusCode)
            {
                return Ok("Success");
            }
            return BadRequest("Failed.");
        }

        [HttpPost("/FundWallet")]
        public IActionResult FundWallet(FundingDTO funding)
        {
            var user = _userRepository.FindByEmail(funding.Email);
            // ask for user email, for now. find the
            var response = _userRepository.FundWallet(user, funding.Amount);

            if (response.Item1 == true)
            {
                return Ok($"Success! New balance: {response.Item2}");
            }
            return BadRequest("Failed");
        }

        // isLoggedIn
        // endpoint to fund wallet

        // Logout
    }
}
