﻿using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Results;
using Ben.Controllers;
using NUnit.Framework;

namespace Ben.Tests
{
    [TestFixture]
    public class ValuesControllerTest
    {
        [Test]
        public void Get()
        {
            // Arrange
            var controller = new ValuesController();
            controller.Post("BenTest");
            controller.Post("ChrisIsThaManNowDoge");

            // Act
            var result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("{ id = 1, value = BenTest }", result.ElementAt(0).ToString());
            Assert.AreEqual("{ id = 2, value = ChrisIsThaManNowDoge }", result.ElementAt(1).ToString());
        }

        [Test]
        public void GetById()
        {
            // Arrange
            var controller = new ValuesController();
            controller.Post("Ben has done it");

            // Act
            var result = controller.Get(1);

            // Assert
            Assert.AreEqual("Ben has done it", ((OkNegotiatedContentResult<string>)result).Content);
        }

        [Test]
        public void Post()
        {
            // Arrange
            ValuesController controller = new ValuesController();

            // Act
            var result = controller.Post("value");

            // Assert
            Assert.IsInstanceOf<CreatedNegotiatedContentResult<int>>(result);
            Assert.AreEqual("api/values/1", ((CreatedNegotiatedContentResult<int>)result).Location.ToString());
            Assert.AreEqual(1, ((CreatedNegotiatedContentResult<int>)result).Content);
        }

        [Test]
        public void Put()
        {
            // Arrange
            ValuesController controller = new ValuesController();
            controller.Post("value1");

            // Act
            var result = controller.Put(1, "Change Me");

            //Assert
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public void Put_DoesNotExist()
        {
            // Arrange
            ValuesController controller = new ValuesController();
            controller.Post("PutNotHere");

            // Act
            var result = controller.Put(5, "Changed");


            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void Delete()
        {
            // Arrange
            ValuesController controller = new ValuesController();
            controller.Post("DeleteMe");

            // Act
            var result = controller.Delete(1);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);

            var doubleCheck = controller.Get(1);

            Assert.IsInstanceOf<NotFoundResult>(doubleCheck);
            // todo: how can you double check the item was deleted?
        }
    }
}
