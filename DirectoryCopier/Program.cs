/*
 * Author:          Joe Katuzienski
 * Date:            9/21/2018
 * Revision Date:   10/23/2018
 * Purpose:         To copy family photos and videos to Grandma's portable hard drive
 */


using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace DirectoryCopier
{
    class Program
    {
        static void Main(string[] args)
        {
            
            List<string> dirs = new List<string> { };
            List<string> files = new List<string> { };

            int count = 0;

            //used to create template directories
            //CreateDirectories();


            Console.WriteLine("which cd are you on ? -> ");
            count = Convert.ToInt32(Console.ReadLine());
            for (int i = count; i <= 150; i++)
            {
                try
                {
                    foreach (string directories in System.IO.Directory.GetDirectories(@"E:\"))
                    {
                        //debug stuff
                        Console.WriteLine("Directories");
                        Console.WriteLine(directories);
                        dirs.Add(directories);
                        Console.WriteLine("\n" + directories + " Files");

                        //Console.Beep();
                        foreach (string file in System.IO.Directory.GetFiles(directories))
                        {
                            //filter out unwanted files
                            if (!file.Replace(directories, "").Contains("IFO") && !file.Replace(directories, "").Contains("BUP") && !file.Replace(directories, "").Contains("VIDEO") && !file.Replace(directories, "").Contains(".htm"))
                            {
                                Console.WriteLine(file);
                                //entering copy
                                //Console.Beep();
                                File.Copy(file, formatString(file, directories, i)); //has to format which folder to copy to
                                Console.WriteLine(file + " copied!");
                                //Console.Beep();
                            }
                        }

                    }

                    //Console.Beep();
                    mciSendStringA("set CDAudio door open", "", 127, 0);
                }
                catch
                {

                    Console.WriteLine("waiting...");
                    System.Threading.Thread.Sleep(1000);
                    i -= 1; //keep looking for the next folder to add to
                }

            }
        }

        //for importing the library to open the disc tray
        [DllImport("winmm.dll", EntryPoint = "mciSendStringA")]
        public static extern void mciSendStringA(string lpstrCommand,
       string lpstrReturnString, long uReturnLength, long hwndCallback);

        //formats string to next folder in sequence
        private static string formatString(string f, string s, int i)
        {
            string file = "";
            string folder = "";
            ;

            file = f.Replace(s, "");
            folder = GetNextFolder(i);

                return folder + file.Replace(".VOB", ".mp4");
        }

        //finding the next folder
        //if 23rd cd is inserted grab the 24th folder
        private static string GetNextFolder(int i)
        {
            foreach (string s in System.IO.Directory.GetDirectories(@"F:\Home Videos\"))
                if (s.Contains(i.ToString()))
                    return s;
                else
                    continue;
        
            //fails
            return "";
            
        }

        //used to create directories 17-152 followed by _
        public void CreateDirectories()
        {
            for (int i = 17; i < 153; i++)
                if (!System.IO.Directory.Exists(@"F:\Home Videos\" + i + "_"))
                {
                    System.IO.Directory.CreateDirectory(@"F:\Home Videos\" + i + "_");
                }
        }
    }
    
}
