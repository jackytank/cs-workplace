using Newtonsoft.Json;

var currentDirectory = Directory.GetCurrentDirectory();
var storesDirectory = Path.Combine(currentDirectory, "stores");
var salesToDir = Path.Combine(currentDirectory, "salesTotalDir");
Directory.CreateDirectory(salesToDir);

var salesFiles = FindFiles(storesDirectory);

File.WriteAllText(Path.Combine(salesToDir, "totals.txt"), String.Empty);

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

record SalesData(double Total);
