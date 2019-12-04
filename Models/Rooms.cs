using HotelManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Models
{
    public class Rooms
    {
        public int Id { get; set; }
        [Display(Name = "Room Number")] 
        public int RoomNumebr { get; set; }

        [Display(Name = "Room Type")]
        [Required]
        public Types? types { get; set; }
        [Display(Name = "Mobile Number/Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Is Reserved")]
        public bool Researved { get; set; }
    }
}
