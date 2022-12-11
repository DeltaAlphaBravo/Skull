import { createGameAsync, getGameAsync, selectPhase, selectView } from "./game-slice";
import { useAppDispatch, useAppSelector } from "../../app/hooks";
import { selectIsConnected, ensureSignalRConnectionAsync, subscribeToTableAsync } from "../signalr/signalr-slice";
import { SignalRService } from "../signalr/signalr-service";
import { selectPlayers, selectTableName } from "../table/table-slice";
import { PlayerView } from "./playerView/player-view";
import { selectId } from "../localPlayer/local-player-slice";
import { Player } from "../table/player";

export function Game(props: { signalrService: SignalRService }): JSX.Element {
    const signalrService = props.signalrService
    const isSignalRConnected = useAppSelector(selectIsConnected);
    const dispatch = useAppDispatch();
    const tableName = useAppSelector(selectTableName); 
    const phase = useAppSelector(selectPhase);
    const playerNumber = useAppSelector(selectId);
    const view = useAppSelector(selectView);
    const players = useAppSelector(selectPlayers);

    function StartGame() {
        dispatch(createGameAsync(tableName ?? ""));
        if(!tableName || !playerNumber) return;
        dispatch(getGameAsync({tableName, playerNumber}));
    }

    return (
        <div>
            <div>{tableName}</div>
            <ul>
                {players?.map((player) =>
                    <Player key={player.playerId} value={player} />
                )}
            </ul>
            <button onClick={StartGame} 
                    hidden = { (tableName === null || tableName === undefined)}>
                Start Game
            </button>
            <PlayerView view={view!}/>
        </div>
    );
}