using blockfacts_csharp_sdk.v1.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using WebSocketSharp;

namespace blockfacts_csharp_sdk.v1.WebSocket
{
    public class BlockfactsWebSocketClient : IBlockfactsWebSocketClient
    {
        public WebSocketSharp.WebSocket ws;
        private string ApiKey;
        private string ApiSecret;
        public BlockfactsWebSocketClient(string key = "api-key-not-provided", string secret = "api-secret-not-provided")
        {
            ws = new WebSocketSharp.WebSocket("wss://ws.blockfacts.io/v1/");
            this.ApiKey = key;
            this.ApiSecret = secret;
        }

        /// <summary>
        /// Handles OnOpen event.
        /// </summary>
        /// <param name="onOpenHandler">On open event handler</param>
        public void OnOpen(EventHandler onOpenHandler)
        {
            ws.OnOpen += onOpenHandler;
        }

        /// <summary>
        /// Handles OnMessage event.
        /// </summary>
        /// <param name="onMessageHandler">On message event handler</param>
        public void OnMessage(EventHandler<MessageEventArgs> onMessageHandler)
        {
            ws.OnMessage += onMessageHandler;
        }

        /// <summary>
        /// Handles OnClose event.
        /// </summary>
        /// <param name="onCloseHandler">On close event handler</param>
        public void OnClose(EventHandler<CloseEventArgs> onCloseHandler)
        {
            ws.OnClose += onCloseHandler;
        }

        /// <summary>
        /// Handles OnError event.
        /// </summary>
        /// <param name="onErrorHandler">On error event handler</param>
        public void OnError(EventHandler<ErrorEventArgs> onErrorHandler)
        {
            ws.OnError += onErrorHandler;
        }

        /// <summary>
        /// Connects to BlockFacts websocket server.
        /// </summary>
        public void Connect()
        {
            ws.Connect();
        }

        /// <summary>
        /// Subscribe method used for subscribing to BlockFacts real-time crypto data stream.
        /// </summary>
        /// <param name="channels">List of BlockfactsChannelObjects to subscribe to</param>
        public void Subscribe(List<BlockfactsChannelObject> channels, bool snapshot = false, string id = "")
        {
            BlockfactsSubscribeMessage subscribeMessage = new BlockfactsSubscribeMessage();

            subscribeMessage.type = "subscribe";
            subscribeMessage.snapshot = snapshot;
            subscribeMessage.id = id == null ? "" : id;
            subscribeMessage.X_API_KEY = this.ApiKey;
            subscribeMessage.X_API_SECRET = this.ApiSecret;
            subscribeMessage.channels = channels;
            
            string subscribeMsg = JsonConvert.SerializeObject(subscribeMessage, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }).Replace("X_API_KEY", "X-API-KEY").Replace("X_API_SECRET", "X-API-SECRET").Replace("exchangeName", "name");
            ws.Send(subscribeMsg);
        }

        /// <summary>
        /// Raw subscribe method used for subscribing to BlockFacts real-time crypto data stream.
        /// Reference: Reference: https://docs.blockfacts.io/#subscribe
        /// </summary>
        /// <param name="subscribeMessage">Json string containing the subscribe message</param>
        public void SubscribeRaw(string subscribeMessage)
        {
            ws.Send(subscribeMessage);
        }

        /// <summary>
        /// Unsubscribe method used to unsubscribe from certain channels or pairs.
        /// </summary>
        /// <param name="channels">List of BlockfactsChannelObjects to unsubscribe from</param>
        public void Unsubscribe(List<BlockfactsChannelObject> channels)
        {
            BlockfactsSubscribeMessage unsubscribeMessage = new BlockfactsSubscribeMessage();

            unsubscribeMessage.type = "unsubscribe";
            unsubscribeMessage.channels = channels;

            string unsubscribeMsg = JsonConvert.SerializeObject(unsubscribeMessage, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }).Replace("exchangeName", "name");
            ws.Send(unsubscribeMsg);
        }

        /// <summary>
        /// Raw unsubscribe method used to unsubscribe from certain channels or pairs.
        /// Reference: https://docs.blockfacts.io/#unsubscribe
        /// </summary>
        /// <param name="unsubscribeMessage">Json string containing the unsubscribe message</param>
        public void UnsubscribeRaw(string unsubscribeMessage)
        {
            ws.Send(unsubscribeMessage);
        }

        /// <summary>
        /// Sends a ping type message to the server to determine if the server is online.
        /// Reference: https://docs.blockfacts.io/#ping
        /// </summary>
        public void Ping()
        {
            JObject ping = new JObject();
            ping["type"] = "ping";
            ws.Send(JsonConvert.SerializeObject(ping));
        }

        /// <summary>
        /// Sends a pong type message to the server to let the server know that the client is still connected.
        /// Reference: https://docs.blockfacts.io/#pong
        /// </summary>
        public void Pong()
        {
            JObject pong = new JObject();
            pong["type"] = "pong";
            ws.Send(JsonConvert.SerializeObject(pong));
        }
    }
}
