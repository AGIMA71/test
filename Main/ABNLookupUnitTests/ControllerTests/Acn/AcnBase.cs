using ABNLookup.Hypermedia;
using ABNLookup.Interfaces;
using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using ABNLookup.Domain.Models;

namespace ABNLookupUnitTests.ControllerTests.Acn
{
    public abstract class AcnBase<T> where T:class
    {
        protected IMessageCodeService messageCodeService = Substitute.For<IMessageCodeService>();
        protected ISortMappingService sortValidationService = Substitute.For<ISortMappingService>();
        private Fixture fixture = new Fixture { RepeatCount = 3 };
        private IEnumerable<T> fakeGetAllAcnList;      

        public IAcnService acnService = Substitute.For<IAcnService>();
        public IEnumerable<T> fakeGetAcn;
        public ResourceLink fakeResourceLink;
        public IEnumerable<T> fakeGetAcnListMoreThan10;

        public abstract ResourceLink GetAllAcn();
        public abstract Task<IActionResult> GetAcn(string id);

        public  void VerifyGetAllAcnAsync()
        {
            var actual = GetAllAcn();

            using (new AssertionScope())
            {
                actual.Should().BeEquivalentTo(fakeResourceLink);
            }
        }

        public async Task VerifyGetAcnAsync()
        {
            var actual = await GetAcn("1");

            using (new AssertionScope())
            {
                ((ObjectResult)actual).Value.Should().BeEquivalentTo(fakeGetAcn);
            }
        }

        public async Task VerifyAcnNotFoundAsync()
        {
            var actual = await GetAcn("A");

            using (new AssertionScope())
            {
                ((StatusCodeResult)actual).StatusCode.Should().Be(StatusCodes.Status404NotFound);
            }
        }
        public async Task VerifyAcnUnProcessableEnittyForMoreThan10ResultsAsync()
        {
            var actual = await GetAcn("8");

            using (new AssertionScope())
            {
                ((UnprocessableEntityObjectResult)actual).StatusCode.Should().Be(StatusCodes.Status422UnprocessableEntity);
            }
        }
        protected AcnBase()
        {
            fakeResourceLink = fixture.Create<ResourceLink>();
            fakeGetAllAcnList = fixture.Create<IEnumerable<T>>();
            fakeGetAcnListMoreThan10 = fixture.CreateMany<T>(11);

            var acnList = new List<T>(fakeGetAllAcnList);
            fakeGetAcn = new List<T> { acnList[0] };
          
            messageCodeService.GetMessageByCode(Arg.Any<string>()).Returns(new MessageCode("1003", "dummy description"));
            acnService.GetAcnResourceLinks(Arg.Any<HttpContext>(), Arg.Any<string>(), Arg.Any<string[]>()).Returns(fakeResourceLink);
            acnService.GetAcnAsync<T>(Arg.Any<string>()).Returns(fakeGetAcn);
        }

    }
}
