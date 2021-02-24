using System;
using Dpoch.SocketIO;
public class SocketUtil
{
    private event Action<string, object[]> _onListenEvent = null;
    private SocketIO _socket;
    public SocketUtil()
    {

    }
    public void AddListenEvent(Action<string, object[]> e)
    {
        _onListenEvent += e;
    }
    public void DelListenEvent(Action<string, object[]> e)
    {
        _onListenEvent -= e;
    }
    private void OnOpen()
    {
        if (_onListenEvent != null) _onListenEvent("onConnect", null);
    }
    private void OnConnectFailed()
    {
        if (_onListenEvent != null) _onListenEvent("onConnectError", null);
    }
    private void OnClose()
    {
        if (_onListenEvent != null) _onListenEvent("onDisconnect", null);
    }
    private void OnError(string error)
    {
        if (_onListenEvent != null) _onListenEvent("onError", new object[] { error });
    }
    private void OnEvent(string key,byte[] data){
        if (_onListenEvent != null) _onListenEvent("onMessage", new object[] { key,data });
    }
    public SocketIO Conn(string url)
    {
        _socket = new SocketIO("ws://" + url + "/socket.io/?EIO=4&transport=websocket");

        _socket.OnOpen += OnOpen;
        _socket.OnConnectFailed += OnConnectFailed;
        _socket.OnClose += OnClose;
        _socket.OnError += (error) => { OnError(error.ToString()); };
        _socket.OnEvent += (packet) => { OnEvent(packet.Data.Value<string>(0), packet.Attachments[0]); };
        _socket.Connect();
        return _socket;
    }

    public void Emit(string ev, byte[] data)
    {
        if (_socket == null) return;
        if (!_socket.IsAlive) return;
        _socket.Emit(ev, new object[] { data });
    }

}