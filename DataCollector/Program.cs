// See https://aka.ms/new-console-template for more information
using Bastion.Common;

Console.WriteLine("................");



var counter = 0;
var max = args.Length is not 0 ? Convert.ToInt32(args[0]) : 30;

while (max is -1 || counter < max)
{
    Console.WriteLine($"Counter: {++counter}");

    AppLoger logger = new AppLoger("MKJ-998", new HttpClient(), "http://192.168.1.225:8090");
    var info = await logger.Log("consolka pyka");
    Console.WriteLine(info ? "message sent" : "not sent");

    await Task.Delay(TimeSpan.FromMilliseconds(8_000));
}


Console.WriteLine("-------------");

