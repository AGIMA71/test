using ABNLookup.Controllers;
using ABNLookup.Dtos;
using ABNLookup.Hypermedia;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ABNLookupUnitTests.ControllerTests.Acn
{
    public class AcnV2 : AcnBase<AcnV2DTO>
    {
        private readonly AcnV2Controller systemUnderTest;
        public AcnV2() =>
            systemUnderTest = new AcnV2Controller(acnService, messageCodeService, sortValidationService);       
        public async override Task<IActionResult> GetAcn(string id)=> await systemUnderTest.GetAcnV2(id);       
        public override ResourceLink GetAllAcn()=> systemUnderTest.GetAllAcnV2();

    }
}
