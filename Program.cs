using System.Text;
using System.Text.Json;

class Program
{
    static async Task Main()
    {
        var apiKey = "<your-api-key-here>";

        var url = "https://staging-api.onrout.com/shipper/graphql";

        var query = """
          mutation {
            createShipment(
              input: {
                reserveCarrierId: "bc6926e9-d339-4de3-94a7-70a3a2d7935a"
                allowedCarrierIds: [
                  "99737bb2-0cf4-4947-a123-3ec7084fc1f2"
                ]
                auction: { mode: SYNC, length: { amount: 30, unit: SECONDS } }
                labelFormat: ZPL
                reservePrice: "2000.00"
                reservePriceAdjustment: "0"
                pickupWindow: {
                  start: "2024-10-10T12:00:00Z"
                  end: "2024-10-20T12:00:00Z"
                }
                deliveryWindow: { end: "2024-10-28T12:00:00Z" }
                origin: {
                  name: "Origin"
                  phone: "22125553223"
                  street1: "ABERCORN"
                  city: "SAVANNAH"
                  state: "GA"
                  postalCode: "31405"
                  country: "US"
                }
                destination: {
                  name: "Destination"
                  phone: "22125553223"
                  street1: "9936 INTERNATIONAL BLVD"
                  street2: "DOCK 5"
                  city: "CINCINNATI"
                  state: "OH"
                  postalCode: "45246"
                  country: "US"
                }
                parcels: [
                  {
                    weight: 1
                    massUnit: LB
                    length: 4.0
                    width: 5
                    height: 8
                    distanceUnit: IN
                    declaredValue: "0.00"
                  }
                ]
              }
            )
              {
                id
                auction {
                  result {
                    carrier {
                      id
                      name
                    }
                    price
                    labelUrl
                    reserve
                    shipperMessage
                  }
                  shippingLabels {
                    accountNumber
                    base64Data
                    carrier {
                      id
                    }
                    format
                    parcelId
                    serviceDescription
                    serviceType
                    sortCode
                  }
                }
              }
            }
        """;

        var requestBody = new { query };

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
        var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

        var response = await client.PostAsync(url, content);
        var responseString = await response.Content.ReadAsStringAsync();

        Console.WriteLine(responseString);
    }
}
