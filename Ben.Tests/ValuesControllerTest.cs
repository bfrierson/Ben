using System.Linq;
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

            Assert.AreEqual(1, ((CreatedNegotiatedContentResult<int>)result).Content);
            Assert.AreEqual("Change Me", result);
            //Assert.IsInstanceOfType(result, typeof(???));
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
            Assert.AreEqual("System.Web.Http.Results.NotFoundResult", result.ToString());
            // todo: what should the result be if id 5 does not exist? 
            // Assert.IsInstanceOfType(result, typeof(???));
            Assert.Fail("fill in the blanks buddy");
        }

        [Test]
        public void Delete()
        {
            // Arrange
            ValuesController controller = new ValuesController();
            controller.Post("DeleteMe");

            // Act
            controller.Delete(1);
            var result = controller.Get(1);

            // Assert
            Assert.AreEqual("System.Web.Http.Results.NotFoundResult", result.ToString());
            // todo: how can you double check the item was deleted?
        }
    }
}
