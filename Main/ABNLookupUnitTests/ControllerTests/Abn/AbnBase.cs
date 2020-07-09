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
using ABNLookup.Infrastructure.Logging;
using ABNLookup.Dtos;

namespace ABNLookupUnitTests.ControllerTests.Abn
{
    public abstract class AbnBase<T> where T : class
    {
        protected IMessageCodeService messageCodeService = Substitute.For<IMessageCodeService>();
        protected ISortMappingService sortValidationService = Substitute.For<ISortMappingService>();
        protected ILoggerManager loggerManager = Substitute.For<ILoggerManager>();
        private Fixture fixture = new Fixture { RepeatCount = 3 };
        private IEnumerable<T> fakeGetAllAbnList;              

        public IAbnService AbnService = Substitute.For<IAbnService>();
        public IEnumerable<T> fakeGetAbn;
        public ResourceLink fakeResourceLink;
        public IEnumerable<T> fakeGetAbnListMoreThan10;
        public AbnNewDTO fakeAbnNewDto;
        public AbnRegisterDTO fakeAbnRegisterDTO;

        public abstract ResourceLink GetAllAbn();
        public abstract Task<IActionResult> GetAbn(string id);

        public void VerifyGetAllAbnAsync()
        {
            var actual = GetAllAbn();

            using (new AssertionScope())
            {
                actual.Should().BeEquivalentTo(fakeResourceLink);
            }
        }

        public async Task VerifyGetAbnAsync()
        {
            var actual = await GetAbn("75089205845");

            using (new AssertionScope())
            {
                ((ObjectResult)actual).Value.Should().BeEquivalentTo(fakeGetAbn);
            }
        }

        public async Task VerifyAbnNotFoundAsync()
        {
            var actual = await GetAbn("AAA");

            using (new AssertionScope())
            {
                ((StatusCodeResult)actual).StatusCode.Should().Be(StatusCodes.Status404NotFound);
            }
        }
        public async Task VerifyAbnUnProcessableEnittyForMoreThan10ResultsAsync()
        {
            var actual = await GetAbn("1");

            using (new AssertionScope())
            {
                ((UnprocessableEntityObjectResult)actual).StatusCode.Should().Be(StatusCodes.Status422UnprocessableEntity);
            }
        }
       
        protected AbnBase()
        {
            fakeResourceLink = fixture.Create<ResourceLink>();
            fakeAbnNewDto = fixture.Create<AbnNewDTO>();
            fakeAbnRegisterDTO = fixture.Create<AbnRegisterDTO>();
            fakeGetAllAbnList = fixture.Create<IEnumerable<T>>();
            fakeGetAbnListMoreThan10 = fixture.CreateMany<T>(11);

            var AbnList = new List<T>(fakeGetAllAbnList);
            fakeGetAbn = new List<T> { AbnList[0] };
           
            messageCodeService.GetMessageByCode(Arg.Any<string>()).Returns(new MessageCode ("1003", "dummy description"));
            AbnService.GetAbnResourceLinks(Arg.Any<HttpContext>(), Arg.Any<string>(), Arg.Any<string[]>()).Returns(fakeResourceLink);
            AbnService.GetAbnAsync<T>(Arg.Any<string>()).Returns(fakeGetAbn);
            AbnService.BusniessNameAlreadyRegistered(Arg.Any<string>()).Returns(false);
            AbnService.Register(Arg.Any<AbnRegisterDTO>()).Returns(fakeAbnNewDto);
            
        }

    }
}
