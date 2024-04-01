using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SysVotaciones.BLL;
using SysVotaciones.EN;

namespace SysVotaciones.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors]
    public class TypeContestController : ControllerBase
    {
        private readonly TypeContestBLL _typeContestBLL;

        public TypeContestController(TypeContestBLL typeContestBLL)
        {
            _typeContestBLL = typeContestBLL;
        }

        /*
          /TypeContest
        */
        [HttpGet]
        public ActionResult<List<TypeContest>> GetAll([FromQuery] int offset, int amount)
        {
            List<TypeContest> listTypeContest = [];
            try
            {
                listTypeContest = _typeContestBLL.GetAll(offset, amount);

                return Ok(new { ok = true, data = listTypeContest });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ok = false, data = listTypeContest });
            }
        }

        /*
           /TypeContest/total
        */
        [HttpGet("total")]
        public ActionResult GetTotal()
        {
            try
            {
                int total = _typeContestBLL.GetTotal();

                return Ok(new { ok = true, total });
            }
            catch (Exception)
            {
                return StatusCode(500, new { ok = false, total = 0 });
            }
        }

        /*
           /TypeContest/id
        */
        [HttpGet("{id:int}")]
        public ActionResult<TypeContest> GetById(int id)
        {
            TypeContest? typeContest = null;
            try
            {
                typeContest = _typeContestBLL.GeById(id);

                if (typeContest is null) return BadRequest(new { ok = false, data = typeContest });

                return Ok(new { ok = true, data = typeContest });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ok = false, data = typeContest });
            }
        }

        /*
           /TypeContest/save
        */
        [HttpPost("save")]
        public IActionResult Save(TypeContest typeContest)
        {
            TypeContest? currentTypeContest = null;
            try
            {
                int currentId = _typeContestBLL.Save(typeContest);

                if (currentId > 0)
                {
                    currentTypeContest = _typeContestBLL.GeById(currentId);
                    return Ok(new { ok = true, data = currentTypeContest, message = "Registro guardado" });
                }

                return BadRequest(new { ok = false, data = currentTypeContest, message = "Error al guardar" });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { ok = false, data = currentTypeContest, message = "Ha ocurrido un error inesperado" });
            }
        }

        /*
           /TypeContest/delete/id
        */
        [HttpDelete("delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                int rowsAffected = _typeContestBLL.Delete(id);

                if (rowsAffected != 0) return Ok(new { ok = true, message = "Registro borrado" });

                return BadRequest(new { ok = false, message = "Error al borrar" });
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { ok = false, message = "Ha ocurrido un error inesperado" });
            }
        }

        /*
           /Category/update
        */
        [HttpPatch("update")]
        public IActionResult Update(TypeContest typeContest)
        {
            try
            {
                int rowsAffected = _typeContestBLL.Update(typeContest);

                if (rowsAffected > 0) return Ok(new { ok = true, message = "Registro actualizado" });

                return BadRequest(new { ok = false, message = "Error al actualizar" });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ok = false, message = "Ha ocurrido un error inesperado" });
            }
        }

        /*
           /TypeContest/search?keyword
        */
        [HttpGet("search")]
        public ActionResult<List<TypeContest>> Search([FromQuery] string keyword)
        {
            List<TypeContest> listTypeContest = [];
            try
            {
                listTypeContest = _typeContestBLL.Search(keyword);

                return Ok(new { ok = true, data = listTypeContest });
            }
            catch (Exception)
            {
                return StatusCode(500, new { ok = false, data = listTypeContest });
            }
        }
    }
}
