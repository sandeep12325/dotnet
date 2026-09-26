using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // In-memory list to store users.
        // Marked 'static' so data remains in memory across multiple HTTP requests.
        private static readonly List<User> userList = new List<User>
        {
            new User { Id = 1, Name = "Salman", Age = 25, Number = "9876543210" },
            new User { Id = 2, Name = "Rahul", Age = 30, Number = "9123456780" }
        };

        // 1. READ ALL - GET: api/user
        [HttpGet]
        public ActionResult<List<User>> GetAllUsers()
        {
            return Ok(userList);
        }

        // 2. READ BY ID - GET: api/user/{id}
        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            var user = userList.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound($"User with Id = {id} not found.");
            }
            return Ok(user);
        }

        // 3. CREATE - POST: api/user
        [HttpPost]
        public ActionResult<User> CreateUser([FromBody] User newUser)
        {
            if (newUser == null)
            {
                return BadRequest("Invalid user data.");
            }

            // Generate a unique Id (1 if list is empty, otherwise max Id + 1)
            int nextId = userList.Count > 0 ? userList.Max(u => u.Id) + 1 : 1;
            newUser.Id = nextId;

            userList.Add(newUser);

            // Returns 201 Created status code with location of the new resource
            return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
        }

        // 4. UPDATE - PUT: api/user/{id}
        [HttpPut("{id}")]
        public ActionResult<User> UpdateUser(int id, [FromBody] User updatedUser)
        {
            var user = userList.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound($"User with Id = {id} not found.");
            }

            // Update only Name, Age, and Number
            user.Name = updatedUser.Name;
            user.Age = updatedUser.Age;
            user.Number = updatedUser.Number;

            return Ok(user);
        }

        // 5. DELETE - DELETE: api/user/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            var user = userList.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound($"User with Id = {id} not found.");
            }

            userList.Remove(user);
            return Ok(new { message = $"User with Id = {id} deleted successfully." });
        }
    }
}
