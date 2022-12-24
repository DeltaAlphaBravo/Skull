import { useRef, useState } from "react";
import { IGamePlayerView } from "../../../api/skull-api";
import { useAppDispatch } from "../../../app/hooks";
import { makeBidAsync } from "../game-slice";

export function Bid(props: { view: IGamePlayerView | null, tableName: string }): JSX.Element {
    const dispatch = useAppDispatch();

    const minAllowedBid = !!(props.view?.bids?.length) ? Math.max(...props.view.bids.map(b => b.bid ?? 0)) + 1 : 1;
    const numberOfCardsPlayed = (!!props.view?.opponentStates && !!props.view?.playedCoasters)
        ? (props.view?.playedCoasters?.length ?? 0) + props.view?.opponentStates?.flatMap(o => o.stackCount ?? 0).reduce((p,c) => p + c)
        : 0;
    const [ wantToBid, setWantToBid] = useState(false);
    const inputRef = useRef<HTMLInputElement>(null);

    function showBidOptions() {
        setWantToBid(true);
    }

    function isChallengePhase() {
        return props.view?.phase?.toLowerCase() === 'challenge'
    }

    function showBidButton() {
        return wantToBid || minAllowedBid > numberOfCardsPlayed || !props?.view?.playedCoasters?.length || isChallengePhase();
    }

    function sendBid() {
        const table = props.tableName;
        const player = props.view!.playerId!;
        const bid = parseInt(inputRef.current!.value);
        dispatch(makeBidAsync({tableName: table, playerNumber: player, bid: bid}));
    }

    function sendPass() {
        const table = props.tableName;
        const player = props.view!.playerId!;
        const bid = null;
        dispatch(makeBidAsync({tableName: table, playerNumber: player, bid: bid}));
    }

    return (
        <div>
            <button onClick={showBidOptions} hidden={showBidButton()}>Bid</button>
            {/* <label hidden={!wantToBid} htmlFor="quantity">Quantity:</label> */}
            <input hidden={!wantToBid && !isChallengePhase()} 
                   ref={inputRef}
                   type="number" 
                   id="quantity" 
                   name="quantity" 
                   min={minAllowedBid} 
                   max={numberOfCardsPlayed}/>
            <button onClick={sendBid} hidden={!wantToBid && !isChallengePhase()}>Bid</button>
            <button onClick={sendPass} hidden={!isChallengePhase()}>Pass</button>
        </div>
    );
}