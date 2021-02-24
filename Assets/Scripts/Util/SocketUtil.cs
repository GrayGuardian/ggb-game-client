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
        if (_onListenEvent != null) _onListenEvent("OnOpen", null);
        _socket.On("aaa", (info) =>
        {
            UnityEngine.Debug.Log("返回aaa的值>>>");
            UnityEngine.Debug.Log(info.Data);

        });
    }
    private void OnConnectFailed()
    {
        if (_onListenEvent != null) _onListenEvent("OnConnectFailed", null);
    }
    private void OnClose()
    {
        if (_onListenEvent != null) _onListenEvent("OnClose", null);
    }
    private void OnError(string error)
    {
        if (_onListenEvent != null) _onListenEvent("OnError", new object[] { error });
    }
    public SocketIO Conn(string url)
    {
        _socket = new SocketIO("ws://" + url + "/socket.io/?EIO=4&transport=websocket");

        _socket.OnOpen += OnOpen;
        _socket.OnConnectFailed += OnConnectFailed;
        _socket.OnClose += OnClose;
        _socket.OnError += (error) => { OnError(error.ToString()); };

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