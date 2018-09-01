using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SportsStore.Tests
{
    public class AdminControllerTests
    {
        private T GetViewModel<T>(IActionResult result)
            where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }


        [Fact]
        public void IndexContainsAllProducts()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product{ ProductID = 1, Name = "P1"},
                new Product{ ProductID = 2, Name = "P2"},
                new Product{ ProductID = 3, Name = "P3"},
                new Product{ ProductID = 4, Name = "P4"}
            }).AsQueryable());

            AdminController target = new AdminController(mock.Object);

            Product[] result = GetViewModel<IEnumerable<Product>>(target.Index())?.ToArray();
        }

        [Fact]
        public void CanEditProduct()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product{ProductID = 1, Name = "P1"},
                new Product{ProductID = 2, Name = "P2"},
                new Product{ProductID = 3, Name = "P3"},
                new Product{ProductID = 4, Name = "P4"}
            }).AsQueryable());

            AdminController target = new AdminController(mock.Object);

            Product p3 = GetViewModel<Product>(target.Edit(3));
            Product p1 = GetViewModel<Product>(target.Edit(1));
            Product p2 = GetViewModel<Product>(target.Edit(2));

            Assert.Equal(1, p1.ProductID);
            Assert.Equal(2, p2.ProductID);
            Assert.Equal(3, p3.ProductID);
        }

        [Fact]
        public void CanNotEditNonexistentProduct()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
                {
                    new Product {ProductID = 1, Name = "P1"},
                    new Product {ProductID = 2, Name = "P2"},
                    new Product {ProductID = 3, Name = "P3"}
                }).AsQueryable());

            AdminController target = new AdminController(mock.Object);

            Product result = GetViewModel<Product>(target.Edit(4));

            Assert.Null(result);
        }

        [Fact]
        public void CanSaveValidChanges()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            Mock<ITempDataDictionary> tempData = new Mock<ITempDataDictionary>();

            AdminController target = new AdminController(mock.Object)
            {
                TempData = tempData.Object
            };

            Product product = new Product { Name = "Test" };

            IActionResult result = target.Edit(product);

            mock.Verify(m => m.SaveProduct(product));

            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", (result as RedirectToActionResult).ActionName);
        }

        [Fact]
        public void CanNotSaveInvalidProduct()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            AdminController target = new AdminController(mock.Object);

            Product product = new Product { Name = "Test" };

            target.ModelState.AddModelError("eror", "error");

            IActionResult result = target.Edit(product);

            mock.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void CanDeleteValidProducts()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            Product product = new Product { ProductID = 2, Name = "P2" };

            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                product,
                new Product {ProductID = 4, Name = "P4"}
            }).AsQueryable());

            AdminController target = new AdminController(mock.Object);

            target.Delete(product.ProductID);

            mock.Verify(m => m.DeleteProduct(product.ProductID));
        }
    }
}
