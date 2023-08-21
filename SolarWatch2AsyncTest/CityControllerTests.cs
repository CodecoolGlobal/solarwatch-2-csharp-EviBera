using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SolarWatch2Async.Controllers;
using SolarWatch2Async.Services;

namespace SolarWatch2Async2AsyncTest
{
    [TestFixture]
    public class CityControllerTests
    {
        private Mock<ILogger<CityController>> _loggerMock;
        private Mock<ICityService> _cityServiceMock;
        private Mock<IJsonProcessor> _jsonProcessorMock;
        private CityController _controller;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<CityController>>();
            _cityServiceMock = new Mock<ICityService>();
            _jsonProcessorMock = new Mock<IJsonProcessor>();
            _controller = new CityController(_loggerMock.Object, _cityServiceMock.Object);
        }

        [Test]
        public async Task GetReturnsNotFoundResultIfCityServiceFails()
        {
            //arrange
            _cityServiceMock.Setup(item => item.GetCityAsync(It.IsAny<string>(), It.IsAny<DateOnly>()))
              .Throws(new Exception());

            //act
            var result = await _controller.GetAsync("Budapest", DateOnly.FromDateTime(DateTime.Now));

            //assert
            Assert.That(result.Result, Is.InstanceOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task GetReturnsOkIfCityServiceWorks()
        {
            //arrange
            var expectedCity = new SolarWatch2Async.Models.City();
            _cityServiceMock.Setup(item => item.GetCityAsync(It.IsAny<string>(), It.IsAny<DateOnly>()))
                .ReturnsAsync(expectedCity);

            //act
            var result = await _controller.GetAsync("Budapest", DateOnly.FromDateTime(DateTime.Now));

            //assert
            Assert.IsInstanceOf(typeof(OkObjectResult), result.Result);
            Assert.That(((OkObjectResult)result.Result).Value, Is.EqualTo(expectedCity));
        }

        [Test]
        public async Task GetReturnsValidCityIfCityServiceWorks()
        {
            DateOnly day = DateOnly.FromDateTime(DateTime.Now);
            SolarWatch2Async.Models.City mockCity = new SolarWatch2Async.Models.City()
            {
                CityName = "London"
            };

            _cityServiceMock.Setup(item => item.GetCityAsync("London", day))
                .ReturnsAsync(mockCity);

            var result = await _controller.GetAsync("London", day);

            Assert.IsInstanceOf(typeof(OkObjectResult), result.Result);
            Assert.That(((OkObjectResult)result.Result).Value, Is.EqualTo(mockCity));

        }

        [Test]
        public async Task GetReturnsTheGivenCityIfCityServiceWorks()
        {
            DateOnly day = DateOnly.FromDateTime(DateTime.Now);
            SolarWatch2Async.Models.City mockCity = new SolarWatch2Async.Models.City()
            {
                CityName = "London"
            };

            _cityServiceMock.Setup(item => item.GetCityAsync("London", day))
                .ReturnsAsync(mockCity);

            var result = await _controller.GetAsync("London", day);

            Assert.IsInstanceOf(typeof(OkObjectResult), result.Result);
            Assert.That(((SolarWatch2Async.Models.City)((OkObjectResult)(result.Result)).Value).CityName, Is.EqualTo("London") );
        }
    }
}