using MaritimeAPI.Controllers;
using MaritimeAPI.Data;
using MaritimeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaritimeAPI.UnitTests.Controllers
{
    public class VoyageControllerTests
    {
        private async Task<ApplicationDbContext> GetDbContextWithData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "VoyageTestDb")
                .Options;

            var context = new ApplicationDbContext(options);

            var country = new Country { Name = "Romania" };
            var port1 = new Port { Name = "Constanta", Country = country };
            var port2 = new Port { Name = "Barcelona", Country = new Country { Name = "Spain" } };
            var ship = new Ship { Name = "Titanic", MaxSpeed = 22.5f };

            context.Countries.Add(country);
            context.Ports.AddRange(port1, port2);
            context.Ships.Add(ship);

            await context.SaveChangesAsync();

            var voyage = new Voyage
            {
                VoyageDate = DateTime.Today,
                VoyageStart = DateTime.Today.AddHours(8),
                VoyageEnd = DateTime.Today.AddHours(18),
                DeparturePortId = port1.Id,
                ArrivalPortId = port2.Id,
                ShipId = ship.Id
            };

            context.Voyages.Add(voyage);
            await context.SaveChangesAsync();

            return context;
        }

        [Fact]
        public async Task GetVoyages_ReturnsAllVoyages()
        {
            var context = await GetDbContextWithData();
            var controller = new VoyageController(context);

            var result = await controller.GetVoyages();

            var actionResult = Assert.IsType<ActionResult<IEnumerable<Voyage>>>(result);
            var voyages = Assert.IsAssignableFrom<IEnumerable<Voyage>>(actionResult.Value);
            Assert.Single(voyages);
        }

        [Fact]
        public async Task GetVoyage_ReturnsVoyageById()
        {
            var context = await GetDbContextWithData();
            var controller = new VoyageController(context);

            var voyage = context.Voyages.First();

            var result = await controller.GetVoyage(voyage.Id);

            var actionResult = Assert.IsType<ActionResult<Voyage>>(result);
            var returned = Assert.IsType<Voyage>(actionResult.Value);
            Assert.Equal(voyage.Id, returned.Id);
        }

        [Fact]
        public async Task GetVoyage_InvalidId_ReturnsNotFound()
        {
            var context = await GetDbContextWithData();
            var controller = new VoyageController(context);

            var result = await controller.GetVoyage(0);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task AddItem_CreatesVoyage()
        {
            var context = await GetDbContextWithData();
            var controller = new VoyageController(context);

            var voyage = new Voyage
            {
                VoyageDate = DateTime.Today,
                VoyageStart = DateTime.Today.AddHours(9),
                VoyageEnd = DateTime.Today.AddHours(19),
                DeparturePortId = context.Ports.First().Id,
                ArrivalPortId = context.Ports.Last().Id,
                ShipId = context.Ships.First().Id
            };

            var result = await controller.AddItem(voyage);

            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdVoyage = Assert.IsType<Voyage>(created.Value);
            Assert.Equal(voyage.VoyageStart, createdVoyage.VoyageStart);
        }

        [Fact]
        public async Task UpdateVoyage_ValidUpdate_ReturnsNoContent()
        {
            var context = await GetDbContextWithData();
            var controller = new VoyageController(context);

            var voyage = context.Voyages.First();
            voyage.VoyageEnd = voyage.VoyageEnd.AddHours(1);

            var result = await controller.UpdateVoyage(voyage.Id, voyage);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateVoyage_IdMismatch_ReturnsBadRequest()
        {
            var context = await GetDbContextWithData();
            var controller = new VoyageController(context);

            var voyage = context.Voyages.First();

            var result = await controller.UpdateVoyage(999, voyage);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteVoyage_RemovesVoyage()
        {
            var context = await GetDbContextWithData();
            var controller = new VoyageController(context);

            var voyage = context.Voyages.First();

            var result = await controller.DeleteVoyage(voyage.Id);

            Assert.IsType<NoContentResult>(result);
            Assert.Null(await context.Voyages.FindAsync(voyage.Id));
        }

        [Fact]
        public async Task DeleteVoyage_InvalidId_ReturnsNotFound()
        {
            var context = await GetDbContextWithData();
            var controller = new VoyageController(context);

            var result = await controller.DeleteVoyage(999);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}