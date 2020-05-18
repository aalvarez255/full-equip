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
    public class StudentsController : BaseController
    {
        private readonly IStudentService _service;

        public StudentsController(
            IStudentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> Get()
        {
            return Ok((await _service.GetAllAsync()).Select(x => x.ToDto()));
        }      
    }
}
