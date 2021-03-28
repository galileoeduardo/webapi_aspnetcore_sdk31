using Api.Domain.Entities;
using Domain.Dtos;
using Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<object> Login([FromBody] LoginDto userEntity, [FromServices] ILoginService service )
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var result = await service.FindByLogin(userEntity);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (ArgumentException e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError,e.Message);
            }
        }
    }
}
