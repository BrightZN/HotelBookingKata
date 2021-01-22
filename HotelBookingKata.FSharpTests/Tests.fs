module Tests

open System
open Xunit


module HotelServiceTests =
    open HotelBookingKata.FSharp

    let hotelId = HotelId "h4ck"
    let hotelName = HotelName "Hacker's Paradise"
        
        
    let doesHotelExist exists _ = 
        async { 
            return exists
        }

    let getHotelById hotels hotelId = 
        async {
            return hotels 
            |> List.tryFind (fun h -> h.Id = hotelId)
        }

    [<Fact>]
    let ``addHotel - new hotel - returns HotelService.HotelAdded`` () =
        let mutable savedHotel = None

        let addHotel hotel =
            async {
                savedHotel <- Some hotel
                return Ok ()
            }

        async {

            let! result = HotelService.addHotel (doesHotelExist false) addHotel hotelId hotelName

            let expected = HotelService.HotelAdded

            Assert.Equal(expected, result)


            Assert.Equal(Some { Id = hotelId; Name = hotelName; RoomTypeConfigs = List.empty }, savedHotel)
        }

    [<Fact>]
    let ``addHotel - existing hotel - returns HotelService.HotelAlreadyExists`` () =
        let addHotel hotel =
            async {
                return Error ()
            }

        async {
            let! result = HotelService.addHotel (doesHotelExist true) addHotel hotelId hotelName

            let expected = HotelService.HotelAlreadyExists

            Assert.Equal(expected, result)
        }

    [<Fact>]
    let ``setRoom - existing hotel and new room - returns HotelService.RoomSet`` () =
        let mutable existingHotel = { Id = hotelId; Name = hotelName; RoomTypeConfigs = List.empty }

        let getHotelById = getHotelById [existingHotel]

        let updateHotel hotel =
            async {
                existingHotel <- hotel
                return Ok ()
            }

        async {
            let! result = HotelService.setRoom getHotelById updateHotel hotelId RoomType.Standard (NumberOfRooms 5)

            Assert.Equal(HotelService.RoomSet, result)

            Assert.NotEmpty(existingHotel.RoomTypeConfigs)
            Assert.Contains({ Type = RoomType.Standard; NumberOfRooms = NumberOfRooms 5 }, existingHotel.RoomTypeConfigs)
        }
        
