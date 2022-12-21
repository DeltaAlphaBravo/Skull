import { createGameAsync, getGameAsync, selectPhase, selectView, showCardPlayed } from "./game-slice";
import { useAppDispatch, useAppSelector } from "../../app/hooks";
import { SignalRService } from "../signalr/signalr-service";
import { getTableAsync, selectPlayers, selectTableName } from "../table/table-slice";
import { PlayerView } from "./playerView/player-view";
import { selectId } from "../localPlayer/local-player-slice";
import { Player } from "../table/player";
import './game.css';
import { useParams } from "react-router-dom";
import { ensureSignalRConnectionAsync, selectIsConnected } from "../signalr/signalr-slice";
import { useEffect } from "react";

export function Game(props: { signalrService: SignalRService }): JSX.Element {
    const signalrService = props.signalrService;
    const dispatch = useAppDispatch();
    const tableName = useAppSelector(selectTableName); 
    const phase = useAppSelector(selectPhase);
    const playerNumber = useAppSelector(selectId);
    const view = useAppSelector(selectView);
    const players = useAppSelector(selectPlayers);
    const routerParams = useParams();
    const isSignalRConnected = useAppSelector(selectIsConnected);

    function StartGame() {
        if(!tableName || playerNumber === null) throw('tableName = ' + tableName + ' playerNumber = ' + playerNumber);
        dispatch(createGameAsync(tableName ?? ""))
        .then(() => dispatch(getGameAsync({tableName, playerNumber})));
    }

    useEffect(()=> {
        if(routerParams.table === undefined) throw("Can't render game, table undefined.");
        dispatch(getTableAsync(routerParams.table))
        .catch(() => alert('Did not find ' + routerParams.table))
    }, [routerParams.table]);

    useEffect(() => {
        dispatch(getGameAsync({tableName: routerParams.table ?? "", playerNumber: playerNumber ?? -1}))
        .catch(() => alert('Error! did not subscribe'));
    }, [routerParams.table, playerNumber]);

    useEffect(() => {
        if(signalrService.subscribedTable !== routerParams.table) {
            dispatch(ensureSignalRConnectionAsync(signalrService))
            .then(
                () => { if(signalrService.isConnected) signalrService.subscribeTo(routerParams.table!, playerNumber!); },
                () => alert("Failed to connect to SignalR"))
            .then(() => signalrService.OnGameEvents(
                        () => {
                            if(!tableName || playerNumber === null) throw('tableName = ' + tableName + ' playerNumber = ' + playerNumber);
                            dispatch(getGameAsync({tableName, playerNumber}))},
                        (id) => {
                            if (tableName === null || playerNumber === null) throw('tableName = ' + tableName + ' playerNumber = ' + playerNumber);
                            dispatch(getGameAsync({tableName, playerNumber}))},
                        (id, bid) => {}))
            .then(() => signalrService.OnPlayerJoin(() => dispatch(getTableAsync(tableName!))))
            .catch((reason) => alert(reason));
        }        
    }, [routerParams.table, playerNumber, isSignalRConnected]);

    return (
        <div className="game">
            <div className="player-view">
                <button onClick={StartGame} 
                        hidden = { (tableName === null || tableName === undefined) || !!phase }>
                    Start Game
                </button>
                <PlayerView view={view!} tableName={tableName ?? ""}/>
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