using System;
using System.Threading;

namespace Donut
{
    class Program
    {
        static void Main(string[] args)
        {
            Donut donut = new Donut(40, 40);
            
            float A = 0;
            float B = 0;
            
            while (true)
            {
                donut.RenderFrame(A, B);

                A += 0.02f;
                B += 0.01f;
                
                Thread.Sleep(16);
            }
        }
    }
}