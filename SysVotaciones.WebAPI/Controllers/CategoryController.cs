﻿using Microsoft.AspNetCore.Cors;
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
        public ActionResult<List<Category>> GetAll([FromQuery] int offset, int amount)
        {
            List<Category> listCategory = [];
            try
            {
                listCategory = _categoryBLL.GetAll(offset, amount);

                return Ok(new { ok = true, data = listCategory });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ok = false, data = listCategory });
            }
        }

        /*
           /Category/total
        */
        [HttpGet("total")]
        public ActionResult GetTotal()
        {
            try
            {
                int total = _categoryBLL.GetTotal();

                return Ok(new { ok = true, total });
            }
            catch (Exception)
            {
                return StatusCode(500, new { ok = false, total = 0 });
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
            Category? currentCategory = null;
            try
            {
                int currentId = _categoryBLL.Save(category);

                if (currentId > 0)
                {
                    currentCategory = _categoryBLL.GeById(currentId);
                    return Ok(new { ok = true, data = currentCategory,  message = "Registro guardado" });
                }

                return BadRequest(new { ok = false, data = currentCategory, message = "Error al guardar" });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { ok = false, data = currentCategory, message = "Ha ocurrido un error inesperado" });
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

        /*
           /Category/search?keyword
        */
        [HttpGet("search")]
        public ActionResult<List<Category>> Search([FromQuery] string keyword) 
        {
            List<Category> listCategory = [];
            try
            {
                listCategory = _categoryBLL.Search(keyword);

                return Ok(new { ok = true, data = listCategory });
            }
            catch (Exception)
            {
                return StatusCode(500, new { ok = false, data = listCategory });
            }
        }
    }
}
