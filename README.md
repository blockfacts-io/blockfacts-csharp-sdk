![alt text](https://blockfacts.io/img/logo/bf-logo@2x.png "BlockFacts official logo")
# BlockFacts C# / .NET SDK
Official BlockFacts C#/.NET SDK including Rest and WebSocket API support.

[![NuGet version](https://badge.fury.io/nu/BlockFactsSDK.svg)](https://badge.fury.io/nu/BlockFactsSDK)

## Features

- REST API client with function wrapper for easy API access
- WebSocket API client for real-time data gathering

* [Installation](#installation)
* [Getting started](#getting-started)
* [Using Rest API Client](#using-rest-api-client)
* [Asset endpoints](#asset-endpoints)
* [BlockFacts endpoints](#blockfacts-endpoints)
* [Exchange endpoints](#exchange-endpoints)
* [Using WebSocket API Client](#using-websocket-api-client)

## Installation
For .NET Universal
```bash
$ Install-Package BlockFactsSDK
```

For .NET Core
```bash
$ Install-Package BlockFactsSDK.Core
```

## Getting started
```csharp
string key = "your-api-key";
string secret = "your-api-secret";
var restClient = new BlockfactsRestClient(key, secret);
var wsClient = new BlockfactsWebSocketClient(key, secret);

// Test one endpoint
var response = restClient.Assets.GetSpecificAsset("BTC").Result;
Console.WriteLine(JsonConvert.SerializeObject(response));
```

## Using Rest API Client

In the examples below, you can see which method is mapped to call its predefined endpoint. You can also read more about authorization and how to obtain an API Key here: https://docs.blockfacts.io/#authorization

**Note**: All methods are asynchronous and need to be awaited. If you are not located in an async function, you can use .Result at the end of the method call.

Also many methods return Blockfacts models, just so you don't have to map the JSON response yourself. You can find out more about which models are supported in the *Models* folder.

## Asset endpoints

### List all assets
Get all assets that we support.
- [`List<BlockfactsAssetModel> ListAllAssets()`](https://docs.blockfacts.io/#list-all-assets)

```csharp
var response = await restClient.Assets.ListAllAssets();

// OR

var response = restClient.Assets.ListAllAssets().Result;
```

### Get specific asset
Get specific asset by ticker ID.
- [`BlockfactsAssetModel GetSpecificAsset(string tickerId)`](https://docs.blockfacts.io/#specific-asset)

```csharp
var response = await restClient.Assets.GetSpecificAsset("BTC");

// OR

var response = restClient.Assets.GetSpecificAsset("BTC").Result;
```

## BlockFacts endpoints

### Exchanges in normalization
List exchanges that go into the normalization for specific asset-denominator pair.
- [`JObject GetExchangesInNormalization(string pairs)`](https://docs.blockfacts.io/#exchanges-in-normalization)

```csharp
var response = await restClient.Blockfacts.GetExchangesInNormalization("BTC-USD, ETH-USD, BTC-GBP");

// OR

var response = restClient.Blockfacts.GetExchangesInNormalization("BTC-USD, ETH-USD, BTC-GBP").Result;

Console.WriteLine(response["ETH-USD"]["asset"]);
```

### Current data
Get current normalization data for specific asset-denominator pair.
- [`JObject GetCurrentData(string assets, string denominators)`](https://docs.blockfacts.io/#current-data)

```csharp
var response = await restClient.Blockfacts.GetCurrentData("BTC, ETH", "USD, GBP");

// OR

var response = restClient.Blockfacts.GetCurrentData("BTC, ETH", "USD, GBP").Result;

Console.WriteLine(response["BTC-USD"]);
```

### Historical data
Get historical normalization data by asset-denominator, date, time and interval.
- [`BlockfactsHistoricalNormalizationResultsModel GetHistoricalData(string asset, string denominator, string date, string time, int interval, int page)`](https://docs.blockfacts.io/#historical-data)

```csharp
var response = await restClient.Blockfacts.GetHistoricalData("BTC", "USD", "2.9.2019", "14:00:00", 20, 1);

// OR

var response = restClient.Blockfacts.GetHistoricalData("BTC", "USD", "2.9.2019", "14:00:00", 20, 1).Result;
```

### Specific historical data
Get historical normalized price by specific point in time.
- [`BlockfactsNormalizationModel GetSpecificHistoricalData(string asset, string denominator, string date, string time)`](https://docs.blockfacts.io/#specific-historical-data)

```csharp
var response = await restClient.Blockfacts.GetSpecificHistoricalData("BTC", "USD", "12.9.2019", "14:00:00");

// OR

var response = restClient.Blockfacts.GetSpecificHistoricalData("BTC", "USD", "12.9.2019", "14:00:00").Result;
```

### Normalization pairs
Get all running normalization pairs. Resulting in which asset-denominator pairs are currently being normalized inside our internal system.
- [`List<BlockfactsRunningNormalizationPairsTradesModel> GetNormalizationPairs()`](https://docs.blockfacts.io/#normalization-pairs)

```csharp
var response = await restClient.Blockfacts.GetNormalizationPairs();

// OR

var response = restClient.Blockfacts.GetNormalizationPairs().Result;
```

### End of day data
Get normalized end of day data for specific asset-denominator.
- [`List<BlockfactsEndOfDayModel> GetEndOfDayData(string asset, string denominator, int length)`](https://docs.blockfacts.io/#end-of-day-data)

```csharp
var response = await restClient.Blockfacts.GetEndOfDayData("BTC", "USD", 2);

// OR

var response = restClient.Blockfacts.GetEndOfDayData("BTC", "USD", 2).Result;
```

## Exchange endpoints

### List all exchanges
List all exchanges that we support.
- [`List<BlockfactsExchangeDataModel>> ListAllExchanges()`](https://docs.blockfacts.io/#all-exchanges)

```csharp
var response = await restClient.Exchanges.ListAllExchanges();

// OR

var response = restClient.Exchanges.ListAllExchanges().Result;
```

### Specific exchange data
Get information about a specific exchange by its name. Returns information such as which assets are supported, asset ticker info, etc.
- [`BlockfactsExchangeDataModel GetSpecificExchangeData(string exchange)`](https://docs.blockfacts.io/#specific-exchange-data)

```csharp
var response = await restClient.Exchanges.GetSpecificExchangeData("KRAKEN");

// OR

var response = restClient.Exchanges.GetSpecificExchangeData("KRAKEN").Result;
```

### Current trade data
Get current trade data for specific asset-denominator pair, from specific exchange(s).
- [`JObject GetCurrentTradeData(string assets, string denominators, string exchanges)`](https://docs.blockfacts.io/#current-trade-data)

```csharp
var response = await restClient.Exchanges.GetCurrentTradeData("BTC, ETH", "USD, GBP", "COINBASE, KRAKEN");

// OR

var response = restClient.Exchanges.GetCurrentTradeData("BTC, ETH", "USD, GBP", "COINBASE, KRAKEN").Result;

Console.WriteLine(response["BTC-USD"][0]["pair"]);
```

### Historical trade data
Get exchange historical price by asset-denominator, exchange, date, time and interval.
- [`BlockfactsHistoricalExchangeTradesModel GetHistoricalTradeData(string asset, string denominator, string exchanges, string date, string time, int interval, int page)`](https://docs.blockfacts.io/#historical-trade-data)

```csharp
var response = await restClient.Exchanges.GetHistoricalTradeData("BTC", "USD", "KRAKEN, COINBASE", "2.9.2019", "14:00:00", 1);

// OR

var response = restClient.Exchanges.GetHistoricalTradeData("BTC", "USD", "KRAKEN, COINBASE", "2.9.2019", "14:00:00", 1).Result;
```

### Specific trade data
Get historical exchange trades in specific second.
- [`List<BlockfactsTradeModel> GetSpecificTradeData(string asset, string denominator, string exchanges, string date, string time)`](https://docs.blockfacts.io/#specific-trade-data)

```csharp
var response = await restClient.Exchanges.GetSpecificTradeData("BTC", "USD", "KRAKEN, COINBASE", "2.9.2019", "14:00:00");

// OR

var response = restClient.Exchanges.GetSpecificTradeData("BTC", "USD", "KRAKEN, COINBASE", "2.9.2019", "14:00:00").Result;
```

### End of day data
Get exchange end of day data for specific asset-denominator and exchange
- [`List<BlockfactsEndOfDayModel> GetEndOfDayData(string asset, string denominator, string exchange, int length)`](https://docs.blockfacts.io/#end-of-day-data-2)

```csharp
var response = await restClient.Exchanges.GetEndOfDayData("BTC", "USD", "KRAKEN", 1);

// OR

var response = restClient.Exchanges.GetEndOfDayData("BTC", "USD", "KRAKEN", 1).Result;
```

## Using WebSocket API Client
Our WebSocket feed provides real-time market data streams from multiple exchanges at once and the BlockFacts normalized price stream for each second. The WebSocket feed uses a bidirectional protocol, and all messages sent and received via websockets are encoded in a `JSON` format.

### Getting started and connecting
To get started simply create a new instance of the WebSocket class, and create a list of BlockfactsChannelObjects. 
We will use this list in order to subscribe to specific channels.

```csharp
var wsClient = new BlockfactsWebSocketClient(key, secret);
            
List<BlockfactsChannelObject> channelObjects = new List<BlockfactsChannelObject>();
channelObjects.Add(new BlockfactsChannelObject("BLOCKFACTS", new List<string>() { "BTC-USD" }));
channelObjects.Add(new BlockfactsChannelObject("COINBASE", new List<string>() { "BTC-USD", "ETH-USD" }));
channelObjects.Add(new BlockfactsChannelObject("HEARTBEAT", null));

wsClient.OnOpen(OnOpen);
wsClient.OnMessage(OnMessage);
wsClient.OnClose(OnClose);
wsClient.OnError(OnError);

wsClient.Connect();
```

Firstly, create `OnOpen` and `OnMessage` function handlers. You can also handle websocket connection close and error events with `OnClose` and `OnError`.

```csharp
private static void OnOpen(object sender, EventArgs e)
{
    Console.WriteLine("Connection open!");
}

private static void OnMessage(object sender, MessageEventArgs e)
{
    var json = JsonConvert.DeserializeObject<dynamic>(e.Data);
    Console.WriteLine(json);

    if (json.type == "ping")
    {
        // Send Pong message
    }

    if (json.type == "blockfactsPrice")
    {
        // Handle blockfactsPrice
    }

    if (json.type == "exchangeTrade")
    {
        // Handle exchangeTrade
    }

    if (json.type == "unsubscribed")
    {
        // Handle unsubscribed
    }

    if (json.type == "heartbeat")
    {
        // Handle heartbeat
    }

    if (json.type == "error")
    {
        // Handle error
    }
}

private static void OnClose(object sender, CloseEventArgs e)
{
    Console.WriteLine("Connection closed.");
}

private static void OnError(object sender, ErrorEventArgs e)
{
    Console.WriteLine("Error! "+ e.Message);
}
```

### Subscribing
In order to subscribe to a specific channel or asset-pair you must send out a `subscribe` type message. Our SDK allows you to do just that with the following code example: 

```csharp
wsClient.Subscribe(channelObjects); // List<BlockfactsChannelObject> we created earlier
```

### Unsubscribing
If you wish to unsubscribe from certain channels or pairs, you can do so by sending the `unsubscribe` type message.

```csharp
wsClient.Unsubscribe(channelObjects);
```

### Ping
Clients can send `ping` type messages to determine if the server is online.

```csharp
wsClient.Ping();
```

### Pong
Clients must respond to `ping` type messages sent from the server with a `pong` type message.

```csharp
wsClient.Pong();
```

In order to have a better understanding of our server responses, please refer to: https://docs.blockfacts.io/#server-messages