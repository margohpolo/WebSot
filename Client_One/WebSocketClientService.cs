using Newtonsoft.Json;
using System.Text;
using WebSocketSharp;

namespace Client_One
{
    public class WebSocketClientService
    {
        private WebSocket _webSocket;
        private bool isClientConnected = false;

        public WebSocketClientService(string address)
        {
            CreateNewClient(address);
            ConnectClient();
        }

        public void CreateNewClient(string address)
        {
            _webSocket = new WebSocket(address);

            _webSocket.EmitOnPing = true;
            _webSocket.Origin = "ConsoleClient";
            _webSocket.OnOpen += OnOpenBehavior;
            _webSocket.OnMessage += OnMessageBehavior;
            _webSocket.OnError += OnErrorBehavior;
        }

        public void ConnectClient()
        {
            WriteLog("Attempting to connect...");
            _webSocket.Connect();
            _webSocket.Ping();
            //isClientConnected = _webSocket.IsAlive; //TODO: Need to debug this check further. But it can connect.

            isClientConnected = true; //TODO: this ia a temp workaround

            if (isClientConnected)
            {
                WriteLog($"Connected to {_webSocket.Url}");
            }
            else
            {
                WriteLog("Failed to connect.");
            }
        }

        public bool CloseClient()
        {
            bool result = false;

            return result;
        }

        private void OnOpenBehavior(object? sender, EventArgs e)
        {
            WriteLog("Connection opened.");
        }

        private void OnMessageBehavior(object? sender, EventArgs e)
        {
            WriteLog($"Received from server: {JsonConvert.SerializeObject(e)}");
        }

        private void OnErrorBehavior(object? sender, EventArgs e)
        {
            WriteLog($"Sent to Server: {JsonConvert.SerializeObject(e)}");
        }

        public void SendToServer(string message)
        {
            _webSocket.Send(message);
        }


        private void WriteLog(string log)
        {
            Console.WriteLine($"[{DateTime.Now}] {log}");
        }
    }
}
