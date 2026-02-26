using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using System.Linq;

class Program
{
    static async Task Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness App Menu:");
            Console.WriteLine("1. Start Breathing Activity");
            Console.WriteLine("2. Start Reflection Activity");
            Console.WriteLine("3. Start Listing Activity");
            Console.WriteLine("4. Exit");
            Console.Write("Select an activity (1-4): ");
            string choice = Console.ReadLine();

            Activity activity = null;

            switch (choice)
            {
                case "1":
                    activity = new BreathingActivity();
                    break;
                case "2":
                    activity = new ReflectionActivity();
                    break;
                case "3":
                    activity = new ListingActivity();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Press any key to return to menu.");
                    Console.ReadKey();
                    continue;
            }

            if (activity != null)
            {
                await activity.Run();
            }
        }
    }
}

class Activity
{
    protected string _name;
    protected string _description;
    protected int _duration;

    public Activity() { }

    protected async Task StartingMessage()
    {
        Console.Clear();
        Console.WriteLine($"Welcome to the {_name}.");
        Console.WriteLine($"\n{_description}");
        Console.Write("\nHow long in seconds would you like for your session? ");
        string durationInput = Console.ReadLine();
        if (int.TryParse(durationInput, out int duration))
        {
            _duration = duration;
        }
        else
        {
            _duration = 30;
            Console.WriteLine("Invalid input. Defaulting to 30 seconds.");
        }
        Console.WriteLine("Prepare to begin...");
        await PauseWithSpinner(5);
    }

    protected async Task EndingMessage()
    {
        Console.WriteLine("\nWell done!");
        await PauseWithSpinner(3);
        Console.WriteLine($"\nYou have completed the {_name} activity for {_duration} seconds.");
        await PauseWithSpinner(5);
    }

    protected async Task PauseWithSpinner(int seconds)
    {
        List<string> spinner = new List<string> { "|", "/", "-", "\\" };
        for (int i = 0; i < seconds * 2; i++) 
        {
            Console.Write(spinner[i % spinner.Count]);
            await Task.Delay(500); 
            Console.Write("\b \b"); 
        }
    }
    
    protected async Task PauseWithCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i);
            await Task.Delay(1000); 
            Console.Write("\b \b"); 
        }
    }

    public virtual async Task Run()
    {
        await StartingMessage();
        await EndingMessage();
    }
}

class BreathingActivity : Activity
{
    public BreathingActivity()
    {
        _name = "Breathing Activity";
        _description = "This activity will help you relax by walking your through breathing in and out slowly. Clear your mind and focus on your breathing.";
    }

    public override async Task Run()
    {
        await StartingMessage();

        int timeElapsed = 0;
        while (timeElapsed < _duration)
        {
            Console.Write("\nBreathe in...");
            await PauseWithCountdown(4); // Inhale for 4 seconds
            timeElapsed += 4;
            if (timeElapsed >= _duration) break;

            Console.Write("\nBreathe out...");
            await PauseWithCountdown(4); // Exhale for 4 seconds
            timeElapsed += 4;
        }

        await EndingMessage();
    }
}

class ReflectionActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };
    private List<string> _questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };
    private Random _random = new Random();

    public ReflectionActivity()
    {
        _name = "Reflection Activity";
        _description = "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.";
    }

    public override async Task Run()
    {
        await StartingMessage();
        Console.WriteLine("\nConsider the following prompt:");
        Console.WriteLine($"\n--- {_prompts[_random.Next(_prompts.Count)]} ---");
        Console.WriteLine("\nWhen you have something in mind, press enter to continue.");
        Console.ReadKey();

        Console.WriteLine("\nNow ponder on each of the following questions as they relate to this experience.");
        Console.Write("You may begin in: ");
        await PauseWithCountdown(5);
        Console.Clear();

        DateTime startTime = DateTime.Now;
        while ((DateTime.Now - startTime).TotalSeconds < _duration)
        {
            Console.Write($"\n> {_questions[_random.Next(_questions.Count)]} ");
            await PauseWithSpinner(5); // Pause with spinner for reflection time
        }

        await EndingMessage();
    }
}

class ListingActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };
    private Random _random = new Random();

    public ListingActivity()
    {
        _name = "Listing Activity";
        _description = "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area of strength or positivity.";
    }

    public override async Task Run()
    {
        await StartingMessage();
        Console.WriteLine("\nList as many responses as you can to the following prompt:");
        Console.WriteLine($"\n--- {_prompts[_random.Next(_prompts.Count)]} ---");
        Console.Write("\nYou may begin in: ");
        await PauseWithCountdown(5);

        Console.WriteLine();
        DateTime startTime = DateTime.Now;
        List<string> items = new List<string>();
        while ((DateTime.Now - startTime).TotalSeconds < _duration)
        {
            Console.Write("> ");
            string item = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(item))
            {
                items.Add(item);
            }
        }

        Console.WriteLine($"\nYou listed {items.Count} items.");

        await EndingMessage();
    }
}