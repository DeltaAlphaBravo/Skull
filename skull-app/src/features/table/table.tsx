import { useAppDispatch, useAppSelector } from "../../app/hooks";
import { selectIsConnected, ensureSignalRConnectionAsync, subscribeToTableAsync } from "../signalr/signalr-slice";
import { SignalRService } from "../signalr/signalr-service";
import { createTableAsync, getTableAsync, joinTableAsync, selectName, selectPlayers } from "./table-slice";
import { Player } from "./player";
import useModal from "../modal/useModal";
import Modal from "../modal/string-modal";
import { setName } from "../localPlayer/local-player-slice";

export function Table(props: { signalrService: SignalRService }): JSX.Element {
    const signalrService = props.signalrService
    const isSignalRConnected = useAppSelector(selectIsConnected);
    const name = useAppSelector(selectName);
    const players = useAppSelector(selectPlayers);
    const dispatch = useAppDispatch();
    const { isShowing, toggle } = useModal();

    return (
        <div>
            <div>{name}</div>
            <button onClick={toggle}            >
                Create Table
            </button>
            <ul>
                {players?.map((player) =>
                    <Player key={player.playerId} value={player} />
                )}
            </ul>
            <Modal
                innerBody={<div>Join as ...</div>}
                isShowing={isShowing}
                ok={(name) => {
                    let tableName: string;
                    dispatch(setName(name));
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
                    toggle();
                }}
                cancel={toggle}
            />
        </div>
    );
}