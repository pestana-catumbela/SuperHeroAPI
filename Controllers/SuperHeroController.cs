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

        /// <summary>
        /// Get all heroes
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        public async Task<IActionResult> GetAllHeroes()
        {
            var heroes = await _dataContext.SuperHeroes.ToListAsync();

            return Ok(heroes);
        }

        /// <summary>
        /// Get hero by id
        /// </summary>
        /// <param name="id">Id: entry param</param>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<List<SuperHero>>> GetHeroById(int id)
        {
            var hero = await _dataContext.SuperHeroes.FindAsync(id);

            if (hero is null)
                return NotFound("Hero Not Found");

            return Ok(hero);
        }

        /// <summary>
        /// Create Hero
        /// </summary>
        /// <param name="superHero">Name: entry param</param>
        /// <response code="200">Success</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero superHero)
        {
            _dataContext.SuperHeroes.Add(superHero);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.SuperHeroes.ToListAsync());
        }

        /// <summary>
        /// Update hero
        /// </summary>
        /// <param name="superHero">Name: entry param</param>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
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

        /// <summary>
        /// Delete hero
        /// </summary>
        /// <param name="id">Name: entry param</param>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
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
