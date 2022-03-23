// See https://aka.ms/new-console-template for more information
var titles = new Dictionary<string, string>()
{
    ["Doyle, Arthur Conan"] = "Hound of the Barnesville, The",
    ["London, Jack"] = "Call of the Wild, The",
    ["Shakespeare, William"] = "Tempest, The"
};
Console.WriteLine("Author and Title List");
Console.WriteLine();
Console.WriteLine($"|{"Author",-25}|{"Title",30}|");
foreach (var title in titles)
    Console.WriteLine($"|{title.Key,-25}|{title.Value,30}|");

Console.WriteLine("Hello, World!");

//the code now runs with out program class and Main Method as follows
Programs.Main();
public class Vegetable
{
    public Vegetable(string name) => Name = name;

    public string Name { get; }

    public override string ToString() => Name;
}

class Programs
{
    public enum Unit { item, kilogram, gram, dozen };

    public static void Main()
    {
        var item = new Vegetable("eggplant");
        var date = DateTime.Now;
        var price = 1.99m;
        var unit = Unit.item;
        Console.WriteLine($"On {date}, the price of {item} was {price} per {unit}.");
    }
}

/*
 eta pugyo aile lai
https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/
https://docs.microsoft.com/en-us/dotnet/csharp/how-to/*/