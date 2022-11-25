using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MLSZ.Data;
using MLSZ.Entities;
using MLSZ.Services.MailService;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using static MLSZ.Shared.Shared;

namespace MLSZ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UsersController : Controller
    {
        private readonly MlszContext _context;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;

        public UsersController(MlszContext context, IEmailService emailService, IUserService userService)
        {
            _context = context;
            _emailService = emailService;
            _userService = userService;
        }

        // GET: Users
        [HttpGet]
        public async Task<ActionResult<User[]?>> Index()
        {
            return await _userService.GetAllUsers();
        }

        // GET: Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Details(int id)
        {

            var user = await _userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("create")]
        public async Task<IActionResult> Create([Bind("Name,Email,Phone,Org,Position,Role,PwSalt,PwHash")] UserDto user)
        {
            if (ModelState.IsValid)
            {
                var newUser = await _userService.CreateUser(user);
                return Ok(newUser);
            }
            return NotFound();
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit([Bind("Id,Name,Email,Phone,Org,Position,Role,PwSalt,PwHash")] User user)
        {
            if (ModelState.IsValid)
            {
                await _userService.EditUser(user);
            }
            return Ok(user);
        }

        // POST: Users/Delete/5
        [HttpPost("delete/{id}"), ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.DeleteUser(id);
            return Ok();
        }

        private bool UserExists(int id)
        {
          return _context.Users.Any(e => e.Id == id);
        }

        //Find user by email
        [HttpPost("findByEmail"), ActionName("Find")]
        public async Task<User?> Find(string email)
        {
           var user = await _context.Users.FindAsync(email);

            if (user == null) return null;
            return user;
        }
    }
}
