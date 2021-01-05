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
    public class UpsertModel : PageModel
    {
        private AppDbContext _db;

        public UpsertModel(AppDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Car Car { get; set; }
        public async Task<IActionResult> OnGet(int? id)
        {
            Car = new Car();
            if (id == null)
            {
                //create handler
                return Page();
            }
            //update handler
            Car = await _db.Car.FirstOrDefaultAsync(u => u.Id == id);
            if (Car == null)
            {

                Log.Logger.Error("Car not found");
                return NotFound();
            }
            Log.Logger.Information("Car ID:{id} B_NAME:{brand} M_NAME:{model} P_NUMBER:{plate} is being edited(Upsert).", Car.Id, Car.Brand_Name, Car.Model_Name, Car.Plate_Number);

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Car.Id == 0)
                {
                    _db.Car.Add(Car);

                    Log.Logger.Information("Car B_NAME:{brand} M_NAME:{model} P_NUMBER:{plate} has been added(Upsert).", Car.Brand_Name, Car.Model_Name, Car.Plate_Number);
                }
                else
                {
                    _db.Car.Update(Car);//Update do zmiany wszystkich wlasciwosci

                    Log.Logger.Information("Car ID:{id} B_NAME:{brand} M_NAME:{model} P_NUMBER:{plate} has changed values(Upsert).", Car.Id, Car.Brand_Name, Car.Model_Name, Car.Plate_Number);
                }

                await _db.SaveChangesAsync();

                return RedirectToPage("Index");

            }
            return RedirectToPage();
        }
    }
}
