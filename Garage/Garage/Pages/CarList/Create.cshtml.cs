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
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _db;
        

        public CreateModel(AppDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Car Car { get; set; }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await _db.Car.AddAsync(Car);
                await _db.SaveChangesAsync();

                Log.Logger.Information("Page: Create Function: async Task<IActionResult> OnPost() Car ID:{id} B_NAME:{brand} M_NAME:{model} P_NUMBER:{plate} has been created.", Car.Id, Car.Brand_Name, Car.Model_Name, Car.Plate_Number);

                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
