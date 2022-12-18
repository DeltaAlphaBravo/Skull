import { IOpponentState } from "../../../../api/skull-api";
import { OpponentHand } from "./opponent-hand";
import { OpponentStack } from "./opponent-stack";

export function Opponent(props: { opponent: IOpponentState | null }): JSX.Element {
    return (
        <div>
            Player {props.opponent?.playerId}
            <OpponentHand hand={props?.opponent?.hand ?? null}/>
            <OpponentStack count={props.opponent?.stackCount ?? null}/>
        </div>
    );
}