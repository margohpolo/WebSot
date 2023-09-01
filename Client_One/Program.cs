// See https://aka.ms/new-console-template for more information

using Client_One;

WebSocketClientService service = new WebSocketClientService("ws://127.0.0.1:5000/ws");

string input = string.Empty;

while (input != "exit")
{
    input = Console.ReadLine();
    if (!String.IsNullOrEmpty(input) && input != "exit")
    {
        service.SendToServer(input);
    }
}