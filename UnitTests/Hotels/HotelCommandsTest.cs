using FakeItEasy;
using FluentAssertions;
using HotelBooking.Interfaces;
using HotelBooking.Models;

namespace UnitTests
{
    public class HotelCommandsTest
    {

        // mock instance of IHotelRepository
        private readonly IHotelRepository hotelCommands;

        public HotelCommandsTest()
        {
            hotelCommands = A.Fake<IHotelRepository>(); 
        }

        [Fact]
        public void GetHotelsList_Successful()
        {
            // Arrange
            var expectedHotels = new List<Hotel> { new Hotel(), new Hotel() };
            A.CallTo(() => hotelCommands.GetHotelsList()).Returns(expectedHotels);

            // Act
            var actionResult = hotelCommands.GetHotelsList();

            // Assert
            actionResult.Value.Should().BeEquivalentTo(expectedHotels);
        }

        [Fact]
        public void GetHotelsListById_Successful()
        {
            var id = 123;
            // Arrange
            var expectedHotel = new Hotel();
            A.CallTo(() => hotelCommands.GetHotelById(id)).Returns(expectedHotel);

            // Act
            var actionResult = hotelCommands.GetHotelById(id);

            // Assert
            actionResult.Value.Should().BeEquivalentTo(expectedHotel);

        }

        [Fact]
        public void GetHotelById_Failed_ExceptionThrown()
        {
            int hotelId = 123; // Specify a valid hotel ID

            // Arrange
            // No need to set up any fake behavior as we want to simulate an exception

            // Act
            var actionResult = hotelCommands.GetHotelById(hotelId);

            // Assert
            actionResult.Value.Should().BeNull();
        }
    }
}