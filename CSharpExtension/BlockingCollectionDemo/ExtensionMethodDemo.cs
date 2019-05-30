using System;
using System.Collections.Generic;
using System.Text;

namespace DemoLibrary
{
    public static class ExtensionMethodDemo
    {
        public static void SayName(this Person person)
        {
            Console.WriteLine("Person's Name equal" + person.Name);
        }
    }

    public class Person
    {
        public string Name { get; set; }

    }

    public interface IMyInterface
    {
        void MethodB();
    }

    public static class Extension
    {
        public static void MethodA(this IMyInterface myInterface, int i)
        {
            Console.WriteLine("Extension.MehtodA(this IMyInterface , int i)");
        }

        public static void MethodA(this IMyInterface myInterface, string str)
        {
            Console.WriteLine("Extension.MethodA(this IMyinterface myinterface,string str)");
        }

        public static void MethodB(this IMyInterface myInterface)
        {
            Console.WriteLine("Extension.MethodB(this IMyInterface myInterface)");
        }

        public class A : IMyInterface
        {
            public void MethodB()
            {
                Console.WriteLine("A.MethodB()");
            }
        }

        public class B : IMyInterface
        {
            public void MethodB()
            {
                Console.WriteLine("B.MethodB()");
            }
        }

        public class C : IMyInterface
        {
            public void MethodB()
            {
                Console.WriteLine("C.MethodB()");
            }
            public void MethodA(object obj)
            {
                Console.WriteLine("C.MethodA(object obj)");
            }
        }
    }
}
