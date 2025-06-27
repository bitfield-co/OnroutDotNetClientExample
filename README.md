# Onrout C# sample client

Running:
1) Add your API key at the top of the file
2) Run the script:
  * `./run.sh` (*nix)
  * `run.bat` Windows

## Usage

```csharp
var apiKey = "<your-api-key-here>";

var url = "https://staging-api.onrout.com/shipper/graphql";

var requestBody = new { query = "..." };

using var client = new HttpClient();
client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

var response = await client.PostAsync(url, content);
var responseString = await response.Content.ReadAsStringAsync();

Console.WriteLine(responseString);
```
