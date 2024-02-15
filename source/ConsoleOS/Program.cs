using MOOS;
using MOOS.Misc;
using System;
using System.Runtime;

/*
 * Notes & Ramblings:
 * I tried to pull Corlib out of the equation and just allow a project file, it appears it doesn't work that way
 * I think aleratively we can expand the scope of a reference to a shared item, not sure about this;
 * I might try to fiddle with a symbolic styled link, whether corlib becomes phyiscal or not I don't know;;
 * It's not my goal to give user less access to project files, the goal rather is to remove clutter potional,
 * I think I might split apart the .sln file or at least change it in such a way that all possible kernels are first thing
 * a user sees, I'd have to invest in in-depth documenation to ensure that even if edited the system would still compile
 * for everyone
 * -------------------
 * 
 */
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
            try
            {
                Console.WriteLine("I work");
                //throw new Exception("Failure"); //Dispite the exception name, this *does* work (partly)
            }
            catch (Exception ee)
            {
                Console.WriteLine("I work but i'm an exception");
            }
            string s = Console.ReadLine();
            Console.WriteLine(s);
        }

    }
}