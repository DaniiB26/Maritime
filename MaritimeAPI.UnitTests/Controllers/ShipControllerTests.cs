using MaritimeAPI.Controllers;
using MaritimeAPI.Data;
using MaritimeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaritimeAPI.UnitTests.Controllers
{
    public class ShipControllerTests
    {

        private async Task<ApplicationDbContext> GetDbContextWithData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ShipTestDb")
                .Options;

            var context = new ApplicationDbContext(options);

            if (!context.Ships.Any())
            {
                context.Ships.AddRange(
                    new Ship { Name = "Titanic", MaxSpeed = 22.5f },
                    new Ship { Name = "Evergreen", MaxSpeed = 21.8f }
                );
                await context.SaveChangesAsync();
            }

            return context;
        }

        [Fact]
        public async Task GetShips_ReturnsAllShips()
        {
            var context = await GetDbContextWithData();
            var controller = new ShipController(context);

            var result = await controller.GetShips();

            var actionResult = Assert.IsType<ActionResult<IEnumerable<Ship>>>(result);
            var ships = Assert.IsAssignableFrom<IEnumerable<Ship>>(actionResult.Value);
            Assert.Equal(2, ships.Count());
        }

        [Fact]
        public async Task GetShip_ReturnsShipById()
        {
            var context = await GetDbContextWithData();
            var controller = new ShipController(context);

            var result = await controller.GetShip(1);

            var actionResult = Assert.IsType<ActionResult<Ship>>(result);
            var ship = Assert.IsType<Ship>(actionResult.Value);
            Assert.Equal("Titanic", ship.Name);
        }

        [Fact]
        public async Task GetShip_ReturnsNotFound_InvalidId()
        {
            var context = await GetDbContextWithData();
            var controller = new ShipController(context);

            var result = await controller.GetShip(0);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task AddItem_CreatesShip()
        {
            var context = await GetDbContextWithData();
            var controller = new ShipController(context);
            var newShip = new Ship { Name = "Poseidon", MaxSpeed = 25.1f };

            var result = await controller.AddItem(newShip);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var ship = Assert.IsType<Ship>(createdResult.Value);
            Assert.Equal("Poseidon", ship.Name);
        }

        [Fact]
        public async Task UpdateShip_ValidUpdate_ReturnsNoContent()
        {
            var context = await GetDbContextWithData();
            var controller = new ShipController(context);

            var existingShip = context.Ships.First();
            existingShip.MaxSpeed = 30f;

            var result = await controller.UpdateShip(existingShip.Id, existingShip);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateShip_IdMismatch_ReturnsBadRequest()
        {
            var context = await GetDbContextWithData();
            var controller = new ShipController(context);

            var existingShip = context.Ships.First();
            existingShip.MaxSpeed = 30f;

            var result = await controller.UpdateShip(0, existingShip);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteShip_RemovesShip()
        {
            var context = await GetDbContextWithData();
            var controller = new ShipController(context);

            var result = await controller.DeleteShip(1);

            Assert.IsType<NoContentResult>(result);
            Assert.Null(await context.Ships.FindAsync(1));
        }

        [Fact]
        public async Task DeleteShip_InvalidId_ReturnsNotFound()
        {
            var context = await GetDbContextWithData();
            var controller = new ShipController(context);

            var result = await controller.DeleteShip(0);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}