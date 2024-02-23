using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SysVotaciones.BLL;
using SysVotaciones.EN;

namespace SysVotaciones.WebAPI.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    [EnableCors]
    public class ContestController : ControllerBase
    {
        private readonly ContestBLL _contestBLL;

        public ContestController (ContestBLL contestBLL)
        {
            _contestBLL = contestBLL;
        }

        /*
            /Contest
        */
        [HttpGet]
        public ActionResult<List<Contest>> GetAll()
        {
            List<Contest> listContest = [];
            try
            {
                listContest = _contestBLL.GetAll();

                return Ok(new { ok = true, data = listContest });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ok = false, data = listContest });
            }
        }

        /*
           /Contest/id
        */
        [HttpGet("{id:int}")]
        public ActionResult<Contest> GetById(int id)
        {
            Contest? contest = null;
            try
            {
                contest = _contestBLL.GeById(id);

                if (contest is null) return BadRequest(new { ok = false, data = contest });

                return Ok(new { ok = true, data = contest });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ok = false, data = contest });
            }
        }

        /*
           /Contest/save
        */
        [HttpPost("save")]
        public IActionResult Save(Contest contest)
        {
            try
            {
                int rowsAffected = _contestBLL.Save(contest);

                if (rowsAffected > 0) return Ok(new { ok = true, message = "Registro guardado" });

                return BadRequest(new { ok = false, message = "Error al guardar" });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ok = false, message = "Ha ocurrido un error inesperado" });
            }
        }

        /*
           /Contest/delete/id
        */
        [HttpDelete("delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                int rowsAffected = _contestBLL.Delete(id);

                if (rowsAffected != 0) return Ok(new { ok = true, message = "Registro borrado" });

                return BadRequest(new { ok = false, message = "Error al borrar" });
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { ok = false, message = "Ha ocurrido un error inesperado" });
            }
        }

        /*
           /Contest/update
        */
        [HttpPatch("update")]
        public IActionResult Update(Contest contest)
        {
            try
            {
                int rowsAffected = _contestBLL.Update(contest);

                if (rowsAffected > 0) return Ok(new { ok = true, message = "Registro actualizado" });

                return BadRequest(new { ok = false, message = "Error al actualizar" });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ok = false, message = "Ha ocurrido un error inesperado" });
            }
        }
    }
}
