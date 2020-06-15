using System;
using System.Management;
using see_gpu_label.Properties;

namespace see_gpu_label
{
    class Program
    {
        const string name = "see-gpu-label";
        static string gpu_label = "";

        static void usage()
        {
            Console.WriteLine("usage: " +name+ " [OPTION]... ]");
            Console.WriteLine("A tool to see your gpu's name.");
            Console.WriteLine("Arguments & usage, and an explanation: ");
            Console.WriteLine("\n-h, --help          display this message.");
            Console.WriteLine("-g, --gpu-only      display gpu label only.");
            Environment.Exit(0);
        }

        static void display_credit()
        {
            Console.WriteLine("see-vram-script.");
            Console.WriteLine("https://github.com/W1ll1am04" + "\n");
            return;
        }

        static bool check_empty()
        {
            if (gpu_label == null || gpu_label == "")
            {
                return true;
            }
            return false;
        }

        static void Main(string[] args)
        {
            try
            {
                ManagementObjectSearcher myVideoObject = new ManagementObjectSearcher("select * from Win32_VideoController");
                foreach (ManagementObject obj in myVideoObject.Get()){ gpu_label = ""+obj["Name"]; }

                Console.Title = name;
                if (args.Length == 0)
                {
                    display_credit();
                    if (check_empty() == true)
                    {
                        Console.WriteLine("Unable to get gpu label. WDDM returned an empty value.");
                        Console.Write("Debug information: WDDM returned: '" + gpu_label + "'.");
                    }
                    else
                    {
                        Console.WriteLine("GPU: " + gpu_label);
                    }
                }
                else
                {
                    if (args[0] == "-h" || args[0] == "--help")
                    {
                        usage();
                    }
                    else if (args[0] == "-g" || args[0] == "--gpu-only")
                    {
                        if (check_empty() == true)
                        {
                            Console.WriteLine("Unable to get gpu label. WDDM returned an empty value.");
                            Console.Write("Debug information: WDDM returned: '" +gpu_label+ "'.");
                        }
                        else
                        {
                            Console.Write(gpu_label);
                        }
                    }
                    else
                    {
                        usage();
                    }
                }
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An exception occured while getting gpu information: " + ex.Message);   
            }
        }
    }
}
