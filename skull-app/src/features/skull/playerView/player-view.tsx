import { IGamePlayerView } from "../../../api/skull-api";
import { Opponent } from "./opponent/opponent";
import { PlayerHand } from "./player-hand";

export function PlayerView(props: { view: IGamePlayerView | null, tableName: string }): JSX.Element {
    let opponents = props.view?.opponentStates?.map(o => <Opponent opponent={o} key={o.playerId}></Opponent>)

    return (
        <div>
            <PlayerHand 
                hand={props.view?.hand ?? null} 
                tableName={props.tableName} 
                turn={props.view !== null && (props.view?.nextPlayer === props.view?.playerId)}
            />
            {opponents}
        </div>
    );
}