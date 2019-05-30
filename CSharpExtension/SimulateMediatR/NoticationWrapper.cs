using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace SimulateMediatR
{
    interface INoticationWrapper
    {
        Task Handle<TNotication>(TNotication request, ServiceFactory factory) where TNotication : INotication;
    }

    internal class NoticationWrapperImpl<TNotication> : INoticationWrapper where TNotication : INotication
    {
        public Task Handle<TNotication>(TNotication request, ServiceFactory factory)
        {
            IEnumerable<TNotication> list = factory.GetInstances<TNotication>();

        }
    }
}
