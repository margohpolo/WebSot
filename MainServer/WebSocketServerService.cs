
using Fleck;
using System.Text;

namespace MainServer
{
    public class WebSocketServerService
    {
        private readonly WebSocketServer _server;
        private List<IWebSocketConnection> _connections;

        public bool HasOpenConnections => _server is not null && _connections.Count > 0;

        public WebSocketServerService(string fullAddress)
        {
            _server = new WebSocketServer(fullAddress);
            _connections = new List<IWebSocketConnection>();

            Start();
        }

        public void Start()
        {
            _server.Start(wsConn =>
            {
                wsConn.OnOpen = () => SocketOnOpen(wsConn);

                wsConn.OnPing = (byte[] pingMessage) => SocketOnPing(pingMessage);

                wsConn.OnPong = (byte[] pongMessage) => SocketOnPong(pongMessage);

                wsConn.OnMessage = (string message) => SocketOnMessage(message);

                wsConn.OnError = (Exception ex) => SocketOnException(ex);

                wsConn.OnClose = () => SocketOnClose(wsConn);

                WriteLog($"WS Connection Established with: {wsConn.ConnectionInfo.Origin}");
            });
        }

        public void StopServer()
        {
            _connections.Clear();
            _server.ListenerSocket.Close();
            _server.Dispose();
        }

        public bool SendOnWebSocket(string message)
        {
            bool result = true;

            foreach (var wsConn in _connections)
            {
                StringBuilder _sb = new StringBuilder();

                _sb.Append($"Sent to {wsConn.ConnectionInfo.Origin} ");

                Task sendMsgTask = wsConn.Send(message); //would it make sense to run async?

                if (sendMsgTask.IsCompletedSuccessfully)
                {
                    _sb.Append("successfully.");
                }
                else
                {
                    _sb.Append("unsuccessfully.");
                    result = false;
                }

                WriteLog(_sb.ToString());
            }

            return result;
        }

        private void SocketOnOpen(IWebSocketConnection conn)
        {
            _connections.Add(conn);
            WriteLog($"WS Opened: {conn.ConnectionInfo.Path}");

        }

        private void SocketOnPing(byte[] message)
        {
            WriteLog($"ping {Encoding.UTF8.GetString(message)}");
        }

        private void SocketOnPong(byte[] message)
        {
            WriteLog($"pong {Encoding.UTF8.GetString(message)}");
        }

        private void SocketOnMessage(string message)
        {
            _connections.ForEach(ws => ws.Send(message));
            WriteLog($"Sent Message: {message}");
        }

        private void SocketOnException(Exception ex)
        {
            WriteLog($"Exception encountered: {ex.InnerException}");
        }

        private void SocketOnClose(IWebSocketConnection conn)
        {
            _connections.Remove(conn);
            WriteLog($"WS Server Closed: {conn.ConnectionInfo.Path}");
        }

        private static void WriteLog(string log)
        {
            Console.WriteLine($"[{DateTime.Now}] {log}");
        }

    }
}
