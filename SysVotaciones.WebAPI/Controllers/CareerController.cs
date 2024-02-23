using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SysVotaciones.BLL;
using SysVotaciones.EN;

namespace SysVotaciones.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors]
    public class CareerController : ControllerBase
    {
        private readonly CareerBLL _careerBLL;

        public CareerController(CareerBLL careerBLL)
        {
            _careerBLL = careerBLL;
        }


        /*
          /Career
        */
        [HttpGet]
        public ActionResult<List<Career>> GetAll()
        {
            List<Career> listCareer = [];
            try
            {
                listCareer = _careerBLL.GetAll();

                return Ok(new { ok = true, data = listCareer });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ok = false, data = listCareer });
            }
        }

        /*
           /Career/id
        */
        [HttpGet("{id:int}")]
        public ActionResult<Career> GetById(int id)
        {
            Career? career = null;
            try
            {
                career = _careerBLL.GeById(id);

                if (career is null) return BadRequest(new { ok = false, data = career });

                return Ok(new { ok = true, data = career });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ok = false, data = career });
            }
        }

        /*
           /Career/save
        */
        [HttpPost("save")]
        public IActionResult Save(Career career)
        {
            try
            {
                int rowsAffected = _careerBLL.Save(career);

                if (rowsAffected > 0) return Ok(new { ok = true, message = "Registro guardado" });

                return BadRequest(new { ok = false, message = "Error al guardar" });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ok = false, message = "Ha ocurrido un error inesperado" });
            }
        }

        /*
           /Career/delete/id
        */
        [HttpDelete("delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                int rowsAffected = _careerBLL.Delete(id);

                if (rowsAffected != 0) return Ok(new { ok = true, message = "Registro borrado" });

                return BadRequest(new { ok = false, message = "Error al borrar" });
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { ok = false, message = "Ha ocurrido un error inesperado" });
            }
        }

        /*
           /Career/update
        */
        [HttpPatch("update")]
        public IActionResult Update(Career career)
        {
            try
            {
                int rowsAffected = _careerBLL.Update(career);

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
