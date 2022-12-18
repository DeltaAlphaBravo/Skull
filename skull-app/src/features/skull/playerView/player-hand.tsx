import { IReadOnlyHand } from "../../../api/skull-api";
import { useAppDispatch } from "../../../app/hooks";
import { playCardAsync } from "../game-slice";

export function PlayerHand(props: { hand: IReadOnlyHand | null,  tableName: string, turn: boolean}): JSX.Element {
    const dispatch = useAppDispatch();
    const playFlower = () => dispatch(playCardAsync({tableName: props.tableName, playerNumber: props.hand?.playerId ?? -1, card: false}));
    const playSkull = () => dispatch(playCardAsync({tableName: props.tableName, playerNumber: props.hand?.playerId ?? -1, card: true}));

    const flowers = (props.hand?.cardCount ?? 0) - (props.hand?.hasSkull ? 1 : 0);
    let cards = Array.from(Array(flowers).keys()).map(a => <button key={a} onClick={playFlower} disabled={!props.turn}>flower </button>)
    if(props.hand?.hasSkull) cards = cards.concat(<button key={flowers} onClick={playSkull} disabled={!props.turn}>skull</button>);
    return (
        <div>
            {cards}
        </div>
    );
}