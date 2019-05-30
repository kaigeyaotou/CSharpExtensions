using System;
using System.Collections.Generic;
using System.Text;

namespace DemoLibrary
{
    public class MakeGenericTypeDemo
    {
        public static void Try_MakeGenericType()
        {
            Console.WriteLine("\r\n-- Create a constructed type from the generic Dictionary type.");

            // Create a type object representing the generic Dictionary
            // type ,by omitting the type arguments ( but keeping hte 
            // comma that separates them,so the compiler can infer the 
            // number of type parameters)

            Type generic = typeof(Dictionary<,>);
            DisplayTypeInfo(generic);
            Type[] typeArgs = { typeof(string), typeof(MakeGenericTypeDemo) };
            Type constructed = generic.MakeGenericType(typeArgs);
            DisplayTypeInfo(constructed);

            Console.WriteLine("\r\n --Compare types obtained by different methods:");

            Type t = typeof(Dictionary<string, MakeGenericTypeDemo>);
            Console.WriteLine("\tAre the constructed types equal?{0}", t == constructed);
            Console.WriteLine("\tAre the generic types equal?{0}", t.GetGenericTypeDefinition() == generic);
        }

        public static void DisplayTypeInfo(Type t)
        {
            Console.WriteLine("\r\n {0}", t);

            Console.WriteLine("\t Is this a generic type definition?{0}", t.IsGenericTypeDefinition);

            Console.WriteLine("\r Is it a generic type?{0}", t.IsGenericType);

            Type[] typeArguments = t.GetGenericArguments();
            Console.WriteLine("\t List type arguments ({0}):", typeArguments.Length);
            foreach (Type tParam in typeArguments)
            {
                Console.WriteLine("\t\t{0}", tParam);
            }
        }
    }
}
