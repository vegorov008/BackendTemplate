using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BackendTemplate.Utilities;
using BaseBackend.Application.Handlers;
using BaseBackend.Application.Queues;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SampleMicroservice.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected virtual async Task<IActionResult> Post<TRequest>(HttpRequest httpRequest)
            where TRequest : class
        {
            return await PostExplicit<TRequest, IAsyncRequestHandler<TRequest>>(httpRequest);
        }

        protected virtual async Task<IActionResult> PostExplicit<TRequest, THandler>(HttpRequest httpRequest)
            where TRequest : class
            where THandler : class, IAsyncRequestHandler<TRequest>, IDisposable
        {
            IActionResult result = null;
            try
            {
                if (ModelState.IsValid)
                {
                    var request = await GetContent<TRequest>(httpRequest);
                    if (request != null)
                    {
                        Ioc.GetInstance<IAsyncRequestHandlerQueue<TRequest, THandler>>().Execute(request);
                        result = Ok();
                    }
                    else
                    {
                        result = BadRequest();
                    }
                }
                else
                {
                    result = BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
            return result;
        }

        protected virtual async Task<IActionResult> Post<TRequest, TResponse>(HttpRequest httpRequest)
            where TRequest : class
            where TResponse : class
        {
            return await PostExplicit<TRequest, TResponse, IAsyncRequestHandler<TRequest, TResponse>>(httpRequest);
        }

        protected virtual async Task<IActionResult> PostExplicit<TRequest, TResponse, THandler>(HttpRequest httpRequest)
            where TRequest : class
            where TResponse : class
            where THandler : class, IAsyncRequestHandler<TRequest, TResponse>, IDisposable
        {
            IActionResult result = null;
            try
            {
                if (ModelState.IsValid)
                {
                    var request = await GetContent<TRequest>(httpRequest);
                    if (request != null)
                    {
                        var response = await Ioc.GetInstance<IAsyncRequestHandlerQueue<TRequest, TResponse, THandler>>().ExecuteAsync(request);
                        result = CreateResponse(response, HttpStatusCode.OK);
                    }
                    else
                    {
                        result = BadRequest();
                    }
                }
                else
                {
                    result = BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
            return result;
        }

        protected virtual async Task<T> GetContent<T>(HttpRequest message)
        {
            byte[] contentBytes = null;

            using (var ms = new MemoryStream())
            {
                await message.Body.CopyToAsync(ms);
                contentBytes = ms.ToArray();
            }

            string contentString = Encoding.UTF8.GetString(contentBytes);
            return JsonConvert.DeserializeObject<T>(contentString);
        }

        protected virtual IActionResult CreateResponse(object content, HttpStatusCode statusCode)
        {
            IActionResult result = File(JsonConvert.SerializeObject(content), "application/json");
            Response.StatusCode = (int)statusCode;
            return result;
        }
    }
}
