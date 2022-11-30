import { IReadOnlyHand } from "../../../api/skull-api";

export function PlayerHand(props: { hand: IReadOnlyHand | null }): JSX.Element {
    const flowers = props.hand?.cardCount ?? 0 - (props.hand?.hasSkull ? 1: 0);
    let cards = Array.from(Array(flowers).keys()).map(a => <span>flower</span>)
    if(props.hand?.hasSkull) cards = cards.concat(<span>skull</span>);
    return (
        <div>
            {cards}
        </div>
    );
}