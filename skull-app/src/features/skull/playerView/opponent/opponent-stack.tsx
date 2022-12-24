import { OpponentHand } from "./opponent-hand";

export function OpponentStack(props: { count: number | null, enabled: boolean }): JSX.Element {
    const cardCount = props.count ?? 0;
    let cards = Array.from(Array(cardCount).keys()).map(a => <button key={a} disabled={!props.enabled}>Card </button>)
    return (
        <div>
            {cards}
        </div>
    );
}