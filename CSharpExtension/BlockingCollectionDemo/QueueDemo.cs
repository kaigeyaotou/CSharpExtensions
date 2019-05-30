using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DemoLibrary
{
    public class QueueDemo
    {
        public static void Try_EnqueueDequeue()
        {
            Queue myQueue = new Queue();
            myQueue.Enqueue(1);
            myQueue.Enqueue(2);
            myQueue.Enqueue(31);
            myQueue.Enqueue("this is my first string  ");
            myQueue.Enqueue(null);
            myQueue.Enqueue("this is my last value");

            Console.WriteLine($"Number of Elements = {myQueue.Count}");
            //foreach (var item in myQueue)
            //{
            //    Console.WriteLine(item);
            //}
            while (myQueue.Count > 0)
            {
                Console.WriteLine(myQueue.Dequeue());
            }
        }
    }
}
