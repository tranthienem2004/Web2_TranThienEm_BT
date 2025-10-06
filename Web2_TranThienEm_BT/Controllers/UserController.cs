﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web2_TranThienEm_BT.Models.DTO;
using Web2_TranThienEm_BT.Repositories;

namespace Web2_TranThienEm_BT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;
        public UserController(UserManager<IdentityUser> userManager, ITokenRepository
       tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO
       registerRequestDTO)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDTO.Username,
                Email = registerRequestDTO.Username
            };
            var identityResult = await _userManager.CreateAsync(identityUser,
           registerRequestDTO.Password);
            if (identityResult.Succeeded)
            {
                if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(identityUser,
                   registerRequestDTO.Roles);
                }
                if (identityResult.Succeeded)
                {
                    return Ok("Register Successful! Let login!");
                }
            }
            return BadRequest("Something wrong!");
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var user = await _userManager.FindByEmailAsync(registerRequestDTO.Username);
            if (user != null)
            {
                var checkPasswordResult = await
               _userManager.CheckPasswordAsync(user, registerRequestDTO.Password);
                if (checkPasswordResult)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        var jwtToken = _tokenRepository.CreateJWTToken(user,
                       roles.ToList());
                        var response = new LoginResponseDTO
                        {
                            JwtToken = jwtToken
                        };

                        return Ok(response);
                    }
                }
            }
            return BadRequest("Username or password incorrect");
        }
    }
}

