using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelBookingKata
{
    public class Hotel
    {
        public Hotel(HotelId id, HotelName name, params Room[] rooms)
        {
            Id = id;
            Name = name;

            _rooms = rooms.ToList();
        }

        public HotelId Id { get; set; }
        public HotelName Name { get; set; }
        public int NumberOfRooms => _rooms.Count;

        private readonly List<Room> _rooms = new List<Room>();

        public IEnumerable<Room> Rooms => _rooms;

        public void SetRoom(RoomNumber roomNumber, RoomType roomType)
        {
            if(_rooms.Any(r => r.Number == roomNumber))
            {
                var roomToUpdate = _rooms.First(r => r.Number == roomNumber);

                roomToUpdate.Type = roomType;
            }
            else
            {
                _rooms.Add(new Room(roomNumber, roomType));
            }
        }
    }
}