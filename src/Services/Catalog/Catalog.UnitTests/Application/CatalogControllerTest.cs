using Catalog.API.IntegrationEvents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopOnContainers.Services.Catalog.API;
using Microsoft.eShopOnContainers.Services.Catalog.API.Controllers;
using Microsoft.eShopOnContainers.Services.Catalog.API.Infrastructure;
using Microsoft.eShopOnContainers.Services.Catalog.API.Model;
using Microsoft.eShopOnContainers.Services.Catalog.API.ViewModel;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using System;

namespace UnitTest.Catalog.Application
{
    public class CatalogControllerTest
    {
        private static CatalogContext GetDbContext()
        {
            var dbOptions = new DbContextOptionsBuilder<CatalogContext>()
                .UseInMemoryDatabase(databaseName: "in-memory" + Guid.NewGuid().ToString())
                .Options;
            
            var dbContext = new CatalogContext(dbOptions);
            
            dbContext.AddRange(GetFakeCatalog());
            dbContext.SaveChanges();
            return dbContext;
        }

        [Fact]
        public async Task Get_catalog_items_success()
        {
            //Arrange
            var brandFilterApplied = 1;
            var typesFilterApplied = 2;
            var pageSize = 4;
            var pageIndex = 1;

            var expectedItemsInPage = 2;
            var expectedTotalItems = 6;

            var catalogContext = GetDbContext();
            var catalogSettings = new TestCatalogSettings();

            var integrationServicesMock = new Mock<ICatalogIntegrationEventService>();

            //Act
            var orderController = new CatalogController(catalogContext, catalogSettings, integrationServicesMock.Object);
            var actionResult = await orderController.ItemsByTypeIdAndBrandIdAsync(typesFilterApplied, brandFilterApplied, pageSize, pageIndex);

            //Assert
            Assert.IsType<ActionResult<PaginatedItemsViewModel<CatalogItem>>>(actionResult);
            var page = Assert.IsAssignableFrom<PaginatedItemsViewModel<CatalogItem>>(actionResult.Value);
            Assert.Equal(expectedTotalItems, page.Count);
            Assert.Equal(pageIndex, page.PageIndex);
            Assert.Equal(pageSize, page.PageSize);
            Assert.Equal(expectedItemsInPage, page.Data.Count());
        }

        /*
        [Fact]
        public async Task Get_catalog_items_by_name_success()
        {
            //Arrange
            var pageIndex = 0;

            var expectedItemsInPage = 1;
            var expectedTotalItems = 1;

            var catalogContext = GetDbContext();
            var catalogSettings = new TestCatalogSettings();

            var integrationServicesMock = new Mock<ICatalogIntegrationEventService>();

            //Act
            var orderController = new CatalogController(catalogContext, catalogSettings, integrationServicesMock.Object);
            var actionResult = await orderController.ItemsWithNameAsync("fakeItemA");
            
            //Assert
            Assert.IsType<ActionResult<PaginatedItemsViewModel<CatalogItem>>>(actionResult);
            var page = Assert.IsAssignableFrom<PaginatedItemsViewModel<CatalogItem>>(actionResult.Value);
            Assert.Equal(expectedTotalItems, page.Count);
            Assert.Equal(pageIndex, page.PageIndex);
            Assert.Equal(expectedItemsInPage, page.Data.Count());
        }
        */

        private static List<CatalogItem> GetFakeCatalog()
        {
            return new List<CatalogItem>()
            {
                new CatalogItem()
                {
                    Id = 1,
                    Name = "fakeItemA",
                    CatalogTypeId = 2,
                    CatalogBrandId = 1,
                    PictureFileName = "fakeItemA.png"
                },
                new CatalogItem()
                {
                    Id = 2,
                    Name = "fakeItemB",
                    CatalogTypeId = 2,
                    CatalogBrandId = 1,
                    PictureFileName = "fakeItemB.png"
                },
                new CatalogItem()
                {
                    Id = 3,
                    Name = "fakeItemC",
                    CatalogTypeId = 2,
                    CatalogBrandId = 1,
                    PictureFileName = "fakeItemC.png"
                },
                new CatalogItem()
                {
                    Id = 4,
                    Name = "fakeItemD",
                    CatalogTypeId = 2,
                    CatalogBrandId = 1,
                    PictureFileName = "fakeItemD.png"
                },
                new CatalogItem()
                {
                    Id = 5,
                    Name = "fakeItemE",
                    CatalogTypeId = 2,
                    CatalogBrandId = 1,
                    PictureFileName = "fakeItemE.png"
                },
                new CatalogItem()
                {
                    Id = 6,
                    Name = "fakeItemF",
                    CatalogTypeId = 2,
                    CatalogBrandId = 1,
                    PictureFileName = "fakeItemF.png"
                }
            };
        }
    }

    public class TestCatalogSettings : IOptionsSnapshot<CatalogSettings>
    {
        public CatalogSettings Value => new CatalogSettings
        {
            PicBaseUrl = "http://image-server.com/",
            AzureStorageEnabled = true
        };

        public CatalogSettings Get(string name) => Value;
    }

}
