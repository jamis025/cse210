using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Journal
{
    private List<Entry> _entries = new List<Entry>();
    private List<string> _prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?",
        "What is one thing I am grateful for today?",
        "Describe a challenge you overcame today."
    };

    public void AddEntry()
    {
        Random rand = new Random();
        string randomPrompt = _prompts[rand.Next(_prompts.Count)];

        Console.WriteLine($"\n{randomPrompt}");
        Console.Write("> ");
        string response = Console.ReadLine();

        Entry newEntry = new Entry()
        {
            Prompt = randomPrompt,
            Response = response
        };

        _entries.Add(newEntry);
    }

    public void DisplayJournal()
    {
        if (_entries.Any())
        {
            Console.WriteLine("\n--- Journal Entries ---");
            foreach (var entry in _entries)
            {
                Console.WriteLine(entry.ToString());
            }
            Console.WriteLine("-----------------------\n");
        }
        else
        {
            Console.WriteLine("\nJournal is currently empty.\n");
        }
    }

    public void SaveToFile()
    {
        Console.Write("Enter filename to save to (e.g., myjournal.txt): ");
        string filename = Console.ReadLine();

        try
        {
            using (StreamWriter sw = new StreamWriter(filename))
            {
                foreach (var entry in _entries)
                {
                    sw.WriteLine(entry.ToFileString());
                }
            }
            Console.WriteLine($"\nJournal saved to {filename} successfully.\n");
        }
        catch (Exception e)
        {
            Console.WriteLine($"\nAn error occurred: {e.Message}\n");
        }
    }

    public void LoadFromFile()
    {
        Console.Write("Enter filename to load from (e.g., myjournal.txt): ");
        string filename = Console.ReadLine();

        if (File.Exists(filename))
        {
            try
            {
                _entries.Clear();
                string[] lines = File.ReadAllLines(filename);

                foreach (string line in lines)
                {
                    string[] parts = line.Split('~');
                    if (parts.Length == 3)
                    {
                        Entry loadedEntry = new Entry(parts[0], parts[1], parts[2]);
                        _entries.Add(loadedEntry);
                    }
                }
                Console.WriteLine($"\nJournal loaded from {filename} successfully. Total entries: {_entries.Count}\n");
            }
            catch (Exception e)
            {
                Console.WriteLine($"\nAn error occurred while loading: {e.Message}\n");
            }
        }
        else
        {
            Console.WriteLine($"\nFile not found: {filename}\n");
        }
    }
}