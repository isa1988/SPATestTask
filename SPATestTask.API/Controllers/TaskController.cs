using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SPATestTask.API.Models.Task;
using SPATestTask.Services.Dto;
using SPATestTask.Services.Services.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SPATestTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        public TaskController(ITaskService service, IMapper mapper)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        private readonly ITaskService service;
        private readonly IMapper mapper;

        // GET: api/<UserController>
        [HttpGet]
        public async Task<IEnumerable<TaskModel>> Get()
        {
            var lstUsers = service.GetAll();
            Console.WriteLine(" кол-во" + lstUsers.Count);
            var models = mapper.Map<IEnumerable<TaskModel>>(lstUsers);
            return models;
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public JsonResult Get(int? id)
        {
            if (!id.HasValue)
            {
                return new JsonResult(new TaskModel());
            }
            var user = service.GetById(id.Value);
            var model = mapper.Map<TaskModel>(user);
            return new JsonResult(model);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskAddModel value)
        {
            if (value == null)
            {
                return NotFound();
            }
            var userDto = mapper.Map<TaskDto>(value);

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
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] TaskUpdateModel value)
        {
            if (value == null)
            {
                return NotFound();
            }
            var userDto = mapper.Map<TaskDto>(value);

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
        [HttpDelete("{id}")]
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
