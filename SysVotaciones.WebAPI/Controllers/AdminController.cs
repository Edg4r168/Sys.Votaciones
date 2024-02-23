using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SysVotaciones.BLL;
using SysVotaciones.EN;

namespace SysVotaciones.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors]
    public class AdminController: ControllerBase
    {
        private readonly AdminBLL _adminBLL;

        public AdminController(AdminBLL adminBLL)
        {
           _adminBLL = adminBLL;
        }

        /*
           /Admin/login
        */
        [HttpPost("login")]
        public IActionResult Login(Admin admin)
        {
            try
            {
                var result = _adminBLL.Login(admin);

                if (!result.Logged) return BadRequest(new { ok = false, token = "" });

                return Ok(new { ok = true, token = result.Token });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new { ok = false, token = "" });
                throw;
            }
        }
    }
}
