import { IOpponentState } from "../../../../api/skull-api";
import { OpponentHand } from "./opponent-hand";
import { OpponentStack } from "./opponent-stack";

export function Opponent(props: { opponent: IOpponentState | null, name: string, bid: number | null, revealEnabled: boolean }): JSX.Element {
    return (
        <div>
            {props.name}
            <OpponentHand hand={props?.opponent?.hand ?? null}/>
            <OpponentStack count={props.opponent?.stackCount ?? null} enabled={props.revealEnabled}/>
            {props.bid}
        </div>
    );
}