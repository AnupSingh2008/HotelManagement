using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelManagement.Models;
using HotelManagement.Models;
using HotelManagement.ViewModels;

namespace HotelManagement.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly AppDBContext _context;

        public ReservationsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reservation.ToListAsync());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public async Task<IActionResult> Create()
        {
            ReservationViewModel reservationViewModels = new ReservationViewModel();
            List<Clients> clients = _context.Clients.ToList();
            List<Rooms> rooms = _context.Rooms.ToList();
            reservationViewModels.Rooms = rooms;
            reservationViewModels.Clients = clients;
            return View(reservationViewModels);
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReservationViewModel reservation)
        {
            if (ModelState.IsValid)
            {
                Reservation reservation1 = new Reservation() 
                { 
                    ClientId = reservation.ClientId,
                    RoomNumber = reservation.RoomId,
                    DateIN = reservation.DateIN,
                    DateOut = reservation.DateOut
                };

                _context.Add(reservation1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation.FindAsync(id);

            ReservationViewModel reservationViewModels = new ReservationViewModel();
            List<Clients> clients = _context.Clients.ToList();
            List<Rooms> rooms = _context.Rooms.ToList();
            reservationViewModels.Id = reservation.Id;
            reservationViewModels.DateIN = reservation.DateIN;
            reservationViewModels.DateOut = reservation.DateOut;
            reservationViewModels.ClientId = reservation.ClientId;
            reservationViewModels.RoomId = reservation.RoomNumber;
            reservationViewModels.Rooms = rooms;
            reservationViewModels.Clients = clients;
            if (reservationViewModels == null)
            {
                return NotFound();
            }
            return View(reservationViewModels);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ReservationViewModel reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Reservation reservation1 = new Reservation()
                    {
                        Id = reservation.Id,
                        ClientId = reservation.ClientId,
                        RoomNumber = reservation.RoomId,
                        DateIN = reservation.DateIN,
                        DateOut = reservation.DateOut
                    };
                    _context.Update(reservation1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservation.FindAsync(id);
            _context.Reservation.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservation.Any(e => e.Id == id);
        }
    }
}
