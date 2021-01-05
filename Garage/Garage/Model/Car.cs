using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Garage.Model
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Brand_Name { get; set; }
        [Required]
        public string Model_Name { get; set; }

        [MaxLength(10)]
        public string Plate_Number { get; set; }


    }
}
