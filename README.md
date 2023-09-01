# WebSot

A WebSocket exploration in .Net.

For a quick check in the browser client, JavaScript can be used the browser console:

```
let ws = new WebSocket(<Address>);
ws.onmessage = message => console.log(`${message.data}`);
ws.send(<MessageToSend>);
```

Quick shoutout to Hussein Nasser & this [Great Udemy](https://www.udemy.com/course/fundamentals-of-backend-communications-and-protocols/) of his.

#### Moving Forward
- Debugging the connection check in Client
- Usage of WebSocketBehavior class