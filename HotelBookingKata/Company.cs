using System;

namespace HotelBookingKata
{
    public class Company
    {
        public Company(CompanyId id)
        {
            Id = id;
        }

        public CompanyId Id { get; }
        public bool HasPolicy => true;

        public bool CanBook(RoomType roomType)
        {
            return roomType == RoomType.Standard;
        }
    }
}