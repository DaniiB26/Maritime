using MaritimeAPI.Controllers;
using MaritimeAPI.Data;
using MaritimeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaritimeAPI.UnitTests.Controllers
{
    public class CountryControllerTests
    {
        private async Task<ApplicationDbContext> GetDbContextWithData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);

            context.Countries.Add(new Country { Name = "Romania" });
            await context.SaveChangesAsync();

            return context;
        }

        [Fact]
        public async Task GetCountries_ReturnsAll()
        {
            var context = await GetDbContextWithData();
            var controller = new CountryController(context);

            var result = await controller.GetCountries();

            var actionResult = Assert.IsType<ActionResult<IEnumerable<Country>>>(result);
            var countries = Assert.IsAssignableFrom<IEnumerable<Country>>(actionResult.Value);
            Assert.Single(countries);
        }

        [Fact]
        public async Task GetCountry_ReturnsCountryById()
        {
            var context = await GetDbContextWithData();
            var controller = new CountryController(context);
            var country = context.Countries.First();

            var result = await controller.GetCountry(country.Id);

            var actionResult = Assert.IsType<ActionResult<Country>>(result);
            var returned = Assert.IsType<Country>(actionResult.Value);
            Assert.Equal(country.Name, returned.Name);
        }

        [Fact]
        public async Task GetCountry_InvalidId_ReturnsNotFound()
        {
            var context = await GetDbContextWithData();
            var controller = new CountryController(context);

            var result = await controller.GetCountry(999);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task AddItem_CreatesCountry()
        {
            var context = await GetDbContextWithData();
            var controller = new CountryController(context);

            var country = new Country { Name = "Spain" };
            var result = await controller.AddItem(country);

            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdCountry = Assert.IsType<Country>(created.Value);
            Assert.Equal("Spain", createdCountry.Name);
        }

        [Fact]
        public async Task UpdateCountry_ValidUpdate_ReturnsNoContent()
        {
            var context = await GetDbContextWithData();
            var controller = new CountryController(context);
            var country = context.Countries.First();
            country.Name = "Updated Romania";

            var result = await controller.UpdateCountry(country.Id, country);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateCountry_IdMismatch_ReturnsBadRequest()
        {
            var context = await GetDbContextWithData();
            var controller = new CountryController(context);
            var country = context.Countries.First();

            var result = await controller.UpdateCountry(999, country);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteCountry_DeletesSuccessfully()
        {
            var context = await GetDbContextWithData();
            var controller = new CountryController(context);
            var country = context.Countries.First();

            var result = await controller.DeleteCountry(country.Id);

            Assert.IsType<NoContentResult>(result);
            Assert.Null(await context.Countries.FindAsync(country.Id));
        }

        [Fact]
        public async Task DeleteCountry_InvalidId_ReturnsNotFound()
        {
            var context = await GetDbContextWithData();
            var controller = new CountryController(context);

            var result = await controller.DeleteCountry(999);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}