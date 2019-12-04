using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.Security;
using HotelManagement.Controllers;
using HotelManagement.Models;
using HotelManagement.ViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HotelManagement.Controllers
{
    [Route("[controller]/[action]")]
    public class RoomsController : Controller
    {
        private readonly IRoom _room;
        private readonly ILogger<RoomsController> _logger;
        
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IDataProtector protector;
        // GET: Rooms
        public RoomsController(ILogger<RoomsController> logger,
            IHostingEnvironment hostingEnvironment,
                              IDataProtectionProvider dataProtectionProvider,
                              DataProtectionPurposeStrings dataProtectionPurposeStrings,IRoom room)
        {
            this._room = room;
        }
        public ActionResult Index()
        {
            var model = this._room.GetAllRooms();

            return View(model);
        }

        // GET: Rooms/Details/5
        [Route("{id}")]
        public ViewResult Details(int id)
        {
           
           
            Rooms room = _room.GetRoom(id);
            if (room == null)
            {
                Response.StatusCode = 404;
                return View("RoomsNotFound", id);
            }

            RoomDetailsViewModel roomDetailsViewModel = new RoomDetailsViewModel()
            {
                room = room,
                roomDiscription = "Rooms Details"

            };
            return View(roomDetailsViewModel);

        }

        // GET: Rooms/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RoomsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var room = _room.GetRoomByRoomNumber(model.RoomNumebr);
                if (room == null)
                {
                    Rooms newRoom = new Rooms
                    {

                        RoomNumebr = model.RoomNumebr,
                        types = model.types,
                        PhoneNumber = model.PhoneNumber,
                        Researved = model.IsResearved
                    };
                    _room.Add(newRoom);
                    return RedirectToAction("Details", new { id = newRoom.Id });
                }
                else
                {
                    ModelState.AddModelError("", "Room number can not be duplicate.");
                }

            }
            return View();
        }

        // GET: Rooms/Edit/5
        public ActionResult Edit(int id)
        {
            Rooms room = _room.GetRoom(id);
           
            if (room == null)
            {

                RoomsViewModel roomsEditViewModel = new RoomsViewModel
                {
                    Id = room.Id,
                    RoomNumebr = room.RoomNumebr,
                    PhoneNumber = room.PhoneNumber,
                    types = room.types,
                    IsResearved = room.Researved
                };
                return View(roomsEditViewModel);

            }
            return View();

        }

        // POST: Rooms/Edit/5
        [HttpPost]
        public IActionResult Edit(RoomsViewModel model)
        {
            if (ModelState.IsValid)
            {
                Rooms room = _room.GetRoom(model.Id);
                var roomExist = _room.GetRoomByRoomNumber(model.RoomNumebr, model.Id);
                if (roomExist == null)
                {
                    room.Id = model.Id;
                    room.RoomNumebr = model.RoomNumebr;
                    room.types = model.types;
                    room.PhoneNumber = model.PhoneNumber;
                    room.Researved = model.IsResearved;

                    Rooms updateRooms = _room.Update(room);
                    return RedirectToAction("index");
                }

                else
                {
                    ModelState.AddModelError("", "Room number can not be duplicate.");
                }
            }
            
            return View();
        }

        // GET: Rooms/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Rooms/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}