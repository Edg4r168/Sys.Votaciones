using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SysVotaciones.BLL;
using SysVotaciones.EN;

namespace SysVotaciones.WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
[EnableCors]
public class ParticipantController : ControllerBase
{
    private readonly ParticipantBLL _participantBLL;

    public ParticipantController(ParticipantBLL participantBLL)
    {
        _participantBLL = participantBLL;
    }


    /*
      /Participant
    */
    [HttpGet]
    public ActionResult<List<Participant>> GetAll([FromQuery] int offset, int amount)
    {
        List<Participant> list = [];
        try
        {
            list = _participantBLL.GetAll(offset, amount);

            return Ok(new { ok = true, data = list});
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { ok = false, data = list});
        }
    }

    /*
       /Participant/total
    */
    [HttpGet("total")]
    public ActionResult GetTotal()
    {
        try
        {
            int total = _participantBLL.GetTotal();

            return Ok(new { ok = true, total });
        }
        catch (Exception)
        {
            return StatusCode(500, new { ok = false, total = 0 });
        }
    }

    /*
       /Participant/{id}
    */
    [HttpGet("{id:int}")]
    public ActionResult<Participant> GetById(int id)
    {
        Participant? participant = null;
        try
        {
            participant = _participantBLL.GeById(id);

            if (participant is null) return BadRequest(new { ok = false, data = participant });

            return Ok(new { ok = true, data = participant });
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { ok = false, data = participant });
        }
    }

    /*
       /Participant/save
    */
    [HttpPost("save")]
    public IActionResult Save(Participant participant)
    {
        Participant? currentParticipant = null;
        try
        {
            int currentId = _participantBLL.Save(participant);

            if (currentId > 0)
            {
                currentParticipant = _participantBLL.GeById(currentId);

                return Ok(new { ok = true, data = currentParticipant, message = "Registro guardado" });
            };

            return BadRequest(new { ok = false, data = currentParticipant, message = "Error al guardar" });
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, 
                                new { ok = false, data = currentParticipant, 
                                message = "Ha ocurrido un error inesperado" });
        }
    }

    /*
       /Participant/delete/{id}
    */
    [HttpDelete("delete/{id:int}")]
    public IActionResult Delete(int id)
    {
        try
        {
            int rowsAffected = _participantBLL.Delete(id);

            if (rowsAffected != 0) return Ok(new { ok = true, message = "Registro borrado" });

            return BadRequest(new { ok = false, message = "Error al borrar" });
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError, new { ok = false, message = "Ha ocurrido un error inesperado" });
        }
    }

    /*
       /Participant/update
    */
    [HttpPatch("update")]
    public IActionResult Update(Participant participant)
    {
        try
        {
            int rowsAffected = _participantBLL.Update(participant);

            if (rowsAffected > 0) return Ok(new { ok = true, message = "Registro actualizado" });

            return BadRequest(new { ok = false, message = "Error al actualizar" });
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { ok = false, message = "Ha ocurrido un error inesperado" });
        }
    }

    /*
       /Participant/search?keyword
    */
    [HttpGet("search")]
    public ActionResult<List<Participant>> Search([FromQuery] string keyword)
    {
        List<Participant> list= [];
        try
        {
            list = _participantBLL.Search(keyword);

            return Ok(new { ok = true, data = list });
        }
        catch (Exception)
        {
            return StatusCode(500, new { ok = false, data = list });
        }
    }
}

