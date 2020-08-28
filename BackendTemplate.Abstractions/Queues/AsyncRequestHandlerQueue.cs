using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackendTemplate.Utilities;
using BaseBackend.Application.Handlers;

namespace BaseBackend.Application.Queues
{
    public class AsyncRequestHandlerQueue<TRequest, THandler> : IAsyncRequestHandlerQueue<TRequest, THandler> 
        where TRequest : class 
        where THandler : class, IAsyncRequestHandler<TRequest>, IDisposable
    {
        public void Execute(TRequest request)
        {
            // add request to queue and call Process with all queued requests asynchronously  

            throw new NotImplementedException();
        }

        protected async Task Process(IList<TRequest> requests)
        {
            try
            {
                using (var handler = Ioc.GetInstance<THandler>())
                {
                    await handler.Execute(requests);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }
    }

    public class AsyncRequestHandlerQueue<TRequest, TResponse, THandler> : IAsyncRequestHandlerQueue<TRequest, TResponse, THandler> 
        where TRequest : class 
        where TResponse : class 
        where THandler : class, IAsyncRequestHandler<TRequest, TResponse>, IDisposable
    {
        public Task<TResponse> ExecuteAsync(TRequest request)
        {
            // add request to queue and call Process with all queued requests asynchronously

            throw new NotImplementedException();

            // After completion of Process method return mapped TResponse to controller
        }

        protected async Task<List<TResponse>> Process(IList<TRequest> requests)
        {
            List<TResponse> responses = null;
            try
            {
                using (var handler = Ioc.GetInstance<THandler>())
                {
                    responses = await handler.Execute(requests);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
            return responses;
        }
    }
}
