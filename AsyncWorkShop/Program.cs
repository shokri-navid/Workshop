// See https://aka.ms/new-console-template for more information

using System.Diagnostics;

string strval = "";
int intval = -10;
Console.WriteLine("Hello, World!");
var firstRand = Random.Shared.Next(3000);
var secondRand = Random.Shared.Next(3000);

Console.WriteLine($"firstRand: {firstRand}, secondRand: {secondRand}");

var watch = new Stopwatch();
watch.Start();
try
{
    intval = await FirstFunction(firstRand);
    strval = await SecondFunction(secondRand);
}
catch
{
    // ignored
}
finally
{
    watch.Stop();
    Console.WriteLine($"intval: {intval}, strval: {strval}");
    Console.WriteLine($"Sequential: {watch.ElapsedMilliseconds} ms");
}


watch.Restart();
strval = "";
intval = -10;

watch.Start();
var firstTask = FirstFunction(firstRand);
var secondTask = SecondFunction(secondRand);
try
{
    await Task.WhenAll(firstTask, secondTask);

    strval = secondTask.Result;
    intval = firstTask.Result;
}
catch (Exception e)
{
    Console.WriteLine($"First task failed: {firstTask.Exception} - Second task failed: {secondTask.Exception}");
}
finally
{
    watch.Stop();
    Console.WriteLine($"intval: {intval}, strval: {strval}");
    Console.WriteLine($"Parallel: {watch.ElapsedMilliseconds} ms");
}


async Task<int> FirstFunction(int randomWait)
{
    await Task.Delay(randomWait);
    return Random.Shared.Next();
} 

async Task<string> SecondFunction(int randomWait)
{
    if (randomWait % 2 == 0)
    {
        throw new Exception("even wait is not acceptable");
    }

    string str = "abcdefghijklmnopqrstuwxyz1234567890";
    var randStr = string.Join(string.Empty,Enumerable.Range(0, 6).Select(x=>str[Random.Shared.Next(str.Length-1)]));
    await Task.Delay(randomWait);
    return randStr;
}
