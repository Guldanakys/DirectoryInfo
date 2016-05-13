using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1
{
    class Program
    {
        static void Main()
        {
            string path = @"D:\";
            DirectoryInfo dir = new DirectoryInfo(path);
            string oldpath = "";
            int cursor_pos = 0;
            int cnt = 0;
            bool isFileOpen = false;
            while (true)
            {
                //Console.Clear();
                if (path != oldpath)//esli drugoi puti
                {
                    oldpath = path;
                    cnt = 0;
                    foreach (DirectoryInfo item in dir.GetDirectories())
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(" " + cnt + ". " + item.Name);//nomer i imya papki
                        Console.SetCursorPosition(45, cnt);
                        try//podpapki proverka na dostyp
                        {
                            Console.WriteLine("Dir    SubDirs: " + item.GetDirectories().Length);
                        }
                        catch (Exception err)
                        {
                            Console.WriteLine("Dir    Cannot OPEN(Access denied)");
                        }
                        cnt++;
                    }
                    foreach (FileInfo item in dir.GetFiles())
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(" " + cnt + ". " + item.Name);
                        Console.SetCursorPosition(45, cnt);
                        Console.WriteLine("File   " + "Size: " + item.Length + " bytes");
                        cnt++;
                    }
                }


                //Vistavit cursor
                Console.SetCursorPosition(0, cursor_pos);

                ConsoleKeyInfo keyPressed = Console.ReadKey();
                if (keyPressed.Key == ConsoleKey.UpArrow)
                {
                    if (cursor_pos == 0)
                    {
                        cursor_pos = cnt - 1;
                    }
                    else cursor_pos--;
                }
                else if (keyPressed.Key == ConsoleKey.DownArrow)
                {
                    if (cursor_pos == cnt - 1)
                    {
                        cursor_pos = 0;
                    }
                    else cursor_pos++;
                }
                else if (keyPressed.Key == ConsoleKey.Enter)
                {
                    if (cursor_pos < dir.GetDirectories().Count())
                    {
                        path = dir.GetDirectories()[cursor_pos].FullName;
                        dir = new DirectoryInfo(path);
                        cursor_pos = 0;
                        cnt = 0;
                        Console.Clear();
                    }
                    else
                    {
                        Console.Clear();
                        StreamReader inp = new StreamReader(dir.GetFiles()[cursor_pos - dir.GetDirectories().Length].FullName);
                        string line;
                        while ((line = inp.ReadLine()) != null)
                        {
                            Console.WriteLine(line);
                            //Console.ReadKey();//Pause
                        }
                        isFileOpen = true;
                    }
                }
                else if (keyPressed.Key == ConsoleKey.Escape)
                {
                    if (isFileOpen)
                    {
                        isFileOpen = false;
                        path = dir.FullName;
                        oldpath = "";
                        dir = new DirectoryInfo(path);
                        cursor_pos = 0;
                        cnt = 0;
                        Console.Clear();
                    }
                    else
                    {
                        try
                        {
                            path = dir.Parent.FullName;
                            dir = new DirectoryInfo(path);
                            cursor_pos = 0;
                            cnt = 0;
                            Console.Clear();
                        }
                        catch (Exception err)
                        {
                            break;
                        }
                    }

                }
            }
            Console.Clear();
            Console.WriteLine("BYE BYE");
            Console.ReadKey();
        }
    }
}
