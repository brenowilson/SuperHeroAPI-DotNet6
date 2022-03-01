using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> heroes = new List<SuperHero>
        {
            new SuperHero {
                Id = 1,
                Name = "Spider Man",
                FirstName = "Peter",
                LastName = "Parker",
                Place = "Queens"
            },

            new SuperHero {
                Id = 2,
                Name = "Iron Man",
                FirstName = "Anthony",
                LastName = "Stark",
                Place = "New York City"
            },

            new SuperHero {
                Id = 3,
                Name = "Wonder Woman",
                FirstName = "Diana",
                LastName = "",
                Place = "Temisera"
            }
        };
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null) return BadRequest("Super Hero not found!");
            
            return Ok(hero);
        }

        /*[HttpGet("{name}")]
        public async Task<ActionResult<SuperHero>> Get(string name)
        {
            var hero = heroes.Find(h => h.Name == name);
            if (hero == null) return BadRequest("Super Hero not found!");

            return Ok(hero);
        }*/

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();

            return Ok("Hero added successfully!");
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var hero = await _context.SuperHeroes.FindAsync(request.Id);
            if (hero == null) return BadRequest("Super Hero not found!");

            hero.Name = request.Name;
            hero.FirstName = request.FirstName;
            hero.LastName = request.LastName;
            hero.Place = request.Place;

            await _context.SaveChangesAsync();

            return Ok("Hero Updated");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SuperHero>> Delete(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null) return BadRequest("Super Hero not found!");

            _context.SuperHeroes.Remove(hero);

            await _context.SaveChangesAsync();

            return Ok("Hero Deleted!");
        }
    }
}
