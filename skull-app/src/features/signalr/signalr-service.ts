import { HttpTransportType, HubConnection, HubConnectionBuilder, IHttpConnectionOptions, JsonHubProtocol, LogLevel } from "@microsoft/signalr";

export class SignalRService {
    private _connection: HubConnection | null = null;
    private _tableSubscription: string | null = null;
    private _connected: boolean = false;

    public get isConnected(): boolean {
        return this._connected;
    }

    public async startConnection(): Promise<boolean> {
        if(this._connection === null){
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

            // re-establish the connection if connection dropped
            this._connection.onclose(() =>
                setTimeout(() => this.startSignalRConnection(this._connection!), 5000)
            );

            return await this.startSignalRConnection(this._connection);
        }
        return new Promise<boolean>(function(resolve) {resolve(true)});
    }

    public OnPlayerJoin(
        otherPlayerJoined: (name: string, id: number) => void,
        ): void {
        if(!this._connection) throw("Not connected to SignalR");
        this._connection.on("ReceiveNewPlayer", otherPlayerJoined);
    }

    public OnGameEvents(
        onStart: () => void,
        onCardPlaced: (id: number) => void, 
        onBid: (id: number, bid: number) => void
    ){
        if(!this._connection) throw("Not connected to SignalR");
        this._connection.on("ReceiveNewPlacement", onCardPlaced);
        this._connection.on("ReceiveNewBid", onBid);
        this._connection.on("ReceiveGameStart", onStart);
    }

    public async subscribeTo(table: string, player: number): Promise<string | null> {
        if(this._connection == null) return Promise.resolve(null);
        let result = null;
        await this._connection.invoke("SubscribeToNotificationsAsync", table, player)
        .then(() => {
            console.log("joined ", table);
            this._tableSubscription = table;
            result = table;
        })
        .catch((reason) => console.log(reason));
        return result;
    }

    public get subscribedTable(): string | null {
        return this._tableSubscription;
    }

    public get connectionState(): string| null {return this._connection?.state ?? null;}

    private startSignalRConnection = async (connection: HubConnection) =>
        await connection
            .start()
            .then(() => {
                console.info('SignalR Connected');
                this._connected = true;
                return true;
            })
            .catch((err: any) => {
                console.error('SignalR Connection Error: ', err);
                return false;
            });
}