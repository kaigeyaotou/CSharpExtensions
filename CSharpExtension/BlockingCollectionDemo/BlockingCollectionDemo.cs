using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DemoLibrary
{
    public class BlockingCollectionDemo
    {
    }

    public class AddTakeDemo
    {
        // Demonstrates:
        // BlockingCollection<T>.Add()
        // BlockingCollection<T>.Take()
        // BlockingCollection<T>.CompleteAdding()

        public static async Task BC_AddTakeCompleteAdding()
        {
            using (BlockingCollection<int> bc = new BlockingCollection<int>())
            {
                // spin up a task to pop the blockingcollection
                using (Task t1 = Task.Factory.StartNew(() =>
                 {
                     bc.Add(1);
                     bc.Add(2);
                     bc.Add(3);
                     bc.CompleteAdding();
                 }))
                {
                    using (Task t2 = Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            // consume consume the blockingcollection
                            while (true) Console.WriteLine(bc.Take() + "consume");
                        }
                        catch (InvalidOperationException)
                        {
                            Console.WriteLine("that's all");
                        }
                    }))
                        await Task.WhenAll(t1, t2);

                }

            }
        }
    }

    public class TryTakeDemo
    {
        // demostrates:
        // blcokingcollection<t>.add
        // blockingcollection<t>.completeadding()
        // blockingcollection<t>.trytake()
        // blockingcollection<t>.iscompleted

        public static void BC_TryTake()
        {
            // construct and fill our blockingcollection
            using (BlockingCollection<int> bc = new BlockingCollection<int>())
            {
                int NUMITEMS = 10000;
                for (int i = 0; i < NUMITEMS; i++) bc.Add(i);
                bc.CompleteAdding();
                int outerSum = 0;

                // delegate for consuming the blockingcollection and adding up all items
                Action action = () =>
                {
                    int localItem;
                    int localSum = 0;

                    while (bc.TryTake(out localItem)) localSum += localItem;
                    Interlocked.Add(ref outerSum, localSum);
                };

                Action action1 = () =>
                {
                    int localItem;
                    int localSum = 0;

                    while (bc.TryTake(out localItem)) localSum += localItem;
                    outerSum = 0;
                    Interlocked.Add(ref outerSum, localSum + 1);
                };

                Action action2 = () =>
                {
                    int localItem;
                    int localSum = 0;

                    while (bc.TryTake(out localItem)) localSum += localItem;
                    outerSum = 0;
                    Interlocked.Add(ref outerSum, localSum + 2);
                };

                Parallel.Invoke(action, action1, action2);

                Console.WriteLine("Sum[0..{0})={1},should be {2}", NUMITEMS, outerSum, ((NUMITEMS * (NUMITEMS - 1)) / 2));
                Console.WriteLine("bc.IsCompleted={0} (should be true)", bc.IsCompleted);
            }
        }
    }

    public class FromToAnyDemo
    {
        // demonstrates:
        // bounded blockingcollection<t>
        // blocingcollection<t>.tryaddtoany()
        // blockingcollection<t>.trytakefromany()

        public static void BC_FromToAny()
        {
            BlockingCollection<int>[] bcs = new BlockingCollection<int>[2];
            bcs[0] = new BlockingCollection<int>(5);
            bcs[1] = new BlockingCollection<int>(5);

            //should be able to add 10 items w/o blocking

            int numFailures = 0;
            for (int i = 0; i < 10; i++)
            {
                if (BlockingCollection<int>.TryAddToAny(bcs, i) == -1) numFailures++;
            }
            Console.WriteLine("tryaddtoany:{0} failures(should be 0)", numFailures);

            ////should be able to retrieve 10 items
            //int item;
            //int numItems = 0;
            //while (BlockingCollection<int>.TryTakeFromAny(bcs, out item) != -1) numItems++;
            //Console.WriteLine("trytakfromany:retrieved{0} items (should be 10)", numItems);
            // consume two 
            bcs[0].CompleteAdding();
            foreach (var item in bcs[0].GetConsumingEnumerable())
            {
                Console.WriteLine("bcs0 item {0}", item);
            }

            foreach (var item in bcs[1].GetConsumingEnumerable())
            {
                Console.WriteLine("bcs1 item {0}", item);
            }
        }
    }

    public class ConsumingEnumerableDemo
    {
        // demostrates:
        // blockingcollection<t>.add()
        // blockingcollection<t>.completeadding()
        // blockingcollection<t>.getconsumingEnumerable()

        public static async Task BC_GetConsumingEnumerable()
        {
            using (BlockingCollection<int> bc = new BlockingCollection<int>())
            {
                await Task.Factory.StartNew(async () =>
                {
                    for (int i = 0; i < 10; i++)
                    {
                        bc.Add(i);
                        await Task.Delay(100);
                    }
                    bc.CompleteAdding();
                });

                // now consume the blocing collection with foreach.
                // use bc.getconsumingenumerable() instead of just bc because hte former will block waiting
                // for compleion the latter will
                // simply take a snapshot of the current state of hte underlying collection
                foreach (var item in bc.GetConsumingEnumerable())
                {
                    Console.WriteLine(item);
                }
                return;
            }
        }
    }
}
