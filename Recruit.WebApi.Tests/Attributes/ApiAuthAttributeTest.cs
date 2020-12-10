using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Recruit.WebApi.Attributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recruit.WebApi.Tests.Attributes
{
    [TestClass]
    public class ApiAuthAttributeTest
    {
        private IServiceCollection servicesCollection;
        private IServiceProvider serviceProvider;
        private Mock<IConfiguration> configurationMock;

        delegate void MockTryParseCallback(ActionExecutingContext context, out StringValues output);

        [TestInitialize]
        public void TestInit()
        {
            this.servicesCollection = new ServiceCollection();

            configurationMock = new Mock<IConfiguration>();
            configurationMock.SetupGet(x => x[It.IsAny<string>()]).Returns("Test_Key");

            this.servicesCollection.AddSingleton<IConfiguration>(configurationMock.Object);

            this.serviceProvider = this.servicesCollection.BuildServiceProvider();
        }

        [TestMethod]
        public async Task OnActionExecutionAsync_Success()
        {
            StringValues apiHeaderValue = new StringValues("Test_Key");

            var attribute = new Mock<ApiAuthAttribute>();
            attribute.Setup(x => x.TryGetApiKeyValueFromHeader(It.IsAny<ActionExecutingContext>(), out apiHeaderValue))
                .Callback(new MockTryParseCallback((ActionExecutingContext c, out StringValues output) => output = apiHeaderValue))
                .Returns(true);

            var httpContextMock = Mock.Of<HttpContext>();
            httpContextMock.RequestServices = serviceProvider;

            var actionContext = new ActionContext(
                httpContextMock,
                Mock.Of<RouteData>(),
                Mock.Of<ActionDescriptor>(),
                Mock.Of<ModelStateDictionary>()
            );

            var actionExecutingContext = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                Mock.Of<Controller>()
            );

            var actionExecutionDelegateMock = new Mock<ActionExecutionDelegate>();

            await attribute.Object.OnActionExecutionAsync(actionExecutingContext, actionExecutionDelegateMock.Object);

            actionExecutionDelegateMock.Verify(x => x.Invoke(), Times.Once);
        }
    }
}
