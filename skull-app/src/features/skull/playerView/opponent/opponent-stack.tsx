import { OpponentHand } from "./opponent-hand";

export function OpponentStack(props: { count: number | null }): JSX.Element {
    const cardCount = props.count ?? 0;
    let cards = Array.from(Array(cardCount).keys()).map(a => <span key={a}>Card </span>)
    return (
        <div>
            {cards}
        </div>
    );
}