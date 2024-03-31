using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Data;
using SuperHeroAPI.Entities;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public SuperHeroController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHeroes()
        {
            var heroes = await _dataContext.SuperHeroes.ToListAsync();

            return Ok(heroes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<SuperHero>>> GetHeroById(int id)
        {
            var hero = await _dataContext.SuperHeroes.FindAsync(id);

            if (hero is null)
                return NotFound("Hero Not Found");

            return Ok(hero);
        }
    }
}
