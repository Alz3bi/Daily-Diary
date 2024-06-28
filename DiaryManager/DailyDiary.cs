using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DiaryManager
{
    public class DailyDiary
    {
        static readonly string pattern = @"^\d{4}-\d{2}-\d{2}$";
        static readonly Regex regex = new Regex(pattern);

        static List<string> lines = new List<string>();
        static List<Entry> entries = new List<Entry>();

        static readonly string filePath = Path.Combine(Environment.CurrentDirectory, "mydiary.txt");
        public static void Interface()
        {
            const string exit = "Press any key to return to main menu";
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to Daily Diary");
                Console.WriteLine("1. Read Entries");
                Console.WriteLine("2. Add Entry");
                Console.WriteLine("3. Remove Entry");
                Console.WriteLine("4. Search by Date");
                Console.WriteLine("5. Search by Content");
                Console.WriteLine("6. Total Entries");
                Console.WriteLine("7. Exit");
                Console.WriteLine("Enter your choice:");
                int choice = GetChoice();
                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        ReadEntries();
                        Console.WriteLine("");
                        Console.WriteLine(exit);
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Clear();
                        AddEntry();
                        Console.WriteLine(exit);
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.Clear();
                        RemoveEntry();
                        Console.WriteLine(exit);
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.Clear();
                        SearchByDate();
                        Console.WriteLine(exit);
                        Console.ReadKey();
                        break;
                    case 5:
                        Console.Clear();
                        SearchByContent();
                        Console.WriteLine(exit);
                        Console.ReadKey();
                        break;
                    case 6:
                        Console.Clear();
                        GetTotalEntries();
                        Console.WriteLine(exit);
                        Console.ReadKey();
                        break;
                    case 7:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }
        
        public static string ReadEntries()
        {
            if (File.Exists(filePath))
            {
                string txtContent = File.ReadAllText(filePath);
                Console.WriteLine("File content:");
                Console.WriteLine(txtContent);
            }
            else
            {
                Console.WriteLine("File not exsist " + filePath);
            }
            return File.ReadAllText(filePath);
        }
        public static void AddEntry()
        {
            

            if (File.Exists(filePath))
            {
                Entry entry = new Entry();
                entry.Date = GetDate();
                if (File.ReadAllText(filePath).Contains(entry.Date))
                {
                    Console.WriteLine("Entry already exists for this date.");
                    return;
                }
                entry.Content = GetContent();
                AddEntryToText(entry);
            }
            else
            {
                Console.WriteLine("File not exsist " + filePath);
            }
            

        }
        public static int AddEntryToText(Entry entry)
        {
            string entryString = "\n" + entry.Date + "\n" + entry.Content;
            File.AppendAllText(filePath, entryString + Environment.NewLine);
            return GetTotalEntries();
        }
        public static void RemoveEntry()
        {
            Console.WriteLine("Please enter the date you want to remove:");
            string date = GetDate();
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File does not exist " + filePath);
                return;
            }

            List<string> fileContent = File.ReadAllLines(filePath).ToList();
            bool dateFound = false;
            for (int i = 0; i < fileContent.Count; i++)
            {
                if (fileContent[i] == date)
                {
                    if (i > 0 && i + 1 < fileContent.Count)
                    {
                        fileContent.RemoveAt(i + 1);
                        fileContent.RemoveAt(i);
                        fileContent.RemoveAt(i - 1);
                        dateFound = true;
                        Console.WriteLine("Entry removed.");
                        break;
                    }
                }
            }

            if (!dateFound)
            {
                Console.WriteLine("Date not found.");
                return;
            }

            File.WriteAllLines(filePath, fileContent.ToArray());
        }
        public static int GetChoice()
        {
            string? input = Console.ReadLine();
            bool success = Int32.TryParse(input, out int choice);
            while (!success || choice < 1 || choice > 7)
            {
                Console.WriteLine("Please enter a valid input");
                input = Console.ReadLine();
                success = Int32.TryParse(input, out choice);
            }
            return choice;
        }
        public static string GetDate()
        {
            Console.WriteLine("Enter the date (yyyy-mm-dd):");
            string? date = Console.ReadLine();
            while (!regex.IsMatch(date))
            {
                Console.WriteLine("Please enter a valid date (yyyy-mm-dd):");
                date = Console.ReadLine();
            }
            return date;
        }
        public static string GetContent()
        {
            Console.WriteLine("Enter the content:");
            string? content = Console.ReadLine();
            while (string.IsNullOrEmpty(content) || string.IsNullOrWhiteSpace(content))
            {
                Console.WriteLine("Please enter a valid content:");
                content = Console.ReadLine();
            }
            return content;
        }
        public static void GetEntries()
        {
            entries.Clear();
            if (File.Exists(filePath))
            {
                lines = File.ReadAllLines(filePath).ToList();
                for (int i = 0; i < lines.Count; i++)
                {
                    if (regex.IsMatch(lines[i]))
                    {
                        Entry entry = new Entry();
                        entry.Date = lines[i];
                        entry.Content = lines[i + 1];
                        entries.Add(entry);
                    }
                }
            }
        }
        public static int GetTotalEntries()
        {
            GetEntries();
            Console.WriteLine("Total entries: " + entries.Count);
            return entries.Count;
        }
        public static void SearchByDate()
        {
            GetEntries();
            string date = GetDate();
            bool found = false;
            foreach (Entry entry in entries)
            {
                if (entry.Date == date)
                {
                    Console.WriteLine("Entry found:");
                    Console.WriteLine(entry.Date);
                    Console.WriteLine(entry.Content);
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                Console.WriteLine("Entry not found.");
            }
        }
        public static void SearchByContent()
        {
            GetEntries();
            string content = GetContent();
            bool found = false;
            foreach (Entry entry in entries)
            {
                if (entry.Content.Contains(content))
                {
                    Console.WriteLine("Entry found:");
                    Console.WriteLine(entry.Date);
                    Console.WriteLine(entry.Content);
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                Console.WriteLine("Entry not found.");
            }
        }
    }
}
