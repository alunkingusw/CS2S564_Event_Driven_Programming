using System;
using System.Threading;
using System.Collections.Generic;
using System.Configuration.Assemblies;

namespace TutorialEight{
    class IntegerStack
    {
        Stack<int> numbers;

        public IntegerStack()
        {
            //construct the empty stack when the class is initialised
            numbers = new Stack<int>();
            //create the worker thread here
            Thread t = new Thread(new ThreadStart(this.ProcessItems));
            t.Start();
        }
        public void addItem(int value)
        {
            Console.Write("Pushing value, " + value);
            numbers.Push(value);
            Console.WriteLine(" new stack size: " + numbers.Count);
        }
        private void ProcessItems()
        {
            Console.WriteLine("Process items is running...");
            int poppedItem = 0;
            while(true)
            {
                Thread.Sleep(1000);
                if (numbers.TryPop(out poppedItem))
                {
                    Console.WriteLine("Popped value "+ poppedItem + ", new stack size: " + numbers.Count);

                    //count to the number that was popped
                    int i = 1;
                    while (i <= poppedItem)
                    {
                        Console.WriteLine(i++);
                        Thread.Sleep(1000);
                    }
                }
            }
            
        }
    }

    class ThreadingTutorial
    {


    static void Main(String [] args)
        {
            IntegerStack stack = new IntegerStack();
            String input = null;
            int value=0;
            while (true)
            {
                while(input == null){
                    Console.WriteLine("Enter an integer");
                    input = Console.ReadLine();
                    if(int.TryParse(input, out value)){
                        stack.addItem(value);
                    }
                }
                input = null;
                
            }
        }
    }
}