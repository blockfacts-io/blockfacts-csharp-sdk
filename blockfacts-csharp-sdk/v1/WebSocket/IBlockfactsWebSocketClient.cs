using blockfacts_csharp_sdk.v1.Models;
using System;
using System.Collections.Generic;
using System.Text;
using WebSocketSharp;

namespace blockfacts_csharp_sdk.v1.WebSocket
{
    public interface IBlockfactsWebSocketClient
    {
        void Connect();
        void SubscribeRaw(string subscribeMessage);
        void UnsubscribeRaw(string unsubscribeMessage);
        void OnOpen(EventHandler onOpenHandler);
        void OnMessage(EventHandler<MessageEventArgs> onMessageHandler);
        void OnClose(EventHandler<CloseEventArgs> onCloseHandler);
        void OnError(EventHandler<ErrorEventArgs> onErrorHandler);
        void Subscribe(List<BlockfactsChannelObject> channels, bool snapshot, string id);
        void Unsubscribe(List<BlockfactsChannelObject> channels);
        void Ping();
        void Pong();
    }
}
