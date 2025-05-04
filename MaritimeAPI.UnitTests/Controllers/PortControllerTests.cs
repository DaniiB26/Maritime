using MaritimeAPI.Controllers;
using MaritimeAPI.Data;
using MaritimeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaritimeAPI.UnitTests.Controllers
{

    public class PortControllerTests
    {

        private async Task<ApplicationDbContext> GetDbContextWithData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) //din cauza ca testele suprascriu baza de date
                .Options;

            var context = new ApplicationDbContext(options);

            var country = new Country { Name = "Romania" };
            context.Countries.Add(country);
            await context.SaveChangesAsync();

            var port = new Port
            {
                Name = "Constanta",
                CountryId = country.Id,
            };

            context.Ports.Add(port);
            await context.SaveChangesAsync();

            return context;
        }

        [Fact]
        public async Task GetPorts_ReturnsAllPorts()
        {

            var context = await GetDbContextWithData();
            var controller = new PortController(context);

            var result = await controller.GetPorts();

            var actionResult = Assert.IsType<ActionResult<IEnumerable<Port>>>(result);
            var ports = Assert.IsAssignableFrom<IEnumerable<Port>>(actionResult.Value);
            Assert.Single(ports);
        }

        [Fact]
        public async Task GetPort_ReturnsPortById()
        {
            var context = await GetDbContextWithData();
            var controller = new PortController(context);

            var port = context.Ports.First();

            var result = await controller.GetPort(port.Id);

            var actionResult = Assert.IsType<ActionResult<Port>>(result);
            var returned = Assert.IsType<Port>(actionResult.Value);
            Assert.Equal(port.Id, returned.Id);
        }

        [Fact]
        public async Task GetPort_InvalidId_ReturnsNotFound()
        {

            var context = await GetDbContextWithData();
            var controller = new PortController(context);

            var result = await controller.GetPort(0);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task AddItem_CreatesPort()
        {
            var context = await GetDbContextWithData();
            var controller = new PortController(context);

            var newPort = new Port
            {
                Name = "Barcelona",
                CountryId = context.Countries.First().Id
            };

            var result = await controller.AddItem(newPort);

            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdPort = Assert.IsType<Port>(created.Value);
            Assert.Equal(newPort.Name, createdPort.Name);
        }

        [Fact]
        public async Task UpdatePort_ValidUpdate_ReturnsNoContent()
        {
            var context = await GetDbContextWithData();
            var controller = new PortController(context);

            var port = context.Ports.First();
            port.Name = "Tomis";

            var result = await controller.UpdatePort(port.Id, port);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdatePort_IdMismatch_ReturnsBadRequest()
        {
            var context = await GetDbContextWithData();
            var controller = new PortController(context);

            var port = context.Ports.First();

            var result = await controller.UpdatePort(999, port);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeletePort_RemovesPort()
        {
            var context = await GetDbContextWithData();
            var controller = new PortController(context);

            var port = context.Ports.First();

            var result = await controller.DeletePort(port.Id);

            Assert.IsType<NoContentResult>(result);
            Assert.Null(await context.Ports.FindAsync(port.Id));
        }

        [Fact]
        public async Task DeletePort_InvalidId_ReturnsNotFound()
        {
            var context = await GetDbContextWithData();
            var controller = new PortController(context);

            var result = await controller.DeletePort(999);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}