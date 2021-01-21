using System.Collections.Generic;

namespace HotelBookingKata
{
    public class Company
    {
        private CompanyId id;

        public Company(CompanyId id)
        {
            this.id = id;
        }

        private readonly List<RoomType> _roomTypes = new List<RoomType>();

        public IEnumerable<RoomType> RoomTypes => _roomTypes;
    }
}