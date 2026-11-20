using System;
using System.Threading;
using System.Collections.Generic;

class ThreadingTutorial
{
static void intervalCount()
{
    int i = 1;
        while (true)
        {
            Console.WriteLine(i++);
            Thread.Sleep(1000);
        }
}

static void Main(String [] args)
    {
        Console.WriteLine("Hello World");
        Thread t = new Thread(new ThreadStart(intervalCount));
        t.Start();
    }
}
