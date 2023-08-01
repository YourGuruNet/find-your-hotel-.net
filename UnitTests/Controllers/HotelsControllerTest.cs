using System;
using FakeItEasy;
using FluentAssertions;
using HotelBooking.Controllers;
using HotelBooking.Interfaces;
using HotelBooking.Models;
using Microsoft.AspNetCore.Http;

namespace UnitTests.Controllers
{
	public class HotelsControllerTest
    {
        #region Repositorys
        private HotelsController hotelsController;
        private readonly IHotelRepository hotelsCommands;

        public HotelsControllerTest()
        {
            hotelsCommands = A.Fake<IHotelRepository>();
            hotelsController = new HotelsController(hotelsCommands);
        }
        #endregion

        [Fact]
        public void GetHotelsList_Successful()
        {
            // Arrange
            var expectedHotels = new List<Hotel> { new Hotel(), new Hotel() };
            A.CallTo(() => hotelsCommands.GetHotelsList()).Returns(expectedHotels);

            // Act
            var actionResult = hotelsController.GetHotelsList();

            // Assert
            actionResult.Value.Should().BeEquivalentTo(expectedHotels);
        }

        [Fact]
        public void GetHotelsListById_Successful()
        {
            var id = 123;
            // Arrange
            var expectedHotels = new Hotel();
            A.CallTo(() => hotelsCommands.GetHotelById(id)).Returns(expectedHotels);

            // Act
            var actionResult = hotelsController.GetHotelById(id);

            // Assert
            actionResult.Value.Should().BeEquivalentTo(expectedHotels);
        }




    }
}

