﻿namespace HotelBookingKata
{
    public abstract class BookingAgent
    {

        protected BookingPolicy policy;

        protected BookingAgent(BookingPolicy policy)
        {
            this.policy = policy;
        }

        public bool CanBook(RoomType roomType)
        {
            return NoPolicy() || policy.AllowsBookingFor(roomType);
        }

        public void ChangePolicy(BookingPolicy newPolicy)
        {
            policy = newPolicy;
        }

        public bool NoPolicy() => policy == null;
    }
}