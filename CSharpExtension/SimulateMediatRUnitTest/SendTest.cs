using SimulateMediatR;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimulateMediatRUnitTest
{
    public class MediatorIoc
    {
        private IMediator imediator;

        public MediatorIoc(IMediator imediator)
        {
            this.imediator = imediator;
        }
    }
    public class SendTest
    {
        //private IMediator _mediatr;

        //public SendTest(IMediator mediator)
        //{
        //    _mediatr = mediator;
        ////}

        //[Fact]
        //public void SendTestUnitTest()
        //{
        //    Request request = new Request() { Age = 11 };
        //    var response = _mediatr.Send<Response>(request);
        //    Assert.Equal(response.Result.Name, "wangk");
        //}

        [Fact]
        public async Task SendTestUnitTest()
        {
            var container = new Container(cfg =>
              {
                  cfg.Scan(scanner =>
                  {
                      scanner.IncludeNamespaceContainingType<Request>();
                      scanner.WithDefaultConventions();
                      scanner.AddAllTypesOf(typeof(IHandler<,>));
                  });
                  cfg.For<IHandler<Request, Response>>().Use<Handle>();
                  cfg.For<ServiceFactory>().Use<ServiceFactory>(ctx => t => ctx.GetInstance(t));
                  cfg.For<IMediator>().Use<Mediator>();
              });

            var mediator = container.GetInstance<IMediator>();
            var response = mediator.Send<Response>(new Request() { Age = 11 });
            Assert.Equal(response.Result.Name, "wangk");
        }
    }

    public class Request : IRequest<Response>
    {
        public int Age { get; set; }
    }

    public class Response
    {
        public string Name { get; set; }
    }

    public class Handle : IHandler<Request, Response>
    {
        Task<Response> IHandler<Request, Response>.Handle(Request request)
        {
            return Task.FromResult(new Response() { Name = "wangkai" });
        }
    }
}
