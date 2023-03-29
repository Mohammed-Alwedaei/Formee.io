var signalRConnection = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/livechat")
    .build();

async function start() {
    try {
        await signalRConnection.start();
    } catch (error) {
        setTimout(start, 5000);
    }
}

signalRConnection.onclose(async () => {
    await start();
});

//Will be called when the message is sent from the server
signalRConnection.on("ReceiveMessage",
    () => {

    });

//Will be called when a message is sending to the server

start();