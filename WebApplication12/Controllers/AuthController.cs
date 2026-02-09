using System;
using Microsoft.AspNetCore.Mvc;
using WebApplication12.Data;

namespace WebApplication12.Controllers
{
    public class AuthController : Controller
    {
        [ApiController]
        [Route("api/auth")]
      
            private readonly AppDbContext _context;

            public AuthController(AppDbContext context)
            {
                _context = context;
            }

            [HttpPost("register")]
            public IActionResult Register([FromBody] User user)
            {
                try
                {
                    if (_context.Users.Any(u => u.Email == user.Email))
                        return BadRequest(new { message = "Email already exists" });

                    _context.Users.Add(user);
                    _context.SaveChanges();
                    return Ok(new { message = "Registered Successfully" });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { message = "Internal Server Error" });
                }
            }

            [HttpPost("login")]
            public IActionResult Login([FromBody] User login)
            {
                try
                {
                    var user = _context.Users
                        .FirstOrDefault(u => u.Email == login.Email && u.PasswordHash == login.PasswordHash);

                    if (user == null)
                        return Unauthorized(new { message = "Invalid Email or Password" });

                    return Ok(user);
                }
                catch
                {
                    return StatusCode(500, new { message = "Internal Server Error" });
                }
            }
        }
    }
