namespace HotelBookingKata.FSharp

type HotelId = HotelId of string
type HotelName = HotelName of string

type RoomType = Standard | Junior | Master | Presidential
type NumberOfRooms = NumberOfRooms of int

type RoomTypeConfig = 
    { Type: RoomType
      NumberOfRooms: NumberOfRooms }

module RoomTypeConfig =
    let updateIfMatch roomType numberOfRooms roomTypeConfig =
        if roomTypeConfig.Type = roomType then
            { roomTypeConfig with NumberOfRooms = numberOfRooms }
        else
            roomTypeConfig

type Hotel = 
    { Id: HotelId
      Name: HotelName
      RoomTypeConfigs: RoomTypeConfig list }

module Hotel =
    let addRoom roomType numberOfRooms hotel = 
        let rooms = hotel.RoomTypeConfigs @ [{ Type = roomType; NumberOfRooms = numberOfRooms}]
        { hotel with RoomTypeConfigs = rooms }

    let updateRoom roomType numberOfRooms hotel =
        { hotel with RoomTypeConfigs = hotel.RoomTypeConfigs |> List.map (RoomTypeConfig.updateIfMatch roomType numberOfRooms) }

    let setRoom roomType numberOfRooms hotel =
        let roomTypeConfig = hotel.RoomTypeConfigs |> List.tryFind (fun r -> r.Type = roomType)

        match roomTypeConfig with
        | None -> 
            addRoom roomType numberOfRooms hotel
        | Some _ ->
            updateRoom roomType numberOfRooms hotel
            

module HotelService =
    type AddHotelResult = 
    | HotelAdded
    | HotelAddingFailed
    | HotelAlreadyExists

    let addHotel doesHotelExist addHotel hotelId hotelName = 
        async {
            let! hotelExists = doesHotelExist hotelId

            if hotelExists then
                return HotelAlreadyExists
            else
                let! addResult = addHotel { Id = hotelId; Name = hotelName; RoomTypeConfigs = List.empty }

                return 
                    match addResult with
                    | Ok _ -> HotelAdded
                    | Error _ -> HotelAddingFailed
        }

    type SetRoomResult =
    | RoomSet
    | HotelUpdateFailed
    | HotelNotFound

    let setRoom getHotelById updateHotel hotelId roomType numberOfRooms = 
        async {
            let! hotel = getHotelById hotelId
            
            match hotel with
            | None -> 
                return HotelNotFound
            | Some hotel ->
                let! updateResult = 
                    hotel 
                    |> Hotel.setRoom roomType numberOfRooms
                    |> updateHotel

                match updateResult with
                | Error _ -> 
                    return HotelUpdateFailed
                | Ok _ -> 
                    return RoomSet
        }

    
