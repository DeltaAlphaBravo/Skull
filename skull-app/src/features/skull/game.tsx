import { createGameAsync, getGameAsync, selectPhase, selectView } from "./game-slice";
import { useAppDispatch, useAppSelector } from "../../app/hooks";
import { selectIsConnected, subscribeToTableAsync } from "../signalr/signalr-slice";
import { SignalRService } from "../signalr/signalr-service";
import { getTableAsync, selectPlayers, selectTableName } from "../table/table-slice";
import { PlayerView } from "./playerView/player-view";
import { selectId } from "../localPlayer/local-player-slice";
import { Player } from "../table/player";
import './game.css';
import { useParams } from "react-router-dom";

export function Game(props: { signalrService: SignalRService }): JSX.Element {
    const signalrService = props.signalrService
    const isSignalRConnected = useAppSelector(selectIsConnected);
    const dispatch = useAppDispatch();
    const tableName = useAppSelector(selectTableName); 
    const phase = useAppSelector(selectPhase);
    const playerNumber = useAppSelector(selectId);
    const view = useAppSelector(selectView);
    const players = useAppSelector(selectPlayers);
    const routerParams = useParams();

    if(!tableName && !!routerParams.table) {
        dispatch(getTableAsync(routerParams.table))
        .then(
            () => {
                if(!isSignalRConnected) 
                    dispatch(subscribeToTableAsync({table: tableName!, signalRService: signalrService }))}, 
            () => alert('Did not find ' + routerParams.table))
        .then(
            () => dispatch(getGameAsync({tableName: routerParams.table ?? "", playerNumber: playerNumber ?? -1})),
            () => alert('Error! did not subscribe'))
        .catch((reason) => alert(reason));
    }

    function StartGame() {
        if(!tableName || !playerNumber) throw('tableName = ' + tableName + ' playerNumber = ' + playerNumber);
        dispatch(createGameAsync(tableName ?? ""))
        .then(
            () => dispatch(getGameAsync({tableName, playerNumber})));
    }

    return (
        <div className="game">
            <div className="player-view">
                <button onClick={StartGame} 
                        hidden = { (tableName === null || tableName === undefined) || !!phase }>
                    Start Game
                </button>
                <PlayerView view={view!}/>
            </div>
            <div className='table-summary'>
                <div>{tableName}</div>
                <ul>
                    {players?.map((player) =>
                        <Player key={player.playerId} value={player} />
                    )}
                </ul>
            </div>
        </div>
    );
}