import { createGameAsync } from "./game-slice";
import { useAppDispatch, useAppSelector } from "../../app/hooks";
import { selectIsConnected, ensureSignalRConnectionAsync, subscribeToTableAsync } from "../signalr/signalr-slice";
import { SignalRService } from "../signalr/signalr-service";

export function Game(props: { name: string; }) {
    const name = props.name;
    const isSignalRConnected = useAppSelector(selectIsConnected);
    const dispatch = useAppDispatch();
    const signalrService: SignalRService = new SignalRService();
    return (
        <div>
            <div>{name}</div>
            <button
                onClick={
                    () => {
                        dispatch(createGameAsync(name))
                            .then(async (result) => {
                                console.log(isSignalRConnected);
                                if (!isSignalRConnected) {
                                    await dispatch(ensureSignalRConnectionAsync(signalrService));
                                    return await Promise.resolve(result.payload as string);
                                } else {
                                    return Promise.resolve(result.payload as string);
                                }
                            })
                            .then((gameName) => dispatch(subscribeToTableAsync({ table: gameName, signalRService: signalrService })));
                    }
                }
            >
                Create Game
            </button>
        </div>
    );
}