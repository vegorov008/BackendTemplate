using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaseBackend.Application.Handlers
{
    public interface IAsyncRequestHandler<TRequest> : IDisposable where TRequest : class
    {
        Task Execute(IList<TRequest> requestsList);
    }

    public interface IAsyncRequestHandler<TRequest, TResponse> : IDisposable where TRequest : class where TResponse : class
    {
        Task<List<TResponse>> Execute(IList<TRequest> requestsList);
    }
}
