namespace HotelBookingKata
{
    public interface IHotelMapper<out TResult>
    {
        TResult Map(Hotel hotel);
    }
}