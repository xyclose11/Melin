import * as signalR from "@microsoft/signalr";
const URL = "https://localhost:7120/document"; //or whatever your backend port is
class Connector {
    private connection: signalR.HubConnection;
    public events: (
        onMessageReceived: (username: string, message: string) => void,
    ) => void;
    static instance: Connector;
    constructor() {
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(URL)
            .withAutomaticReconnect()
            .build();
        this.connection.start().catch((err) => document.write(err));
        this.events = (onMessageReceived) => {
            this.connection.on("messageReceived", (username, message) => {
                onMessageReceived(username, message);
            });
        };
    }
    public newMessage = (messages: string) => {
        this.connection
            .send("NewMessage", "foo", messages)
            .then((x) => console.log("sent"));
    };
    public static getInstance(): Connector {
        if (!Connector.instance) Connector.instance = new Connector();
        return Connector.instance;
    }
}
export default Connector.getInstance;
