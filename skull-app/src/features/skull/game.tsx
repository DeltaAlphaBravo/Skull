import { createGameAsync, selectName } from "./game-slice";
import { useAppDispatch, useAppSelector } from "../../app/hooks";
import { selectIsConnected, ensureSignalRConnectionAsync, subscribeToGameAsync } from "../signalr/signalr-slice";
import { SignalRService } from "../signalr/signalr-service";

export function Game() {
    const name = useAppSelector(selectName);
    const isSignalRConnected = useAppSelector(selectIsConnected);
    const dispatch = useAppDispatch();
    const signalrService: SignalRService = new SignalRService();
    return (
        <div>
            <div>{name}</div>
            <button
                onClick={
                    () => {
                        dispatch(createGameAsync())
                            .then((result) => {
                                console.log(isSignalRConnected);
                                if (!isSignalRConnected) {
                                    return dispatch(ensureSignalRConnectionAsync(signalrService))
                                        .then(() => Promise.resolve(result.payload as string));
                                } else {
                                    return Promise.resolve(result.payload as string);
                                }
                            })
                            .then((gameName) => dispatch(subscribeToGameAsync({ game: gameName, signalRService: signalrService })));
                    }
                }
            >
                Create Game
            </button>
        </div>
    );
}