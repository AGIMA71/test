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

namespace ABNLookupUnitTests.ControllerTests.OrgName
{
    public abstract class OrgNameBase<T> where T : class
    {
        protected IMessageCodeService messageCodeService = Substitute.For<IMessageCodeService>();
        protected ISortMappingService sortMappingService = Substitute.For<ISortMappingService>();
        private Fixture fixture = new Fixture { RepeatCount = 3 };
        private IEnumerable<T> fakeGetAllOrgNamesList;        

        public IOrgNameService OrgNameService = Substitute.For<IOrgNameService>();
        public IEnumerable<T> fakeGetOrgName;
        public ResourceLink fakeResourceLink;
        public IEnumerable<T> fakeGetOrgNamesListMoreThan10;

        public abstract ResourceLink GetAllOrgNames();
        public abstract Task<IActionResult> GetOrgName(string name);

        public void VerifyGetAllOrgNamesAsync()
        {
            var actual = GetAllOrgNames();

            using (new AssertionScope())
            {
                actual.Should().BeEquivalentTo(fakeResourceLink);
            }
        }
        public async Task VerifyGetOrgNameAsync()
        {
            var actual = await GetOrgName("ARMOUR");

            using (new AssertionScope())
            {
                ((ObjectResult)actual).Value.Should().BeEquivalentTo(fakeGetOrgName);
                ((ObjectResult)actual).StatusCode.Should().Be(StatusCodes.Status200OK);
            }
        }
        public async Task VerifyOrgNameNotFoundAsync()
        {
            var actual = await GetOrgName("ABCD");

            using (new AssertionScope())
            {
                ((StatusCodeResult)actual).StatusCode.Should().Be(StatusCodes.Status404NotFound);
            }
        }

        public async Task VerifyUnprocessableEntityAsync()
        {
            var actual = await GetOrgName("AR");

            using (new AssertionScope())
            {
                ((ObjectResult)actual).StatusCode.Should().Be(StatusCodes.Status422UnprocessableEntity);
            }
        }
        public async Task VerifyOrgNameUnProcessableEnittyForMoreThan10ResultsAsync()
        {
            var actual = await GetOrgName("PTY LTD");

            using (new AssertionScope())
            {
                ((UnprocessableEntityObjectResult)actual).StatusCode.Should().Be(StatusCodes.Status422UnprocessableEntity);
            }
        }
        protected OrgNameBase()
        {
            fakeResourceLink = fixture.Create<ResourceLink>();
            fakeGetAllOrgNamesList = fixture.Create<IEnumerable<T>>();
            fakeGetOrgNamesListMoreThan10 = fixture.CreateMany<T>(11);

            var OrgNamesList = new List<T>(fakeGetAllOrgNamesList);
            fakeGetOrgName = new List<T> { OrgNamesList[0] };
           
            messageCodeService.GetMessageByCode(Arg.Any<string>()).Returns(new MessageCode ("1003", "dummy description"));
            OrgNameService.GetOrgResourceLinks(Arg.Any<HttpContext>(), Arg.Any<string>(), Arg.Any<string[]>()).Returns(fakeResourceLink);
            OrgNameService.GetOrgAsync<T>(Arg.Any<string>()).Returns(fakeGetOrgName);
        }
    }
}
