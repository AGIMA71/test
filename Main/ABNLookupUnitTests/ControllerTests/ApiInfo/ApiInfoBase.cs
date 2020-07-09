using ABNLookup.Controllers;
using ABNLookup.Interfaces;
using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;

namespace ABNLookupUnitTests.ControllerTests.ApiInfo
{
    internal abstract class ApiInfoBase
    {
        private Fixture fixture = new Fixture { RepeatCount = 3 };
        protected ApiInfoController systemUnderTest;
        private IApiInfoService ApiInfoService = Substitute.For<IApiInfoService>();
        private ABNLookup.Domain.Models.ApiInfo fakeApiInfo;

        public abstract ABNLookup.Domain.Models.ApiInfo GetApiInfo();

        public void VerifyGetApiInfo()
        {
            var actual = GetApiInfo();

            using (new AssertionScope())
            {
                actual.Should().BeEquivalentTo(fakeApiInfo);
            }
        }

        public void VerifyApiVersion()
        {
            var actual = GetApiInfo();

            using (new AssertionScope())
            {
                actual.Version.Should().NotBeNullOrWhiteSpace();
            }
        }
        public void VerifyApiStatus()
        {
            var actual = GetApiInfo();

            using (new AssertionScope())
            {
                actual.Status.Should().NotBeNullOrWhiteSpace();
            }
        }

        protected ApiInfoBase()
        {
            fakeApiInfo = fixture.Create<ABNLookup.Domain.Models.ApiInfo>();
            systemUnderTest = new ApiInfoController(ApiInfoService);
            ApiInfoService.GetApiInfo().Returns(fakeApiInfo);
            ApiInfoService.GetApiV2Info().Returns(fakeApiInfo);
        }
    }
}
