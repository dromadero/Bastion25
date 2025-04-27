// See https://aka.ms/new-console-template for more information
using Bastion.Common;

Console.WriteLine("Hello, World!");

AppLoger logger = new AppLoger("rolo", new HttpClient(), "http://192.168.1.225:8090");
await logger.Log("tak  wkoncu sie udało :)))))!!!!!! jou jou ou");

Console.WriteLine("Hello, World!");
Console.WriteLine("Hello, World!");