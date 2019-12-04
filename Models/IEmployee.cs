using HotelManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Models
{
    public interface IEmployee
    {
        Employee GetEmployee(int Id);
        IEnumerable<Employee> GetAllEmployees();

        Employee Add(Employee employee);
        Employee Update(Employee employee);
        Employee Delete(int id);

    }

    public interface IRoom
    {
        Rooms GetRoom(int Id);
        IEnumerable<Rooms> GetAllRooms();
        Rooms GetRoomByRoomNumber(int Id);
        Rooms GetRoomByRoomNumber(int roomNumber,int Id);
        Rooms Add(Rooms rooms);
        Rooms Update(Rooms rooms);
        Rooms Delete(int id);

    }
}
