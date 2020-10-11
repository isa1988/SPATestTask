using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SPATestTask.API.Models.User;
using SPATestTask.Services.Dto;
using SPATestTask.Services.Services.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SPATestTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController(IUserService service, IMapper mapper)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        private readonly IUserService service;
        private readonly IMapper mapper;

        // GET: api/<UserController>
        [HttpGet("getall")]
        public async Task<IEnumerable<UserModel>> Get()
        {
            var lstUsers = service.GetAll();
            Console.WriteLine(" кол-во" + lstUsers.Count);
            var models = mapper.Map<IEnumerable<UserModel>>(lstUsers);
            return models;
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public UserModel Get(int? id)
        {
            if (!id.HasValue)
            {
                return new UserModel();
            }
            var user = service.GetById(id.Value);
            var model = mapper.Map<UserModel>(user);
            return model;
        }

        // POST api/<UserController>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] UserAddModel value)
        {
            if (value == null)
            {
                return NotFound();
            }
            var userDto = mapper.Map<UserDto>(value);

            var result = await service.CreateAsync(userDto);

            if (result.IsSuccess)
            {
                return Ok();
            }
            else
            {
                return new JsonResult(result.GetErrorString());
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("edit")]
        public async Task<IActionResult> Update([FromBody] UserUpdateModel value)
        {
            if (value == null)
            {
                return NotFound();
            }
            var userDto = mapper.Map<UserDto>(value);

            var result = await service.UpdateAsync(userDto);

            if (result.IsSuccess)
            {
                return Ok();
            }
            else
            {
                return new JsonResult(result.GetErrorString());
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }
            
            var result = await service.DeleteItemAsync(id.Value);

            if (result.IsSuccess)
            {
                return Ok();
            }
            else
            {
                return new JsonResult(result.GetErrorString());
            }
        }
    }
}
