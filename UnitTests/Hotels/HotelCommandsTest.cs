

namespace UnitTests;
using Moq;

public class HotelCommandsTest
{
    [Fact]
    public void GetHotelsList_Successful()
    {
        var serviceMock = new Mock<IHotelRepository>();
        hotelCommands.GetHotelsList();
    }
}
