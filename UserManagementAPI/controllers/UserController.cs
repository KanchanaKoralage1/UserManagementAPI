using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private static List<User> users = new();

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers() => Ok(users);

    [HttpGet("{id}")]
    public ActionResult<User> GetUser(int id)
    {
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpPost]
    public ActionResult<User> CreateUser([FromBody] User user)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        user.Id = users.Count > 0 ? users.Max(u => u.Id) + 1 : 1;
        users.Add(user);
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = users.FirstOrDefault(u => u.Id == id);
        if (user == null) return NotFound();

        user.Name = updatedUser.Name;
        user.Email = updatedUser.Email;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user == null) return NotFound();

        users.Remove(user);
        return NoContent();
    }
}
