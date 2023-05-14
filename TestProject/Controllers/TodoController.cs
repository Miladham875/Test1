using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Domain;
using Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using TestProject.Areas.Identity.Data;

namespace TestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        //private readonly TestApplicationContext _context;

        private readonly IFruitService _fruitService;
        private readonly UserManager<TestProjectUser> _userManager;
        public TodoController(IFruitService fruitService, UserManager<TestProjectUser> userManager)
        {
            _fruitService = fruitService;
            _userManager = userManager;
        }

        // GET: api/Todo
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Fruit>>> GetFruits(string searchText)
        {
            try
            {         
            


            var result=await _fruitService.GetAll();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                result = result.Where(a => a.Color.Contains(searchText) || a.Name.Contains(searchText)
                  || a.Id.ToString().Contains(searchText)).ToList();
            }
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fruit>> GetFruit(int id)
        {
            var fruit = await _fruitService.GetById(id);

            if (fruit == null)
            {
                return NotFound();
            }

            return fruit;
        }

        // PUT: api/Todo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutFruit(int id, Fruit fruit)
        {
            if (id != fruit.Id)
            {
                return BadRequest();
            }

            var currentUser=await _userManager.GetUserAsync(HttpContext.User);
            if (fruit.CreatorUserId != currentUser.Id)
            {
                return BadRequest("This object was created by another user and you can not edit or delete it");
            }



            try
            {
                await _fruitService.Update(fruit);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FruitExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Todo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Fruit>> PostFruit(Fruit fruit)
        {
            await _fruitService.CreateReport(fruit);           

            return CreatedAtAction("GetFruit", new { id = fruit.Id }, fruit);
        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteFruit(int id)
        {
            var fruit = await _fruitService.GetById(id);
            var currentUser=await _userManager.GetUserAsync(HttpContext.User);
            if (fruit.CreatorUserId != currentUser.Id)
            {
                return BadRequest("This object was created by another user and you can not edit or delete it");
            }
            
            if (fruit == null)
            {
                return NotFound();
            }

            await _fruitService.Delete(id);
           

            return NoContent();
        }

        private  bool FruitExists(int id)
        {
            return (_fruitService.GetAll().Result)?.Any(e => e.Id == id)??false;
        }
    }
}
