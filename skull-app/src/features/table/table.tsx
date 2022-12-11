import { useAppDispatch, useAppSelector } from "../../app/hooks";
import { selectIsConnected, ensureSignalRConnectionAsync, subscribeToTableAsync } from "../signalr/signalr-slice";
import { SignalRService } from "../signalr/signalr-service";
import { createTableAsync, getTableAsync, joinTableAsync, selectTableName, selectPlayers, setTableName, leaveTableAsync } from "./table-slice";
import useModal from "../modal/useModal";
import { setId, setName as setPlayerName } from "../localPlayer/local-player-slice";
import StringModal from "../modal/string-modal";
import { useCallback } from "react";
import { useNavigate } from 'react-router-dom';

export function Table(props: { signalrService: SignalRService }): JSX.Element {
    const signalrService = props.signalrService
    const isSignalRConnected = useAppSelector(selectIsConnected);
    const tableName = useAppSelector(selectTableName);
    const players = useAppSelector(selectPlayers);
    const dispatch = useAppDispatch();
    const navigate = useNavigate();
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
            .then(() => dispatch(joinTableAsync({ tableName: tableName, playerName: name })))
            .then((result) => dispatch(setId(result.payload)))
            .then(() => navigate(tableName));
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
                    signalrService.OnPlayerJoin(() => dispatch(getTableAsync(name))
                                                          .then(() => dispatch(setTableName(name))))
                }
            });
        if (tableName) {
            
        }
    }

    const joinTable = (playerName: string, tableName: string) => {
        playerName = playerName ?? "The Nameless One";
        dispatch(joinTableAsync({ tableName: tableName, playerName: playerName }))
            .then((result) => dispatch(setId(result.payload)))
            .then(() => dispatch(setPlayerName(playerName)))
            .then(() => navigate(tableName));
    }

    const memoizedModal_StartTableOnClick = useCallback(() => startTable, []);
    const memoizedModal_FindTableOnClick = useCallback(() => findTable, []);
    const memoizedModal_JoinTableOnClick = useCallback(() => joinTable, []);
    return (
        <div>
            <button onClick={() => { toggleName(); }}
                    hidden = {!!tableName}>
                Create Table
            </button>
            <button onClick={() => { toggleTable(); }}
                    hidden = {!!tableName}>
                Join Table
            </button>
            <StringModal
                innerBody={<div>Join as ...</div>}
                isShowing={isShowingPlayer}
                ok={(playerName) => {
                    !!tableName 
                        ? memoizedModal_JoinTableOnClick()(playerName, tableName)
                        : memoizedModal_StartTableOnClick()(playerName)
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