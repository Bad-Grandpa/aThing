using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Garage.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace Garage.Pages.CarList
{
    public class EditModel : PageModel
    {
        private AppDbContext _db;

        public EditModel(AppDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Car Car { get; set; }
        public async Task OnGet(int id)
        {
            Car = await _db.Car.FindAsync(id);
            Log.Logger.Information("Page: EditModel Function: async Task OnGet Car ID:{id} B_NAME:{brand} M_NAME:{model} P_NUMBER:{plate} is being edited.", Car.Id, Car.Brand_Name, Car.Model_Name, Car.Plate_Number);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var CarFromDb = await _db.Car.FindAsync(Car.Id);
                CarFromDb.Brand_Name = Car.Brand_Name;
                CarFromDb.Model_Name = Car.Model_Name;
                CarFromDb.Plate_Number = Car.Plate_Number;

                Log.Logger.Information("Car ID:{id} B_NAME:{brand} M_NAME:{model} P_NUMBER:{plate} has changed values.", Car.Id, Car.Brand_Name, Car.Model_Name, Car.Plate_Number);

                await _db.SaveChangesAsync();

                return RedirectToPage("Index");

            }
            return RedirectToPage();
        }
    }
}
