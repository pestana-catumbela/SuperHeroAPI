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

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero superHero)
        {
            _dataContext.SuperHeroes.Add(superHero);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero superHero)
        {
            var dbHero = await _dataContext.SuperHeroes.FindAsync(superHero.Id);
            
            if (dbHero is null)
                return NotFound("Hero Not Found");

            dbHero.Name = superHero.Name;
            dbHero.FirstName = superHero.FirstName;
            dbHero.LastName = superHero.LastName;
            dbHero.Place = superHero.Place;

            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.SuperHeroes.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var dbHero = await _dataContext.SuperHeroes.FindAsync(id);

            if (dbHero is null)
                return NotFound("Hero Not Found");

            _dataContext.SuperHeroes.Remove(dbHero);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.SuperHeroes.ToListAsync());
        }
    }
}
