using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimulateMediatR
{

    public class Mediator : IMediator
    {
        private ServiceFactory ServiceFactory;
        private ConcurrentDictionary<Type, object> _handlers = new ConcurrentDictionary<Type, object>();
        public Mediator(ServiceFactory factory)
        {
            this.ServiceFactory = factory;
        }

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        {
            var requestType = request.GetType();
            var obj = Activator.CreateInstance(typeof(RequestHandlerWrapperImpl<,>)
                .MakeGenericType(requestType, typeof(TResponse)));
            var handler = (RequestHandlerWrapper<TResponse>)this._handlers
                .GetOrAdd(requestType, Activator.CreateInstance(typeof(RequestHandlerWrapperImpl<,>)
                .MakeGenericType(requestType, typeof(TResponse))));

            return handler.Handle(request, ServiceFactory);
        }

        public Task Publish<TRequest>(TRequest request) where TRequest : INotication
        {
            var requestType = request.GetType();
        }
    }
}
