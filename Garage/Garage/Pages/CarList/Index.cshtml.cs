using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Garage.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Garage.Pages.CarList
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _db;

        public IndexModel(AppDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Car> Cars { get; set; }

        public async Task OnGet()
        {
            Cars = await _db.Car.ToListAsync();
            Log.Logger.Information("List of Cars in DB has been requested.");
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var car = await _db.Car.FindAsync(id);
            if (car == null)
            {
                NotFound();
                Log.Logger.Error("Cars not found.");
            }
            _db.Car.Remove(car);
            await _db.SaveChangesAsync();

            Log.Logger.Information("Car ID:{id} B_NAME:{brand} M_NAME:{model} P_NUMBER:{plate} has been removed.", car.Id, car.Brand_Name, car.Model_Name, car.Plate_Number);

            return RedirectToPage("Index");
        }
    }
}
