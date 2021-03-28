using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _service;
        public UsersController(IUserService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var result = await _service.GetAll();
                return Ok(result);

            }
            catch (ArgumentException e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);

            }

        }
        [Authorize]
        [HttpGet]
        [Route("{id}", Name = "GetWithId")]
        public async Task<ActionResult> Get(int id)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {

                return Ok(await _service.Get(id));

            }
            catch (ArgumentException e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);

            }


        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserEntity user)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {

                var result = await _service.Post(user);
                if (result != null)
                {
                    return Created(new Uri(Url.Link("GetWithId", new { id = result.Id })), result);
                }
                else
                {
                    return BadRequest();
                }


            }
            catch (ArgumentException e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);

            }


        }
        [Authorize]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UserEntity user)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {

                var result = await _service.Put(user);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }


            }
            catch (ArgumentException e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);

            }


        }
        [Authorize]
        [HttpDelete("{id})")]
        public async Task<ActionResult> Delete(int id)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {

                return Ok(await _service.Delete(id));


            }
            catch (ArgumentException e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);

            }


        }


    }
}
