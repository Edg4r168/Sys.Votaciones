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

        /*[HttpGet]
        public async Task<ActionResult<List<Year>>> GetAllAsync()
        {
            List<Year> listYears = await _YearBLL.GetAllAsync();

            return StatusCode(StatusCodes.Status200OK, new { ok = true, data = listYears });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Year>> GetById(int id)
        {
            Year? year = await _YearBLL.GeById(id);

            return StatusCode(StatusCodes.Status200OK, new { ok = true, data = year });
        }

        [HttpPost]
        public async Task<ActionResult> SaveAsync(Year year)
        {
            int rowsAffected = await _YearBLL.SaveAsync(year);

            if (rowsAffected > 0) return StatusCode(StatusCodes.Status200OK, new { ok = true, data = rowsAffected });

            return StatusCode(StatusCodes.Status200OK, new { ok = false, data = "Error al guardar" });
        }*/

        /*
          /Year
        */
        [HttpGet]
        public ActionResult<List<Year>> GetAll()
        {
            List<Year> listYears = [];
            try
            {
                listYears = _YearBLL.GetAll();

                return Ok(new { ok = true, data = listYears });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ok = false, data = listYears });
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
            try
            {
                int rowsAffected = _YearBLL.Save(year);

                if (rowsAffected > 0) return Ok(new { ok = true, message = "Registro guardado" });

                return BadRequest(new { ok = false, message = "Error al guardar" });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ok = false, message = "Ha ocurrido un error inesperado" });
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
    }
}
