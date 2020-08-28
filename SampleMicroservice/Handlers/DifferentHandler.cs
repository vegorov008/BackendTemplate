using System.Collections.Generic;
using System.Threading.Tasks;
using BackendTemplate.Abstractions.Handlers;
using SampleMicroservice.Api.Models;

namespace SampleMicroservice.Handlers
{
    public class DifferentHandler : BaseAsyncRequestHandler<DifferentRequest, SomeResponse>
    {
        protected override Task<List<SomeResponse>> Process(IList<DifferentRequest> requestsList)
        {
            // do business logic

            throw new System.NotImplementedException();
        }
    }
}