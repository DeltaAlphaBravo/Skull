import { IOpponentState } from "../../../../api/skull-api";
import { Stack } from "../stack";
import { OpponentHand } from "./opponent-hand";

export function Opponent(props: { tableName: string, opponent: IOpponentState | null, name: string, bid: number | null, revealEnabled: boolean }): JSX.Element {
    let reveals = Array.from(props.opponent?.reveals ?? []).map((a, idx) => <span key={idx}>{a ? "Skull" : "Flower"} </span>);
    return (
        <div>
            {props.name}
            <OpponentHand hand={props?.opponent?.hand ?? null}/>
            <Stack tableName={props.tableName}
                   playerId = {props.opponent?.playerId ?? -1}
                   cardCount = {props.opponent?.stackCount ?? -1}
                   revealEnabled = {props.revealEnabled}/>
            {reveals}
        </div>
    );
}