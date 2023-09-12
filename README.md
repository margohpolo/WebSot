# WebSot

A WebSocket exploration in .NET.

For a quick check in the browser client, JavaScript can be used the browser console:

```
let ws = new WebSocket(<Address>);
```
```
ws.onmessage = message => console.log(`${message.data}`);
```

To send a message via client:
```
ws.send(<MessageToSend>);
```

Quick mention of thanks to Hussein Nasser & this [Great Udemy](https://www.udemy.com/course/fundamentals-of-backend-communications-and-protocols/) of his.

#### Moving Forward
- Debugging the connection check in Client
- Usage of WebSocketBehavior class for Client? (Not sure if feasible)
- .NET Console Client Origin still not printing on Server side -> Can be worked around via Custom Headers. Unfortunately WebSocket-Sharp (Original) does not support it, which is why several forks (such as [this one](https://github.com/felixhao28/websocket-sharp)) have emerged. The full open source drama can be seen [here](https://github.com/sta/websocket-sharp/pull/22).