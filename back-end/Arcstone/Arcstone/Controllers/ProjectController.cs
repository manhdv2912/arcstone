using Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arcstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService
            )
        {
            _projectService = projectService;
        }
        // GET: ProjectController
        public async Task<IEnumerable<Project>> GetAll()
        {
            return await _projectService.GetAll();
        }

        // GET: ProjectController/Details/5

    }
}
