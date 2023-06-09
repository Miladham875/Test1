﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestProject.Areas.Identity.Data;
using TestProject.Data;
using TestProject.Models;

namespace TestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SecurityController : ControllerBase
    {
        private readonly TestProjectContext1 _dbContext;
        private readonly UserManager<TestProjectUser> _userManager;
        private readonly SignInManager<TestProjectUser> _signInManager;

        public SecurityController(TestProjectContext1 dbContext, UserManager<TestProjectUser> userManager, SignInManager<TestProjectUser> signInManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet("getFruits")]
        [AllowAnonymous]
        public ActionResult GetFruits()
        {
            List<string> mylist = new List<string>() { "apples", "bannanas" };
            return Ok(mylist);
        }
        [HttpGet("getFruitsAuthenticated")]
        public ActionResult GetFruitsAuthenticated()
        {
            List<string> mylist = new List<string>() { "organicApples", "organicBannanas" };
            return Ok(mylist);
        }

        [AllowAnonymous]
        [HttpPost("getToken")]
        public async Task<ActionResult> GetToken([FromBody] LoginModel myLoginModelType)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Email == myLoginModelType.Email);
            if (user != null)
            {
                var singInResult = await _signInManager.CheckPasswordSignInAsync(user, myLoginModelType.Password, false);
                if (singInResult.Succeeded)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes("@#MY_BIG_SECRET_KEY@#");
                    var tokenDescription = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]                      {
                        new Claim(ClaimTypes.Name, myLoginModelType.Email)
                        }
                       ),
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    };
                    var token = tokenHandler.CreateToken(tokenDescription);
                    var tokenString = tokenHandler.WriteToken(token);
                    return Ok(new { token = tokenString });
                }
                else
                {
                    return Ok("failed, try again");
                }
            }
            return Ok("failed, try again");
        }


        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginModel myLoginModelType)
        {
            TestProjectUser securingWebApiDotNetCoreWithIdentity = new TestProjectUser()
            {
                Email = myLoginModelType.Email,
                UserName = myLoginModelType.Email,
                EmailConfirmed = false,
            };

            var result = await _userManager.CreateAsync(securingWebApiDotNetCoreWithIdentity, myLoginModelType.Password);
            if (result.Succeeded)
            {
                return Ok(new { result = "Register Success" });
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    sb.Append(error.Description);
                }
                return BadRequest(new { Result = $"Register Fail: { sb.ToString()}" });
        }
    }







}
}
