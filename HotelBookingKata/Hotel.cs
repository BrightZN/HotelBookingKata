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

        public HotelId Id { get; }
        public HotelName Name { get; }

        private readonly List<RoomTypeConfig> _roomTypeConfigs;

        public void SetRoom(RoomType roomType, int roomCount)
        {
            if (HasConfigFor(roomType))
            {
                UpdateConfig(roomType, roomCount);
            }
            else
                AddConfig(roomType, roomCount);
        }

        public bool DoesNotOffer(RoomType roomType)
        {
            return RoomCountFor(roomType) == 0;
        }

        private void AddConfig(RoomType roomType, int roomCount)
        {
            _roomTypeConfigs.Add(new RoomTypeConfig(roomType, roomCount));
        }

        private void UpdateConfig(RoomType roomType, int roomCount)
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