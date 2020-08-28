using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleMicroservice.Api.Models;

namespace SampleMicroservice.Controllers
{
    public class SomeController : BaseController
    {
        [Route(SampleMicroservice.Api.Routes.SomeRoute)]
        public async Task<IActionResult> PostSomeRequest()
        {
            return await Post<SomeRequest, SomeResponse>(Request);
        }

        [Route(SampleMicroservice.Api.Routes.DifferentRoute)]
        public async Task<IActionResult> PostDifferentRequest()
        {
            return await Post<DifferentRequest, SomeResponse>(Request);
        }
    }
}
