using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BaseBackend.Application.Handlers;

namespace BaseBackend.Application.Queues
{
    public interface IAsyncRequestHandlerQueue<TRequest, THandler> 
        where TRequest : class 
        where THandler : IAsyncRequestHandler<TRequest>, IDisposable
    {
        /// <summary>
        /// This is void because we don't waiting for response in this case and will go back to controller, but note that handler has Task Execute(IList<TRequest> requestsList) method yet
        /// </summary>
        /// <param name="request"></param>
        void Execute(TRequest request);
    }

    public interface IAsyncRequestHandlerQueue<TRequest, TResponse, THandler> 
        where TRequest : class 
        where TResponse : class 
        where THandler : IAsyncRequestHandler<TRequest, TResponse>, IDisposable
    {
        Task<TResponse> ExecuteAsync(TRequest request);
    }
}
