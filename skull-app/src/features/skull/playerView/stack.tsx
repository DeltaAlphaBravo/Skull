import { useAppDispatch } from "../../../app/hooks";
import { revealCardAsync } from "../game-slice";

export function Stack(props: { tableName: string, playerId: number, cardCount: number, revealEnabled: boolean }): JSX.Element {
    const dispatch = useAppDispatch();
    const revealNextCard = () => dispatch(revealCardAsync({tableName: props.tableName, playerNumber: props.playerId ?? -1}));
    return (
        <div>
            <button key={props.playerId} disabled={!props.revealEnabled || (props.cardCount ?? 0) <= 0} onClick={revealNextCard}>{props.cardCount} cards</button>
        </div>
    );
}