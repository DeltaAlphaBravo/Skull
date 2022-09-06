import { HttpTransportType, HubConnection, HubConnectionBuilder, IHttpConnectionOptions, JsonHubProtocol, LogLevel } from "@microsoft/signalr";

export class SignalRService {
    private _connection: HubConnection | null = null;

    StartConnection(): Promise<boolean> {
        const connectionHub = "https://localhost:55009";

        const protocol = new JsonHubProtocol();
        // let transport to fall back to to LongPolling if it needs to
        const transport =
            HttpTransportType.WebSockets | HttpTransportType.LongPolling;
        const options: IHttpConnectionOptions = {
            transport,
            logMessageContent: true,
            logger: LogLevel.Critical,
            withCredentials: false,
        };

        // create the connection instance
        this._connection = new HubConnectionBuilder()
            .withUrl(`${connectionHub}/gameHub`, options)
            .withHubProtocol(protocol)
            .build();

        //add "on" events here...
        this._connection.on("ReceiveMessage", (message) => { console.log("happened", message); });

        // re-establish the connection if connection dropped
        this._connection.onclose(() =>
            setTimeout(() => this.startSignalRConnection(this._connection!), 5000)
        );

        return this.startSignalRConnection(this._connection);
    }

    async subscribeTo(game: string): Promise<boolean> {
        if( this._connection == null) return Promise.resolve(false);
        try {
            await this._connection.invoke("AddToGroupAsync", game);
            console.log("joined ", game);
            return true;
        } catch (err) {
            console.log(err);
            return false;
        }
    }

    private startSignalRConnection = (connection: HubConnection) =>
        connection
            .start()
            .then(() => {
                console.info('SignalR Connected');
                return true;
            })
            .catch((err: any) => {
                console.error('SignalR Connection Error: ', err);
                return false;
            });
}