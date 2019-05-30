using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimulateMediatR
{
    internal abstract class RequestHandlerBase
    {
        protected static THandler GetHandler<THandler>(ServiceFactory factory)
        {
            THandler handler;

            try
            {
                handler = factory.GetInstance<THandler>();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"error");
            }

            return handler;
        }
    }

    internal abstract class RequestHandlerWrapper<TResponse> : RequestHandlerBase
    {
        public abstract Task<TResponse> Handle(IRequest<TResponse> request, ServiceFactory factory);
    }

    internal class RequestHandlerWrapperImpl<TRequest, TResponse> : RequestHandlerWrapper<TResponse> where TRequest : IRequest<TResponse>
    {
        public override Task<TResponse> Handle(IRequest<TResponse> request, ServiceFactory factory)
        {
            //Task<TResponse> Handler() => GetHandler<IHandler<TRequest, TResponse>>(factory).Handle((TRequest)request);
            var handler = GetHandler<IHandler<TRequest, TResponse>>(factory);

            return handler.Handle((TRequest)request);

        }
    }
}
