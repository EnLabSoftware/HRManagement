using API.DTOs.Users;
using API.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger
            , UserService service)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetUserRequest request)
        {
            var users = await _service.SearchAsync(request);
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddUserRequest request)
        {
            var users = await _service.AddNewAsync(request);
            return Ok(users);
        }

        [HttpPost("payslips")]
        public async Task<IActionResult> AddPayslip([FromBody] AddPayslipRequest request)
        {
            var users = await _service.AddUserPayslipAsync(request);
            return Ok(users);
        }
    }
}