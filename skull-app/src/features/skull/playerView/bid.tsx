import { useState } from "react";
import { IGamePlayerView } from "../../../api/skull-api";

export function Bid(props: { view: IGamePlayerView | null, tableName: string }): JSX.Element {
    const minAllowedBid = !!(props.view?.bids?.length) ? Math.max(...props.view.bids.map(b => b.bid ?? 1)) : 1;
    const numberOfCardsPlayed = !!props.view?.opponentStates
        ? props.view?.playedCoasters?.length ?? 0 + props.view?.opponentStates?.flatMap(o => o.stackCount ?? 0).reduce((p,c) => p + c)
        : 0;
    const [ wantToBid, setWantToBid] = useState(false);

    function showBidOptions() {
        setWantToBid(true);
    }

    return (
        <div>
            <button onClick={showBidOptions} hidden={wantToBid || minAllowedBid > numberOfCardsPlayed}>Bid</button>
            <label hidden={!wantToBid} htmlFor="quantity">Quantity (between 1 and 5):</label>
            <input hidden={!wantToBid} type="number" id="quantity" name="quantity" defaultValue={minAllowedBid} min={minAllowedBid} max={numberOfCardsPlayed}/>
        </div>
    );
}