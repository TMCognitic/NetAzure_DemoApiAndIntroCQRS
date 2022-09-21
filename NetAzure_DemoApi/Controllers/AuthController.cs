using Microsoft.AspNetCore.Mvc;
using NetAzure_DemoApi.CQRS.Commands;
using NetAzure_DemoApi.CQRS.Entities;
using NetAzure_DemoApi.CQRS.Queries;
using NetAzure_DemoApi.Models.Forms;
using Tools.CQRS.Commands;
using Tools.CQRS.Queries;

namespace NetAzure_DemoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ICommandHandler<RegisterCommand> _registerCommandHandler;
        private readonly IQueryHandler<LoginQuery, User> _loginQueryHandler;

        public AuthController(ICommandHandler<RegisterCommand> registerCommandHandler, IQueryHandler<LoginQuery, User> loginQueryHandler)
        {
            _registerCommandHandler = registerCommandHandler;
            _loginQueryHandler = loginQueryHandler;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterForm form)
        {
            Result result = _registerCommandHandler.Execute(new RegisterCommand(form.Email, form.Passwd));

            if (result.IsFailure)
                return BadRequest(new { Message = result.Message });

            return NoContent();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginForm form)
        {
            try
            {
                User? user = _loginQueryHandler.Execute(new LoginQuery(form.Email, form.Passwd));

                if (user is null)
                    return NotFound();

                return Ok(user);
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
