using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Garage.Model;
using System.Text.Json;
using Serilog;

namespace Garage.Pages.CarList
{
    public class DownloadModel : PageModel
    {
        private AppDbContext _db;

        public DownloadModel(AppDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Car Car { get; set; }
        public async Task OnGet(int id)
        {
            Car = await _db.Car.FindAsync(id);
            Log.Logger.Information("Page: DownloadModel Function: async Task OnGet Car ID:{id} B_NAME:{brand} M_NAME:{model} P_NUMBER:{plate} is being viewed.", Car.Id, Car.Brand_Name, Car.Model_Name, Car.Plate_Number);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var CarFromDb = await _db.Car.FindAsync(Car.Id);
            Log.Logger.Information("Page: DownloadModel Function: Task<IActionResult> OnPostAsync() Car ID:{id} B_NAME:{brand} M_NAME:{model} P_NUMBER:{plate} is being downloaded.", Car.Id, Car.Brand_Name, Car.Model_Name, Car.Plate_Number);
            Response.Headers.Add("Content-Disposition", "attachment; filename=CarData.json");
            return new FileContentResult(JsonSerializer.SerializeToUtf8Bytes(CarFromDb), "application/json");
        }
    }
}
