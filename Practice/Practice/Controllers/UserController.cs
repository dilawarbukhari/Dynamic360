using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PacticeService.DTOs.UserDto;
using PracticeDomain.DTOs.UserDto;
using PracticeDomain.IRepository;
using PracticeModel.Models.AuthenticationModel;

namespace Practice.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Register(SignupRequest request)
        {
            await _repository.RegisterUser(request);
            return Ok(new { message = "User registered successfully. Please check your email for confirmation." });
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            
            
                await _repository.ConfirmEmailAsync(userId, code);
                return Ok("Email confirmed successfully.");
            
           
        }

        [HttpPost]
       public async Task<IActionResult> Login(LoginRequest request)
        {
           var resposne= await _repository.Login(request);
            return Ok(resposne);


        }

    }
    }

