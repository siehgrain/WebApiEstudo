using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCarApi.Context;
using MyCarApi.Models;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyCar2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public CarsController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetCars()
        {
            return Ok
            (new
            {
                success = true,
                data = await _appDbContext.Cars.ToListAsync()
            });
        }
        [HttpPost]
        public async Task<IActionResult> CreateCar(Car car)
        {

            _appDbContext.Cars.Add(car);
            await _appDbContext.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                data = car
            });
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCar(Car car)
        {

            _appDbContext.Entry(car).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                data = car
            });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _appDbContext.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            _appDbContext.Cars.Remove(car);
            await _appDbContext.SaveChangesAsync();
            return NoContent();

        }
    }
}
