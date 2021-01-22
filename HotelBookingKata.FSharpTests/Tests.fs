module Tests

open System
open Xunit


module HotelServiceTests =
    open HotelBookingKata.FSharp

    [<Fact>]
    let ``addHotel - new hotel - creates hotel`` () =
        let hotelId = HotelId "h4ck"
        let hotelName = HotelName "Hacker's Paradise"

        let result = HotelService.addHotel hotelId hotelName

        let expected = HotelService.HotelAdded

        Assert.Equal(expected, result)
