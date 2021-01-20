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
            if (HasRoom(roomNumber))
                UpdateExistingRoom(roomNumber, roomType);
            else
                AddNewRoom(roomNumber, roomType);
        }

        private void AddNewRoom(RoomNumber roomNumber, RoomType roomType)
        {
            _rooms.Add(new Room(roomNumber, roomType));
        }

        private void UpdateExistingRoom(RoomNumber roomNumber, RoomType roomType)
        {
            var roomToUpdate = _rooms.First(r => r.HasNumber(roomNumber));

            roomToUpdate.Type = roomType;
        }

        private bool HasRoom(RoomNumber roomNumber)
        {
            return _rooms.Any(r => r.HasNumber(roomNumber));
        }
    }
}