using System.Collections.Generic;
using System.Threading.Tasks;
using BackendTemplate.Abstractions.Handlers;
using SampleMicroservice.Api.Models;

namespace SampleMicroservice.Handlers
{
    public class AnotherHandler : BaseAsyncRequestHandler<AnotherRequest>
    {
        protected override Task Process(IList<AnotherRequest> requestsList)
        {
            // do business logic

            throw new System.NotImplementedException();
        }
    }
}
