using Newtonsoft.Json;

var currentDirectory = Directory.GetCurrentDirectory();
var storesDirectory = Path.Combine(currentDirectory, "stores");
var salesToDir = Path.Combine(currentDirectory, "salesTotalDir");
Directory.CreateDirectory(salesToDir);

var salesFiles = FindFiles(storesDirectory);

var salesTotal = CalculateSalesTotal(salesFiles);

File.AppendAllText(Path.Combine(salesToDir, "totals.txt"), $"{salesTotal}{Environment.NewLine}");

foreach (var file in salesFiles)
{
    Console.WriteLine(file);
}

IEnumerable<string> FindFiles(string folderName)
{
    List<string> files = new List<string>();
    var foundFiles = Directory.EnumerateFiles("stores", "*.json", SearchOption.AllDirectories);
    foreach (var item in foundFiles)
    {
        var extention = Path.GetExtension(item);
        if (extention == ".json")
        {
            files.Add(item);
        }
    }
    return files;
}

double CalculateSalesTotal(IEnumerable<string> salesFiles)
{
    double salesTotal = 0;

    foreach (var file in salesFiles)
    {
        string salesJson = File.ReadAllText(file);
        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);
        salesTotal += data?.Total ?? 0;
    }

    return salesTotal;
}

record SalesData(double Total);
