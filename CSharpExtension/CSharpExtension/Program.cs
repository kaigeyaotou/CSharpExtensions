using DemoLibrary;
using System;
using static DemoLibrary.Extension;

namespace CSharpExtension
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //await BlockingCollectionDemo.AddTakeDemo.BC_AddTakeCompleteAdding();
            //BlockingCollectionDemo.TryTakeDemo.BC_TryTake();
            //await BlockingCollectionDemo.ConsumingEnumerableDemo.BC_GetConsumingEnumerable();
            //BlockingCollectionDemo.FromToAnyDemo.BC_FromToAny();

            //DemoLibrary.QueueDemo.Try_EnqueueDequeue();
            //DemoLibrary.MakeGenericTypeDemo.Try_MakeGenericType();

            //Person person = new Person();
            //person.Name = "wangk";
            //person.SayName();

            A a = new A();
            B b = new B();
            C c = new C();

            a.MethodA(1); //should be Extension.MehtodA(this IMyInterface , int i)
            a.MethodA("hello"); // should be Extension.MethodA(this IMyinterface myinterface,string str)

            a.MethodB(); // should be A.MethodB()
            Console.WriteLine();
            b.MethodA(1); // should be Extension.MehtodA(this IMyInterface , int i)
            b.MethodB(); // should be B.MethodB()
            b.MethodA("hello"); // should be Extension.MethodA(this IMyinterface myinterface,string str)
            Console.WriteLine();

            c.MethodA(1); // should be C.MethodA(object obj)
            c.MethodA("hello"); // should be C.MethodA(object obj)
            c.MethodB(); // should be B.MethodB()

            Console.ReadKey();
        }
    }
}
