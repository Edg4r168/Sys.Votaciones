using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SysVotaciones.BLL;
using SysVotaciones.EN;

namespace SysVotaciones.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryBLL _categoryBLL;

        public CategoryController(CategoryBLL categoryBLL)
        {
            _categoryBLL = categoryBLL;
        }

        /*
          /Category
        */
        [HttpGet]
        public ActionResult<List<Category>> GetAll()
        {
            List<Category> listCategory = [];
            try
            {
                listCategory = _categoryBLL.GetAll();

                return Ok(new { ok = true, data = listCategory });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ok = false, data = listCategory });
            }
        }

        /*
           /Category/id
        */
        [HttpGet("{id:int}")]
        public ActionResult<Category> GetById(int id)
        {
            Category? category = null;
            try
            {
                category = _categoryBLL.GeById(id);

                if (category is null) return BadRequest(new { ok = false, data = category });

                return Ok(new { ok = true, data = category });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ok = false, data = category });
            }
        }

        /*
           /Category/save
        */
        [HttpPost("save")]
        public IActionResult Save(Category category)
        {
            try
            {
                int rowsAffected = _categoryBLL.Save(category);

                if (rowsAffected > 0) return Ok(new { ok = true, message = "Registro guardado" });

                return BadRequest(new { ok = false, message = "Error al guardar" });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ok = false, message = "Ha ocurrido un error inesperado" });
            }
        }

        /*
           /Category/delete/id
        */
        [HttpDelete("delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                int rowsAffected = _categoryBLL.Delete(id);

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
        public IActionResult Update(Category category)
        {
            try
            {
                int rowsAffected = _categoryBLL.Update(category);

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
