import { IGamePlayerView, IPlayer } from "../../../api/skull-api";
import { Opponent } from "./opponent/opponent";
import { PlayerHand } from "./player-hand";

export function PlayerView(props: { view: IGamePlayerView | null, tableName: string, players: IPlayer[] }): JSX.Element {
    let opponents = props.view?.opponentStates?.map(o => <Opponent opponent={o} key={o.playerId} name={props.players.find(p => p.playerId === o.playerId)?.name!}></Opponent>)

    return (
        <div>
            <PlayerHand 
                hand={props.view?.hand ?? null} 
                tableName={props.tableName} 
                turn={props.view !== null && (props.view?.nextPlayer === props.view?.playerId)}
            />
            {opponents}
        </div>
    );
}