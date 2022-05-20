using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniBank.Core.Domains.Users;
using MiniBank.Core.Domains.Users.Services;
using MiniBank.Web.Controllers.Users.Dto;

namespace MiniBank.Web.Controllers.Users
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserController: ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateUserDto> _createValidator;
        private readonly IValidator<UpdateUserDto> _updateValidator;

        public UserController(IUserService userService, IMapper mapper, IValidator<CreateUserDto> createValidator, IValidator<UpdateUserDto> updateValidator)
        {
            _userService = userService;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpPost("Create")]
        public async Task Create([FromBody] CreateUserDto createUserDto)
        {
            await _createValidator.ValidateAndThrowAsync(createUserDto);
            var user = _mapper.Map<CreateUserDto, User>(createUserDto);
            await _userService.Create(user);
        }
        
        [HttpPut("Update")]
        public async Task Update([FromBody] UpdateUserDto updateUserDto)
        {
            await _updateValidator.ValidateAndThrowAsync(updateUserDto);
            var user = _mapper.Map<UpdateUserDto, User>(updateUserDto);
            await _userService.Update(user);
        }
        
        [HttpDelete("Delete")]
        public async Task Delete(Guid id)
        {
            await _userService.Delete(id);
        }
        
        [HttpGet("GetAllUsers")]
        public async Task<IEnumerable<GetUserDto>> GetAllUsers()
        {
            var users=await _userService.GetAllUsers();
            return _mapper.Map<IEnumerable<User>, IEnumerable<GetUserDto>>(users);
        }
    }
}