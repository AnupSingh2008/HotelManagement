using HotelManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.ViewModels
{
    public class ReservationViewModel
    {
        public ReservationViewModel()
        {

            Clients = new List<Clients>();
            Rooms = new List<Rooms>();

        }
        public int Id { get; set; }
        [Display(Name = "Clinet Id")]
        public int ClientId { get; set; }
        [Display(Name = "Room Number")]
        public int RoomId { get; set; }
        [Display(Name = "Date In")]
        [DataType(DataType.DateTime)]
        public DateTime DateIN { get; set; }

        [Display(Name = "Date Out")]
        [DataType(DataType.DateTime)]
        public DateTime DateOut { get; set; }
        [Display(Name = "Client Name")]
        public List<Clients> Clients { get; set; }
        [Display(Name = "Room Number")]
        public List<Rooms> Rooms { get; set; }
    }
}
