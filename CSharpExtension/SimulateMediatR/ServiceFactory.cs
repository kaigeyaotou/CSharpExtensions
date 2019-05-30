using System;
using System.Collections.Generic;
using System.Text;

namespace SimulateMediatR
{
    public delegate object ServiceFactory(Type type);

    public static class ServiceExtension
    {
        public static T GetInstance<T>(this ServiceFactory factory)
        {
            return (T)factory(typeof(T));
        }

        public static IEnumerable<T> GetInstances<T>(this ServiceFactory factory)
        {
            return (IEnumerable<T>)factory(typeof(T));
        }
    }
}
