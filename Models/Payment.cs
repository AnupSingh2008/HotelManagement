using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Models
{
    public class Payment
    {
        public int Id { get; set; }
        [Display(Name ="Client Id")]
        public int ClientId { get; set; }

        [Display(Name = "Room Number")]
        public int RoomNumber { get; set; }

        [Display(Name = "No. of days")]
        public int NoOfDays { get; set; }

        [Display(Name = "Charges per day")]
        public int TotalAmount { get; set; }

        
    }
}
