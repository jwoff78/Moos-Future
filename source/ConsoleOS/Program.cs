using MOOS;
using System.Runtime;

unsafe class Program
{
    static void Main() { }

    /*
     * Minimum system requirement:
     * 1024MiB of RAM
     * Memory Map:
     * 256 MiB - 512MiB   -> System
     * 512 MiB - ∞     -> Free to use
     */
    //Check out Kernel/Misc/EntryPoint.cs
    [RuntimeExport("KMain")]
    static void KMain() 
    {
        
        Console.Clear();
        Console.WriteLine("Now you are in MOOS-ConsoleOS!");
        //Alright so nint and nuint work
        nint e = 10;
        nuint f = 10;
        Console.WriteLine("e = " + e.ToString() + ", " + "f = " + f.ToString());
        //In order to write a debugger class we need to fix launching mechinsm
        //And maybe enable vmware debug mode
        //Serial.WriteLine("Now you are in MOOS-ConsoleOS!");
        for(; ; ) 
        {
            string s = Console.ReadLine();
            Console.WriteLine(s);
        }
    }
}