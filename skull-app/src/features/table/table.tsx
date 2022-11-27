import { useAppDispatch, useAppSelector } from "../../app/hooks";
import { selectIsConnected, ensureSignalRConnectionAsync, subscribeToTableAsync } from "../signalr/signalr-slice";
import { SignalRService } from "../signalr/signalr-service";
import { createTableAsync, getTableAsync, joinTableAsync, selectTableName, selectPlayers, setTableName } from "./table-slice";
import { Player } from "./player";
import useModal from "../modal/useModal";
import { setName as setPlayerName } from "../localPlayer/local-player-slice";
import StringModal from "../modal/string-modal";
import { useCallback } from "react";

export function Table(props: { signalrService: SignalRService }): JSX.Element {
    const signalrService = props.signalrService
    const isSignalRConnected = useAppSelector(selectIsConnected);
    const tableName = useAppSelector(selectTableName);
    const players = useAppSelector(selectPlayers);
    const dispatch = useAppDispatch();
    const { isShowing: isShowingTable, toggle: toggleTable } = useModal();
    const { isShowing: isShowingPlayer, toggle: toggleName } = useModal();

    const startTable = (name: string) => {
        let tableName: string;
        name = name ?? "The Nameless One";
        dispatch(setPlayerName(name));
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
            .then(() => dispatch(joinTableAsync({ tableName: tableName, playerName: name })));
    }

    const findTable = (name: string) => {
        dispatch(ensureSignalRConnectionAsync(signalrService))
            .then(() => dispatch(subscribeToTableAsync({ table: name, signalRService: signalrService })))
            .then(() => dispatch(getTableAsync(name)))
            .then((response) => {
                if (!response.payload) {
                    toggleTable();
                    alert("Not found");
                } else {
                    signalrService.OnPlayerJoin(() => dispatch(getTableAsync(name)))
                }
            });
        if (tableName) {
            dispatch(setTableName(name));
        }
    }

    const joinTable = (playerName: string, tableName: string) => {
        console.log("here");
        playerName = playerName ?? "The Nameless One";
        dispatch(joinTableAsync({ tableName: tableName, playerName: playerName }))
            .then(() => dispatch(setPlayerName(playerName)));
    }

    const memoizedModal_StartTableOnClick = useCallback(() => startTable, []);
    const memoizedModal_FindTableOnClick = useCallback(() => findTable, []);
    const memoizedModal_JoinTableOnClick = useCallback(() => joinTable, []);
    return (
        <div>
            <div>{tableName}</div>
            <ul>
                {players?.map((player) =>
                    <Player key={player.playerId} value={player} />
                )}
            </ul>
            <button onClick={() => { toggleName(); }}
                    hidden = {!!tableName}>
                Create Table
            </button>
            <button onClick={() => { toggleTable(); }}>
                Join {!!tableName ? "A Different" : ""} Table
            </button>
            <button onClick={() => {alert("Yup"); }} 
                    hidden = {!tableName}>
                Start Game
            </button>
            <button onClick={() => {alert("Yup"); }} 
                    hidden = {!tableName}>
                Leave Table
            </button>
            <StringModal
                innerBody={<div>Join as ...</div>}
                isShowing={isShowingPlayer}
                ok={(playerName) => {
                    if(tableName) {
                        console.log("join", tableName);
                        memoizedModal_JoinTableOnClick()(playerName, tableName);
                    }
                    else {
                        console.log("create");
                        memoizedModal_StartTableOnClick()(playerName);
                    }
                    toggleName();
                }}
                cancel={toggleName}
            />
            <StringModal
                innerBody={<div>Find table ...</div>}
                isShowing={isShowingTable}
                ok={(name) => {
                    memoizedModal_FindTableOnClick()(name);
                    toggleTable();
                    toggleName();
                }}
                cancel={toggleTable}
            />
        </div>
    );
}