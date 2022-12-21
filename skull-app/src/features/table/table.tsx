import { useAppDispatch, useAppSelector } from "../../app/hooks";
import { createTableAsync, getTableAsync, joinTableAsync, selectTableName, selectPlayers, setTableName } from "./table-slice";
import useModal from "../modal/useModal";
import { setId, setName as setPlayerName } from "../localPlayer/local-player-slice";
import StringModal from "../modal/string-modal";
import { useCallback } from "react";
import { useNavigate } from 'react-router-dom';

export function Table(): JSX.Element {
    const tableName = useAppSelector(selectTableName);
    const dispatch = useAppDispatch();
    const navigate = useNavigate();
    const { isShowing: isShowingTable, toggle: toggleTable } = useModal();
    const { isShowing: isShowingPlayer, toggle: toggleName } = useModal();

    const startTable = (name: string) => {
        name = name ?? "The Nameless One";
        let table = "";
        let id = -1;
        dispatch(setPlayerName(name));
        dispatch(createTableAsync())
            .then((result) => table = result.payload as string)
            .then(() => dispatch(joinTableAsync({ tableName: table, playerName: name })))
            .then((result) => {
                 dispatch(setId(result.payload))
                 id = result.payload as number;
            })
            .then(() => navigate(table));
    }

    const findTable = (name: string) => {
            dispatch(getTableAsync(name))
            .then(() => dispatch(setTableName(name)))
            .then((response) => {
                if (!response.payload) {
                    toggleTable();
                    alert("Not found");
                } else {
                    // signalrService.OnPlayerJoin(
                    //     () => dispatch(getTableAsync(name)))
                }
            });
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