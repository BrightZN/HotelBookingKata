using System.Threading.Tasks;

namespace HotelBookingKata
{
    public interface IRoomRepository
    {
        Task SaveRoomAsync(Room room);
    }
}