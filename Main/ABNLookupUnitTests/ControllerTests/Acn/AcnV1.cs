using ABNLookup.Controllers;
using ABNLookup.Dtos;
using ABNLookup.Hypermedia;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ABNLookupUnitTests.ControllerTests.Acn
{
    public class AcnV1 : AcnBase<AcnV1DTO>
    {
        private readonly AcnController systemUnderTest;
        public AcnV1()=>
            systemUnderTest = new AcnController(acnService, messageCodeService, sortValidationService);

        public async override Task<IActionResult> GetAcn(string id)=> await systemUnderTest.GetAcnV1(id);        
        public override ResourceLink GetAllAcn() => systemUnderTest.GetAllAcnV1();

    }
}
