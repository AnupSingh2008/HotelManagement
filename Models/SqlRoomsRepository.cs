using HotelManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Models
{
    public class SqlRoomRepository : IRoom
    {
        private AppDBContext _context;

        public SqlRoomRepository(AppDBContext context)
        {
            _context = context;

        }


        public Rooms Add(Rooms rooms)
        {
            _context.Rooms.Add(rooms);
            _context.SaveChanges();
            return rooms;
        }
        public Rooms GetRoomByRoomNumber(int RoomNumber)
        {
            var room = (from obj in _context.Rooms
                        where obj.RoomNumebr == RoomNumber
                        select obj).FirstOrDefault();

            return room;
        }

        public Rooms GetRoomByRoomNumber(int RoomNumber,int Id)
        {
            var room = (from obj in _context.Rooms
                        where obj.RoomNumebr == RoomNumber 
                        && obj.Id != Id
                        select obj).FirstOrDefault();

            return room;
        }

        public IEnumerable<Rooms> GetAllRooms()
        {
            return _context.Rooms;
        }


        public Rooms GetRoom(int Id)
        {
            return _context.Rooms.Find(Id);
        }



        public Rooms Update(Rooms roomchange)
        {
            var room = _context.Rooms.Attach(roomchange);
            room.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return roomchange;
        }

        Rooms IRoom.Delete(int id)
        {
            throw new NotImplementedException();
        }
    }

}
