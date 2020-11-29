using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TectoroWebApi;
using TectoroWebApi.Models;

namespace TectoroWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly TectoroUserDbContext _context;
        const string MANAGER_STR = "MANAGER";
        const string CLIENT_STR = "CLIENT";
        protected DbContext DBContextObject
        {
            get
            {
                return _context;
            }
        }
        protected IEnumerable<UserType> GetUsersProperty(string userType = null)
        {
            IEnumerable<Users> users = _context.Users;
            List<UserType> userTypeList = new List<UserType>();
            if (userType.ToUpper().Equals(MANAGER_STR))
            {
                var tmgrList = (users.Where(u => !string.IsNullOrEmpty(u.Position)));
                foreach (var item in tmgrList)
                {
                    var mgr = new UserType(item, userType);
                    userTypeList.Add(mgr);
                    UserType.Clients(users, mgr);
                }
            }
            else if (userType.ToUpper().Equals(CLIENT_STR))
            {
                var tClient = users.Where(u => u.Level.HasValue);
                foreach (var item in tClient)
                {
                    var client = new UserType(item, userType);
                    userTypeList.Add(client);
                    UserType.ManagerList(users, client);
                }
            }
            return userTypeList;
        }
        public UsersController(TectoroUserDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public IEnumerable<Users> GetUsers()
        {
            return GetUsersProperty();
        }

        [HttpGet]
        [Route("[action]/{userType}")]
        public IEnumerable<UserType> GetManagerOrClient(string userType)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }
            IEnumerable<UserType> result = GetUsersProperty(userType);
            return result;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsers([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = await _context.Users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers([FromRoute] int id, [FromBody] Users users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != users.UserId)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> PostUsers([FromBody] Users users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Users.Add(users);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = users.UserId }, users);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsers([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            return Ok(users);
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}