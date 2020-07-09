using ABNLookup.Controllers;
using ABNLookup.Dtos;
using ABNLookup.Hypermedia;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ABNLookupUnitTests.ControllerTests.Abn
{
    public class AbnV1 : AbnBase<AbnV1DTO>
    {
        private readonly AbnController systemUnderTest;
        public AbnV1()=>
             systemUnderTest = new AbnController(AbnService, messageCodeService, sortValidationService, loggerManager);       
        public async override Task<IActionResult> GetAbn(string id) => await systemUnderTest.GetAbnV1(id);
        public override ResourceLink GetAllAbn() => systemUnderTest.GetAllAbnV1();

    }
}
