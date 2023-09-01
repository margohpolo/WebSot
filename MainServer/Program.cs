using MainServer;

WebSocketServerService socketServerService = new WebSocketServerService("ws://0.0.0.0:5000/ws");

string input = string.Empty;

while (input != "exit")
{
    bool isSuccess = false;

    input = Console.ReadLine();
    
    if (!String.IsNullOrEmpty(input) && input != "exit")
    {
        isSuccess = socketServerService.SendOnWebSocket(input);
    }

    Console.WriteLine((isSuccess) ? "sent to all clients!" : "sending failed.");
}
