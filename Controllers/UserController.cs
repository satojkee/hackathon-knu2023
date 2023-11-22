using Microsoft.AspNetCore.Mvc;
using Hackathon.Interfaces;
using Hackathon.Dto;
using AutoMapper;

namespace Hackathon.Controllers
{
    [ApiController]
    [Route("/api/user")]
    public class UserController : ControllerBase
    {
        public readonly string UserNotFound = "There is no user with that email.";
        public readonly string AuthenticationFailed = "Authentication failed.";
        public readonly string RequestBodyInvalid = "Request body invalid.";
        public readonly string UserAlreadyExists = "User with that email already exists.";

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet("list")]
        [ProducesResponseType(200)]
        public IActionResult GetUsers()
        {
            var users = _mapper.Map<List<UserGetDto>>(_userRepository.GetUsers());

            return Ok(users);
        }

        [HttpGet("{email}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetUser(string email)
        {
            if (!ModelState.IsValid)
                return BadRequest(RequestBodyInvalid);

            if (!_userRepository.CheckExistance(email))
                return NotFound(UserNotFound);
            
            return Ok(_mapper.Map<UserGetDto>(_userRepository.GetUser(email)));
        }

        [HttpPost("create")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult CreateUser([FromBody] UserCreateDto user)
        {
            if (!ModelState.IsValid)
                return BadRequest(RequestBodyInvalid);

            if (_userRepository.CheckExistance(user.Email))
                return BadRequest(UserAlreadyExists);

            if (!_userRepository.CreateUser(user))
                return StatusCode(StatusCodes.Status500InternalServerError);

            return NoContent();
        }

        [HttpPost("auth")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult AuthenticateUser([FromBody] UserAuthDto user)
        {
            if (!ModelState.IsValid)
                return BadRequest(RequestBodyInvalid);

            if (!_userRepository.CheckExistance(user.Email))
                return NotFound(UserNotFound);

            if (!_userRepository.AuthenticateUser(user.Email, user.Password))
                return NotFound(AuthenticationFailed);

            return NoContent();
        }

        [HttpPut("update/{email}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateUser(string email, [FromBody] UserUpdateDto user)
        {
            if (!ModelState.IsValid)
                return BadRequest(RequestBodyInvalid);

            if (!_userRepository.CheckExistance(email))
                return NotFound(UserNotFound);

            if (!_userRepository.UpdateUser(email, user))
                return StatusCode(StatusCodes.Status500InternalServerError);

            return NoContent();
        }

        [HttpDelete("delete/{email}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult DeleteUser(string email)
        {
            if (!_userRepository.CheckExistance(email))
                return NotFound(UserNotFound);

            if (!_userRepository.DeleteUser(email))
                return StatusCode(StatusCodes.Status500InternalServerError);

            return NoContent();
        }
    }

}