using Microsoft.AspNetCore.Mvc;
using NetAzure_DemoApi.Bll.Entities;
using NetAzure_DemoApi.Bll.Services;
using NetAzure_DemoApi.Models.Forms;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Tools.Connections.Databases;

namespace NetAzure_DemoApi.Controllers
{
    /// <summary>
    /// https://localhost:7215/api/ToDo
    /// </summary>
    [ApiController]
    [Route("api/[Controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoService _service;

        public ToDoController(ToDoService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<ToDo> todoes = _service.Get().ToList();
                return Ok(todoes);
            }
            catch (Exception ex)
            {
#if DEBUG
                return BadRequest(new { Message = ex.Message });
#else
                return BadRequest(new { Message = "Erreur durant le traitement, contactez l'admin..." });
#endif
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] AddToDoForm form)
        {
            try
            {
                int rows = _service.Insert(new ToDo(form.Title));

                if (rows == 0)
                    return BadRequest();

                return NoContent();
            }
            catch (Exception ex)
            {
#if DEBUG
                return BadRequest(new { Message = ex.Message });
#else
                    return BadRequest(new { Message = "Erreur durant le traitement, contactez l'admin..." });
#endif
            }
            
        }        
    }
}
