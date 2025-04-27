// See https://aka.ms/new-console-template for more information
Console.WriteLine("................");



var counter = 0;
var max = args.Length is not 0 ? Convert.ToInt32(args[0]) : 30;

while (max is -1 || counter < max)
{
    Console.WriteLine($"Counter: {++counter}");

    await Task.Delay(TimeSpan.FromMilliseconds(8_000));
}


Console.WriteLine("-------------");

