// See https://aka.ms/new-console-template for more information

using System.Diagnostics;

Console.WriteLine("Hello, World!");
var firstRand = Random.Shared.Next(3000);
var secondRand = Random.Shared.Next(3000);
var watch = new Stopwatch();
watch.Start();
var intval = await FirstFunction(firstRand);
var strval = await SecondFunction(secondRand);
watch.Stop();
Console.WriteLine($"intval: {intval}, strval: {strval}");
Console.WriteLine($"Sequential: {watch.ElapsedMilliseconds} ms");

watch.Restart();
strval = "";
intval = -10;

watch.Start();
Parallel.Invoke( () => intval =  FirstFunction(firstRand).GetAwaiter().GetResult(),
     () => strval =  SecondFunction(secondRand).GetAwaiter().GetResult());
watch.Stop();
Console.WriteLine($"intval: {intval}, strval: {strval}");
Console.WriteLine($"Parallel: {watch.ElapsedMilliseconds} ms");
async Task<int> FirstFunction(int randomWait)
{
    await Task.Delay(randomWait);
    return Random.Shared.Next();
} 

async Task<string> SecondFunction(int randomWait)
{
    string str = "abcdefghijklmnopqrstuwxyz1234567890";
    var randStr = string.Join(string.Empty,Enumerable.Range(0, 6).Select(x=>str[Random.Shared.Next(str.Length-1)]));
    await Task.Delay(randomWait);
    return randStr;
} 
