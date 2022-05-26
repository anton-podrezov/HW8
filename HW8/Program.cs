using System;
using System.IO;
class MainClass
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Введите путь к папке");
        string DirName = Console.ReadLine();
        DirDelet(DirName);
    }

    static void DirDelet(string dirName)
    {
        try
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dirName);

            if (dirInfo.Exists)
            {
                DirectoryInfo[] folderNames = dirInfo.GetDirectories();

                foreach (var item in folderNames)
                {
                    DirDelet(item.FullName);

                    if (DateTime.Now - item.LastAccessTime > TimeSpan.FromMinutes(30))
                    {
                        item.Delete(true);
                    }
                }

                FileInfo[] fileNames = dirInfo.GetFiles();

                foreach (var item in fileNames)
                {
                    if (DateTime.Now - item.LastAccessTime > TimeSpan.FromMinutes(30))
                    {
                        item.Delete();
                    }
                }

                Console.WriteLine($"Очистка {dirInfo.FullName} от файлов и папок, не использующихся более 30 минут завершена");
            }
            else
            {
                Console.WriteLine($"Не верный адрес дериктории");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

    }
}