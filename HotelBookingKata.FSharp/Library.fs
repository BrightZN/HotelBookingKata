namespace HotelBookingKata.FSharp

type HotelId = HotelId of string
type HotelName = HotelName of string

module HotelService =
    type AddHotelResult = 
    | HotelAdded

    let addHotel hotelId hotelName = HotelAdded
