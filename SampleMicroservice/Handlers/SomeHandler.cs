using System.Linq;
using System.Threading.Tasks;
using BackendTemplate.Abstractions.Handlers;
using Microsoft.EntityFrameworkCore;
using SampleMicroservice.Api.Models;
using SampleMicroservice.Contexts;

namespace SampleMicroservice.Handlers
{
    public class SomeHandler : BaseDbAsyncEnumerateRequestHandler<SomeRequest, SomeResponse, SomeContext>
    {
        protected override async Task<SomeResponse> Process(SomeRequest request)
        {
            // do business logic

            // context sample code
            var entity = Context.SomeEntities.FirstOrDefaultAsync();
            await Context.SaveChangesAsync();

            throw new System.NotImplementedException();
        }
    }
}
