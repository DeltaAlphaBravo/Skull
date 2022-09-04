import { createGameAsync, selectName } from "./gameSlice";
import { useAppDispatch, useAppSelector } from "../../app/hooks";

export function Game() {
    const name = useAppSelector(selectName);
    const dispatch = useAppDispatch();
    return (
        <div>
            <div>{name}</div>
            <button
                onClick={() => dispatch(createGameAsync())}
            >
                Create Game
            </button>
        </div>
    );
}