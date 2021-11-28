using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Mcv.Plugin.A17Live.Network;

public record MessageReceived(string ReceivedData);
public record RequestConnect(string Url);
public record RequestDisconnect();
public record Disconnected(Exception? Ex);
record ContinueToReceive();


public class WebsocketActor : ReceiveActor
{
    private readonly ClientWebSocket _ws;
    private readonly byte[] _receiveBuffer = new byte[4096];
    private readonly StringBuilder _sb = new StringBuilder();
    private IActorRef? _requester;
    public WebsocketActor()
    {
        _ws = new ClientWebSocket();
        var cts = new CancellationTokenSource();
        ReceiveAsync<RequestConnect>(async m =>
        {
            if (_requester != null)
            {
                return;
            }
            _requester = Sender;
            await _ws.ConnectAsync(new Uri(m.Url), cts.Token);
            //Sender.Tell(new Connected());
            Self.Tell(new ContinueToReceive());
        });
        ReceiveAsync<RequestDisconnect>(async m =>
        {
            try
            {
                await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, null, cts.Token);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            AfterDisconnected();
        });
        ReceiveAsync<ContinueToReceive>(async m =>
        {
            if (_ws.State != WebSocketState.Open || _requester == null)
            {
                return;
            }
            WebSocketReceiveResult result;
            try
            {
                //System.Net.WebSockets.WebSocketException
                result = await _ws.ReceiveAsync(new ArraySegment<byte>(_receiveBuffer), cts.Token);
            }
            catch (Exception ex)
            {
                AfterDisconnected(ex);
                return;
            }
            switch (result.MessageType)
            {
                case WebSocketMessageType.Text:
                    OnReceiveText(result);
                    break;
                case WebSocketMessageType.Binary:
                    break;
                case WebSocketMessageType.Close:
                    Debug.WriteLine($"closed");
                    return;
            }
            Self.Tell(new ContinueToReceive());
        });
        ReceiveAsync<SendMessage>(async m =>
        {
            await _ws.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(m.Text)), WebSocketMessageType.Text, true, CancellationToken.None);
        });
    }
    private void AfterDisconnected(Exception? ex = null)
    {
        if (_requester == null) return;
        _requester.Tell(new Disconnected(ex));
        _requester = null;
    }

    private void OnReceiveText(WebSocketReceiveResult result)
    {
        _sb.Append(Encoding.UTF8.GetString(_receiveBuffer, 0, result.Count));
        if (result.EndOfMessage)
        {
            _requester.Tell(new MessageReceived(_sb.ToString()));
            _sb.Clear();
        }
    }
}
