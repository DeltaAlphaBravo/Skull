import { IGamePlayerView, IPlayer } from "../../../api/skull-api";
import { Bid } from "./bid";
import { Opponent } from "./opponent/opponent";
import { PlayerHand } from "./player-hand";

export function PlayerView(props: { view: IGamePlayerView | null, tableName: string, players: IPlayer[] }): JSX.Element {
    const opponents = props.view?.opponentStates?.map(o => 
            <Opponent opponent={o} key={o.playerId}
                      name={props.players?.find(p => p.playerId === o.playerId)?.name!}
                      bid={props.view?.bids?.find(b => b.playerId === o.playerId)?.bid ?? null}/>)
    const myTurn = props.view !== null && (props.view?.nextPlayer === props.view?.playerId);

    return (
        <div hidden={!props.view?.phase}>
            <PlayerHand 
                hand={props.view?.hand ?? null} 
                tableName={props.tableName} 
                acceptPlay={myTurn && props.view?.phase?.toLowerCase() === 'placement'}
            />
            <div hidden={!myTurn}>
                <Bid view={props.view} tableName={props.tableName}/>
            </div>
            {opponents}
        </div>
    );
}