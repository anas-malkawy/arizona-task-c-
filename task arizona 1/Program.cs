Random r = new Random();
int rInt = r.Next(0, 100);

int a = 0;

while (true)
{
    a++;

    Console.WriteLine("Enter a number: ");

    string i = Console.ReadLine();

    if (i.ToLower() == "quit")
    {
        Console.WriteLine("You have exited the game");
        Console.WriteLine($"You have tried {a} times");

        break;
    }

    bool succedParse = int.TryParse(i, out int num1); // Fixed TryParse to include the 'out' parameter
    if (!succedParse)
    {
        Console.WriteLine("Invalid input. Please enter a valid number.");
        continue;
    }

    if (num1 > rInt)
    {
        Console.WriteLine("higher");
    }
    else if (num1 < rInt)
    {
        Console.WriteLine("less");
    }
    else
    {
        Console.WriteLine("equal");
    }

    if (num1 == rInt)
    {
        Console.WriteLine($"You have tried {a} times");

        break;
    }
}