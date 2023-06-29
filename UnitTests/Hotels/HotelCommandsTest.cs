using HotelBooking.Interfaces;

namespace UnitTests;

public class HotelCommandsTest
{
    private readonly IHotelRepository hotelCommands;

    public HotelCommandsTest(IHotelRepository hotelCommands)
    {
        this.hotelCommands = hotelCommands;
    }

    [Fact]
    public void GetHotelsList_Successful()
    {
        hotelCommands.GetHotelsList();
    }
}
