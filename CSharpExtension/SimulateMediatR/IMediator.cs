using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimulateMediatR
{
    public interface IMediator
    {
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request);
    }
}
