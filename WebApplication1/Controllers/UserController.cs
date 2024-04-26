using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController:ControllerBase
    {

        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>A list of all users.</returns>
       

        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            return _userService.GetUsers();
        }

        [HttpGet("{id}")]
        public ActionResult<User> Get(string id)
        {
            return _userService.GetUser(id);
        }

        [HttpPost]
        public ActionResult<List<User>> Create(User user)
        {
            var Users=_userService.CreateUser(user);
            return Users;
        }

        [HttpPut("{id}")]
        public ActionResult<List<User>> Update(string id, User userIn)
        {
            var users=_userService.UpdateUser(id, userIn);

            return users;
        }

        [HttpDelete("{id}")]
        public ActionResult<string> Delete(string id)
        {
            _userService.DeleteUser(id); 
            // return _userService.GetUsers()
            return "Deleted Successfully";
        }



        //practice

        [HttpGet("{id}/{name}")]

        //  looks like https://localhost:44311/api/User/swd/ss'    swd is the "id"  and ss ia the "name"
        public IActionResult Getbyid(string id,string name)
        {
            return Ok("edbebbc" + id);
        }

        [HttpGet("query")]
        // query perameter
        // looks like https://localhost:44311/api/User/query?id=ijci&val=raj' ijci  is the "id"  & operater dividing second parameter
        public ActionResult<string> Getbyquery(string id, string val)
        {
            return "edbebbc";
        }

        [HttpPost("formheader")]
        public IActionResult FormHeader([FromHeader(Name = "name")] string name)
        {
            return Ok($"Hello, {name} from header!");
        }

        [HttpPost("formbody")]
        public IActionResult FormBody([FromBody] string name)
        {
            return Ok($"Hello, {name} from body!");
        }

        [HttpPost("formform")]
        public IActionResult FormForm([FromForm] string name)
        {
            return Ok($"Hello, {name} from form!");
        }

        [HttpPost("formquery")]
        public IActionResult FormQuery([FromQuery] string name)
        {
            return Ok($"Hello, {name} from query!");
        }

        [HttpPost("formfilterresult.{format}"), FormatFilter]
        public IActionResult FormFilterResult()
        {
            return Ok("hiii");
        }
    }
}
