using Parser;

var importer = new Importer("Host=localhost;Username=postgres;Password=postgres;Database=postgres"); 
//await Task.WhenAll(importer.ImportBooks(), importer.ImportUsers());
await importer.GenerateBookInstances();