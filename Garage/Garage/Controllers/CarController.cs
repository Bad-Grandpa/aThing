using Garage.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage.Controllers
{
    [Route("api/car")]
    [ApiController]
    public class CarController : Controller
    {
        private readonly AppDbContext _db;

        public CarController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Car.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var carFromDb = await _db.Car.FirstOrDefaultAsync(u => u.Id == id);
            if (carFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _db.Car.Remove(carFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" });
        }
    }
}
