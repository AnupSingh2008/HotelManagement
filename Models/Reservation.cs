using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        [Display(Name ="Clinet Id")]
        public int ClientId { get; set; }
        [Display(Name ="Room Number")]
        public int RoomNumber { get; set; }
        [Display(Name ="Date In")]
        [DataType(DataType.DateTime)]
        public DateTime DateIN { get; set; }

        [Display(Name = "Date Out")]
        [DataType(DataType.DateTime)]
        public DateTime DateOut { get; set; }
    }
}
