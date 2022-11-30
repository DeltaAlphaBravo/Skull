import { IGamePlayerView } from "../../../api/skull-api";
import { PlayerHand } from "./player-hand";

export function PlayerView(props: { view: IGamePlayerView | null }): JSX.Element {
    return (
        <div>
            <PlayerHand hand={props.view?.hand ?? null}/>
        </div>
    );
}