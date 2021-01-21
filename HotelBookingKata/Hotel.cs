using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelBookingKata
{
    public class Hotel
    {
        public Hotel(HotelId id, HotelName name, params RoomTypeConfig[] roomTypeConfigs)
        {
            Id = id;
            Name = name;

            _roomTypeConfigs = roomTypeConfigs.ToList();
        }

        public HotelId Id { get; set; }
        public HotelName Name { get; set; }

        private readonly List<RoomTypeConfig> _roomTypeConfigs;

        public IEnumerable<RoomTypeConfig> RoomTypeConfigs => _roomTypeConfigs;

        public void SetRoom(RoomType roomType, int roomCount)
        {
            if (HasConfigFor(roomType))
            {
                UpdateRoomTypeConfig(roomType, roomCount);
            }
            else
                AddRoomTypeConfig(roomType, roomCount);
        }

        private void AddRoomTypeConfig(RoomType roomType, int roomCount)
        {
            _roomTypeConfigs.Add(new RoomTypeConfig(roomType, roomCount));
        }

        private void UpdateRoomTypeConfig(RoomType roomType, int roomCount)
        {
            var roomTypeConfig = _roomTypeConfigs.First(r => r.HasType(roomType));

            roomTypeConfig.Count = roomCount;
        }

        private bool HasConfigFor(RoomType roomType)
        {
            return _roomTypeConfigs.Any(r => r.HasType(roomType));
        }

        public int RoomCountFor(RoomType roomType)
        {
            return _roomTypeConfigs.Where(r => r.HasType(roomType))
                .Select(r => r.Count)
                .FirstOrDefault();
        }
    }
}