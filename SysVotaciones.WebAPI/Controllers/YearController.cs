using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SysVotaciones.BLL;
using SysVotaciones.EN;

namespace SysVotaciones.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors]
    public class YearController : ControllerBase
    {
        private readonly YearBLL _YearBLL;

        public YearController(YearBLL yearBLLl) 
        {
            _YearBLL = yearBLLl;
        }

        /*
          /Year
        */
        [HttpGet]
        public ActionResult<List<Year>> GetAll([FromQuery] int offset, int amount)
        {
            List<Year> listYears = [];
            try
            {
                listYears = _YearBLL.GetAll(offset, amount);

                return Ok(new { ok = true, data = listYears });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ok = false, data = listYears });
            }
        }

        /*
           /Year/total
        */
        [HttpGet("total")]
        public ActionResult GetTotal()
        {
            try
            {
                int total = _YearBLL.GetTotal();

                return Ok(new { ok = true, total });
            }
            catch (Exception)
            {
                return StatusCode(500, new { ok = false, total = 0 });
            }
        }

        /*
           /Year/id
        */
        [HttpGet("{id:int}")]
        public ActionResult<Year> GetById(int id)
        {
            Year? year = null;
            try
            {
                year = _YearBLL.GeById(id);

                if (year is null) return BadRequest(new { ok = false, data = year });

                return Ok(new { ok = true, data = year });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ok = false, data = year });
            }
        }

        /*
           /Year/save
        */
        [HttpPost("save")]
        public IActionResult Save(Year year)
        {
            Year? currentYear = null;
            try
            {
                int currentId = _YearBLL.Save(year);

                if (currentId > 0)
                {
                    currentYear = _YearBLL.GeById(currentId);
                    return Ok(new { ok = true, data = currentYear, message = "Registro guardado" });
                }

                return BadRequest(new { ok = false, data = currentYear, message = "Error al guardar" });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ok = false, data = currentYear, message = "Ha ocurrido un error inesperado" });
            }
        }

        /*
           /Year/delete/id
        */
        [HttpDelete("delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                int rowsAffected = _YearBLL.Delete(id);

                if (rowsAffected != 0) return Ok(new { ok = true, message = "Registro borrado" });

                return BadRequest(new { ok = false, message = "Error al borrar" });
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { ok = false, message = "Ha ocurrido un error inesperado" });
            }
        }

        /*
           /Year/update
        */
        [HttpPatch("update")]
        public IActionResult Update(Year year)
        {
            try
            {
                int rowsAffected = _YearBLL.Update(year);

                if (rowsAffected > 0) return Ok(new { ok = true, message = "Registro actualizado" });

                return BadRequest(new { ok = false, message = "Error al actualizar" });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ok = false, message = "Ha ocurrido un error inesperado" });
            }
        }

        /*
           /Year/search?keyword
        */
        [HttpGet("search")]
        public ActionResult<List<Year>> Search([FromQuery] string keyword)
        {
            List<Year> listYear = [];
            try
            {
                listYear = _YearBLL.Search(keyword);

                return Ok(new { ok = true, data = listYear });
            }
            catch (Exception)
            {
                return StatusCode(500, new { ok = false, data = listYear });
            }
        }
    }
}
