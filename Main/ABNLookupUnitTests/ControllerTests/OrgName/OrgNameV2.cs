using ABNLookup.Controllers;
using ABNLookup.Dtos;
using ABNLookup.Hypermedia;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ABNLookupUnitTests.ControllerTests.OrgName
{
    public class OrgNameV2 : OrgNameBase<AbnV2DTO>
    {
        private readonly OrgNameV2Controller systemUnderTest;
        public OrgNameV2() =>
             systemUnderTest = new OrgNameV2Controller(OrgNameService, messageCodeService, sortMappingService);
        
        public async override Task<IActionResult> GetOrgName(string name) => await systemUnderTest.GetOrgNameV2(name);
        public override ResourceLink GetAllOrgNames() => systemUnderTest.GetAllAbnV2();
    }
}
