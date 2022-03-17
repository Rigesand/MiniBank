using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MiniBank.Core.Domains.Users;
using MiniBank.Core.Domains.Users.Services;
using MiniBank.Web.Controllers.Users.Dto;

namespace MiniBank.Web.Controllers.Users
{
    [ApiController]
    [Route("[controller]")]
    public class UserController: ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("Create")]
        public void Create([FromBody] UserDto userDto)
        {
            var user = _mapper.Map<UserDto, User>(userDto);
            _userService.Create(user);
        }
        
        [HttpPut("Update")]
        public void Update([FromBody] UpdateUserDto updateUserDto)
        {
            var user = _mapper.Map<UpdateUserDto, User>(updateUserDto);
            _userService.Update(user);
        }
        
        [HttpDelete("Delete")]
        public void  Delete(string id)
        {
            _userService.Delete(id);
        }
        
        [HttpGet("GetAllUsers")]
        public List<GetUserDto> GetAllUsers()
        {
            var users=_userService.GetAllUsers();
            return _mapper.Map<List<User>, List<GetUserDto>>(users);
        }
    }
}