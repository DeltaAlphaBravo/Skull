import { IGamePlayerView, IPlayer } from "../../../api/skull-api";
import { Bid } from "./bid";
import { Opponent } from "./opponent/opponent";
import { PlayerHand } from "./player-hand";
import { Stack } from "./stack";

export function PlayerView(props: { view: IGamePlayerView | null, tableName: string, players: IPlayer[] }): JSX.Element {
    const myTurn = !!props.view && (props.view.nextPlayer === props.view!.playerId);
    const opponents = props.view?.opponentStates?.map(o => 
            <Opponent tableName={props.tableName}
                      opponent={o} key={o.playerId}
                      name={props.players?.find(p => p.playerId === o.playerId)?.name!}
                      bid={props.view?.bids?.find(b => b.playerId === o.playerId)?.bid ?? null}
                      revealEnabled={props.view!.phase!.toLowerCase() === 'reveal' && myTurn && (props.view?.playedCoasters?.length ?? -1) === 0}/>)
    
    return (
        <div hidden={!props.view?.phase}>
            <PlayerHand 
                hand={props.view?.hand ?? null} 
                tableName={props.tableName} 
                acceptPlay={myTurn && props.view?.phase?.toLowerCase() === 'placement'}
            />
            <div hidden={!myTurn || props.view!.phase!.toLowerCase() === "reveal"}>
                <Bid view={props.view} tableName={props.tableName}/>
            </div>
            <Stack tableName={props.tableName}
                   playerId = {props.view?.playerId ?? -1}
                   cardCount = {props.view?.playedCoasters?.length ?? -1}
                   revealEnabled = {props.view?.phase?.toLowerCase() === 'reveal' && myTurn}/>
            {opponents}
        </div>
    );
}