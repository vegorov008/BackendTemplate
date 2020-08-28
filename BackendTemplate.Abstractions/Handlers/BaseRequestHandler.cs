using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackendTemplate.Utilities;
using BaseBackend.Application.Handlers;

namespace BackendTemplate.Abstractions.Handlers
{
    public abstract class BaseRequestHandler : IDisposable
    {
        protected void EnumerateRequests<TRequest>(IList<TRequest> requestsList, Action<TRequest> doPerRequest) where TRequest : class
        {
            if (requestsList?.Count > 0)
            {
                for (int i = 0; i < requestsList.Count; i++)
                {
                    try
                    {
                        doPerRequest.Invoke(requestsList[i]);
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler.HandleException(ex);
                    }
                }
            }
        }

        protected List<TResponse> EnumerateRequests<TRequest, TResponse>(IList<TRequest> requestsList, Func<TRequest, TResponse> doPerRequest) where TRequest : class where TResponse : class
        {
            List<TResponse> responsesList = new List<TResponse>();

            if (requestsList?.Count > 0)
            {
                for (int i = 0; i < requestsList.Count; i++)
                {
                    try
                    {
                        var response = doPerRequest.Invoke(requestsList[i]);
                        if (response != null)
                        {
                            responsesList.Add(response);
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler.HandleException(ex);
                    }
                }
            }

            return responsesList;
        }

        protected async Task EnumerateRequestsAsync<TRequest>(IList<TRequest> requestsList, Func<TRequest, Task> doPerRequest) where TRequest : class
        {
            if (requestsList?.Count > 0)
            {
                for (int i = 0; i < requestsList.Count; i++)
                {
                    try
                    {
                        try
                        {
                            await doPerRequest.Invoke(requestsList[i]);
                        }
                        catch (Exception ex)
                        {
                            ExceptionHandler.HandleException(ex);
                        }
                    }
                    catch
                    {

                    }
                }
            }
        }

        protected async Task<List<TResponse>> EnumerateRequestsAsync<TRequest, TResponse>(IList<TRequest> requestsList, Func<TRequest, Task<TResponse>> doPerRequest) where TRequest : class where TResponse : class
        {
            List<TResponse> responsesList = new List<TResponse>();

            if (requestsList?.Count > 0)
            {
                for (int i = 0; i < requestsList.Count; i++)
                {
                    try
                    {
                        var response = await doPerRequest.Invoke(requestsList[i]);
                        if (response != null)
                        {
                            responsesList.Add(response);
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler.HandleException(ex);
                    }
                }
            }

            return responsesList;
        }

        public virtual void Dispose()
        {

        }
    }


    public abstract class BaseRequestHandler<TRequest> : BaseRequestHandler, IAsyncRequestHandler<TRequest>, IDisposable
        where TRequest : class
    {
        public async Task Execute(IList<TRequest> requestsList)
        {
            try
            {
                Process(requestsList);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }

        protected abstract void Process(IList<TRequest> requestsList);
    }

    public abstract class BaseEnumerateRequestHandler<TRequest> : BaseRequestHandler<TRequest>, IAsyncRequestHandler<TRequest>, IDisposable
        where TRequest : class
    {
        protected override void Process(IList<TRequest> requestsList)
        {
            EnumerateRequests(requestsList, Process);
        }

        protected abstract void Process(TRequest request);
    }

    public abstract class BaseRequestHandler<TRequest, TResponse> : BaseRequestHandler, IAsyncRequestHandler<TRequest, TResponse>, IDisposable
        where TRequest : class where TResponse : class
    {
        public async Task<List<TResponse>> Execute(IList<TRequest> requestsList)
        {
            List<TResponse> responses = null;

            try
            {
                responses = Process(requestsList);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }

            return responses;
        }

        protected abstract List<TResponse> Process(IList<TRequest> requestsList);
    }

    public abstract class BaseEnumerateRequestHandler<TRequest, TResponse> : BaseRequestHandler<TRequest, TResponse>, IAsyncRequestHandler<TRequest, TResponse>, IDisposable
        where TRequest : class where TResponse : class
    {
        protected override List<TResponse> Process(IList<TRequest> requestsList)
        {
            return EnumerateRequests(requestsList, Process);
        }

        protected abstract TResponse Process(TRequest request);
    }



    public abstract class BaseAsyncRequestHandler<TRequest> : BaseRequestHandler, IAsyncRequestHandler<TRequest>, IDisposable
        where TRequest : class
    {
        public async Task Execute(IList<TRequest> requestsList)
        {
            try
            {
                await Process(requestsList);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }

        protected abstract Task Process(IList<TRequest> requestsList);
    }

    public abstract class BaseAsyncEnumerateRequestHandler<TRequest> : BaseAsyncRequestHandler<TRequest>, IAsyncRequestHandler<TRequest>, IDisposable
        where TRequest : class
    {
        protected override async Task Process(IList<TRequest> requestsList)
        {
            await EnumerateRequestsAsync(requestsList, Process);
        }

        protected abstract Task Process(TRequest request);
    }


    public abstract class BaseAsyncRequestHandler<TRequest, TResponse> : BaseRequestHandler, IAsyncRequestHandler<TRequest, TResponse>, IDisposable
        where TRequest : class where TResponse : class
    {
        public async Task<List<TResponse>> Execute(IList<TRequest> requestsList)
        {
            List<TResponse> responses = null;

            try
            {
                responses = await Process(requestsList);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }

            return responses;
        }

        protected abstract Task<List<TResponse>> Process(IList<TRequest> requestsList);
    }

    public abstract class BaseAsyncEnumerateRequestHandler<TRequest, TResponse> : BaseAsyncRequestHandler<TRequest, TResponse>, IAsyncRequestHandler<TRequest, TResponse>, IDisposable
        where TRequest : class where TResponse : class
    {
        protected override async Task<List<TResponse>> Process(IList<TRequest> requestsList)
        {
            return await EnumerateRequestsAsync(requestsList, Process);
        }

        protected abstract Task<TResponse> Process(TRequest request);
    }




    public abstract class BaseDbAsyncRequestHandler<TRequest, TContext> : BaseAsyncRequestHandler<TRequest>, IAsyncRequestHandler<TRequest>, IDisposable
        where TRequest : class where TContext : class, IDisposable
    {
        protected TContext Context { get; set; } = Ioc.Instantiate<TContext>();

        public override void Dispose()
        {
            Context.Dispose();
            base.Dispose();
        }
    }

    public abstract class BaseDbAsyncEnumerateRequestHandler<TRequest, TContext> : BaseAsyncEnumerateRequestHandler<TRequest>, IAsyncRequestHandler<TRequest>, IDisposable
        where TRequest : class where TContext : class, IDisposable
    {
        protected TContext Context { get; set; } = Ioc.Instantiate<TContext>();

        public override void Dispose()
        {
            Context.Dispose();
            base.Dispose();
        }
    }


    public abstract class BaseDbAsyncRequestHandler<TRequest, TResponse, TContext> : BaseAsyncRequestHandler<TRequest, TResponse>, IAsyncRequestHandler<TRequest, TResponse>, IDisposable
        where TRequest : class where TResponse : class where TContext : class, IDisposable
    {
        protected TContext Context { get; set; } = Ioc.Instantiate<TContext>();

        public override void Dispose()
        {
            Context.Dispose();
            base.Dispose();
        }
    }

    public abstract class BaseDbAsyncEnumerateRequestHandler<TRequest, TResponse, TContext> : BaseAsyncEnumerateRequestHandler<TRequest, TResponse>, IAsyncRequestHandler<TRequest, TResponse>, IDisposable
        where TRequest : class where TResponse : class where TContext : class, IDisposable
    {
        protected TContext Context { get; set; } = Ioc.Instantiate<TContext>();

        public override void Dispose()
        {
            Context.Dispose();
            base.Dispose();
        }
    }

}
