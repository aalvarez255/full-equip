using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FullEquip.Api.Dto;
using FullEquip.Api.Dto.Extensions;
using FullEquip.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FullEquip.Api.Controllers
{
    public class CoursesController : BaseController
    {
        private readonly ICourseService _service;

        public CoursesController(
            ICourseService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> Get()
        {
            return Ok((await _service.GetAllAsync()).Select(x => x.ToDto()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDetailDto>> Get(Guid id)
        {
            return Ok((await _service.GetDetailAsync(id)).ToDetailDto());
        }

        [HttpGet("{id}/path")]
        public async Task<ActionResult<CourseTreeDto>> GetTree(Guid id)
        {
            return Ok((await _service.GetTreeAsync(id)).ToTreeDto());
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
