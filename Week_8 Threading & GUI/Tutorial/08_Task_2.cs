using System;
using System.Threading;
using System.Collections.Generic;

class ThreadingTutorial
{
static void intervalCount(Object limitObj)
{
    int i = 1;
    int limit = (int)limitObj;
        while (i <= limit)
        {
            Console.WriteLine(i++);
            Thread.Sleep(1000);
        }
}

static void Main(String [] args)
    {
        Console.WriteLine("Hello World");
        Thread t = new Thread(new ParameterizedThreadStart(intervalCount));
        t.Start(5);
    }
}
