using HotelBooking.Interfaces;

namespace UnitTestNUnit;

public class HotelCommandsTest
{
    private IHotelRepository hotelCommands;
    [SetUp]
    public void Setup()
    {
        this.hotelCommands = hotelCommands;
    }

    [Test]
    public void GetHotelsList_Successful()
    {

        var hotels = hotelCommands.GetHotelsList();
        Assert.Pass();
    }
}
