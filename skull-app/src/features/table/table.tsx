import { useAppDispatch, useAppSelector } from "../../app/hooks";
import { selectIsConnected, ensureSignalRConnectionAsync, subscribeToTableAsync } from "../signalr/signalr-slice";
import { SignalRService } from "../signalr/signalr-service";
import { createTableAsync, getTableAsync, joinTableAsync, selectName, selectPlayers } from "./table-slice";
import { Player } from "./player";

export function Table(props: { signalrService: SignalRService }) {
    const signalrService = props.signalrService
    const isSignalRConnected = useAppSelector(selectIsConnected);
    const name = useAppSelector(selectName);
    const players = useAppSelector(selectPlayers);
    const dispatch = useAppDispatch();

    return (
        <div>
            <div>{name}</div>
            <button
                onClick={
                    () => {
                        let tableName: string;
                        dispatch(createTableAsync())
                            .then(async (result) => {
                                console.log(isSignalRConnected);
                                if (!isSignalRConnected) {
                                    await dispatch(ensureSignalRConnectionAsync(signalrService));
                                }
                                tableName = result.payload as string;
                            })
                            .then(() => dispatch(subscribeToTableAsync({ table: tableName, signalRService: signalrService })))
                            .then(() => signalrService.OnPlayerJoin(() => dispatch(getTableAsync(tableName))))
                            .then(() => dispatch(joinTableAsync({ tableName: tableName, playerName: "Bob" })));
                    }
                }
            >
                Create Table
            </button>
            <ul>
                {players?.map((player) =>
                    <Player key={player.playerId} value={player} />
                )}
            </ul>
        </div>
    );
}