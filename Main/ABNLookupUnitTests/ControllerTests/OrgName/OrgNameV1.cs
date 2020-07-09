using ABNLookup.Controllers;
using ABNLookup.Dtos;
using ABNLookup.Hypermedia;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ABNLookupUnitTests.ControllerTests.OrgName
{
    public class OrgNameV1 :OrgNameBase<AbnV1DTO>
    {
        private readonly OrgNameController systemUnderTest;
        public OrgNameV1()=>
            systemUnderTest = new OrgNameController(OrgNameService, messageCodeService, sortMappingService);
        
        public async override Task<IActionResult> GetOrgName(string name) => await systemUnderTest.GetOrgName(name);
        public override ResourceLink GetAllOrgNames() => systemUnderTest.GetAllAbn();
    }
}
