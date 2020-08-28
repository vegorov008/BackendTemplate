using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleMicroservice.Api.Models;

namespace SampleMicroservice.Controllers
{
    public class AnotherController : BaseController
    {
        [Route(SampleMicroservice.Api.Routes.AnotherRoute)]
        public async Task<IActionResult> PostAnotherRequest()
        {
            return await Post<AnotherRequest>(Request);
        }
    }
}
