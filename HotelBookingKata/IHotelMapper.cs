namespace HotelBookingKata
{
    public interface IHotelMapper<out THotelInfo>
    {
        THotelInfo Map(Hotel hotel);
    }
}