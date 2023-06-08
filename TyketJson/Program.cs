using System.IO.Compression;
using TyketJson.Models;
using Newtonsoft.Json;

public class Program
{
    private static async Task download_and_decompress(string url, string path)
    {
        using (HttpClient client = new HttpClient())
        {
            using (HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();

                using (Stream gzipStream = await response.Content.ReadAsStreamAsync())
                using (Stream decompressedStream = new GZipStream(gzipStream, CompressionMode.Decompress))
                using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    await decompressedStream.CopyToAsync(fileStream);
                }
            }
        }
    }

    public static async Task Main()
    {
        string url = "https://staging.media.tyket.app/dotnet-test-data.json.gz";
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dotnet-test-data.json");

        await download_and_decompress(url, path);

        string text = File.ReadAllText(path);

        var data = JsonConvert.DeserializeObject<Person[]>(text);

        Console.WriteLine("Total items: " + data.Count());

        Console.WriteLine();

        Console.WriteLine("Total items in Florida: " + data.Count(item => item.address.state_name == "Florida"));

        Console.WriteLine();

        var highestIncomePerson = data?
            .Where(item => item.address.state_name == "Florida")
            .OrderByDescending(item => item.yearly_income)
            .FirstOrDefault();

        Console.WriteLine("Person with highest income in Florida:");
        Console.WriteLine("ID: " + highestIncomePerson.id);
        Console.WriteLine("Full Name: " + highestIncomePerson.full_name);
        Console.WriteLine("Income: " + highestIncomePerson.yearly_income);

        Console.WriteLine();

        double averageIncome = data
            .Where(item => item.address.state_name == "Florida")
            .Average(item => item.yearly_income);
        Console.WriteLine("Average income for Florida: " + averageIncome);

        Console.ReadLine();
    }
}
