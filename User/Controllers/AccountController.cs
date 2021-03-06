﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Core.Dto;
using User.Services.Interface;

namespace User.Controllers
{
    [Route("User/[Controller]")]
    [ApiController]
    public class AccountController : BaseController.BaseControllers
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Assign-Roles")]
        [Authorize(Roles ="SuperAdmin")]
        public async Task<IActionResult> AssignRoles([FromBody] AssignRoleDto assignRoleDto) => Ok(await _userService.AssignRoles(assignRoleDto));

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto) => Ok(await _userService.Login(loginDto));

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout() => Ok(await _userService.Logout());

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto) => Ok(await _userService.Register(registerDto));

        [HttpGet("Get-All-Users")]
        public async Task<IActionResult> GetPagedUsers([FromQuery] int? pageIndex, [FromQuery] int? pageSize) => Ok(await _userService.GetPagedUsers(pageIndex, pageSize));
        

        [HttpGet("Get-Username-from-Token")]
        public object GetUserName() => Ok(GetCurrentUserName());
        

        [HttpGet("Get-User-By-Id")]
        public async Task<IActionResult> GetUser(string id) => Ok(await _userService.GetUser(id));
    }
}