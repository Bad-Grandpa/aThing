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
    public class DownloadTableModel : PageModel
    {
        private AppDbContext _db;

        public DownloadTableModel(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var cars = await _db.Car.ToListAsync();
            Log.Logger.Information("Page: DownloadTableModel Function: Task<IActionResult> OnPostAsync() Car table is being downloaded.");
            Response.Headers.Add("Content-Disposition", "attachment; filename=CarData.json");
            return new FileContentResult(JsonSerializer.SerializeToUtf8Bytes(cars), "application/json");
        }
    }
}
