import { IOpponentHand, IReadOnlyHand } from "../../../../api/skull-api";

export function OpponentHand(props: { hand: IOpponentHand | null }): JSX.Element {
    const cardCount = props.hand?.cardCount ?? 0;
    let cards = Array.from(Array(cardCount).keys()).map(a => <span key={a}>Card </span>)
    return (
        <div>
            {cards}
        </div>
    );
}