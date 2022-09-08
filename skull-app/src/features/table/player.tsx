import React from "react";
import { IPlayer } from "../../api/skull-api";

export class Player extends React.Component<{value: IPlayer}> {
    constructor(props: Readonly<{value: IPlayer}>) {
        super(props);
        this.state = props.value;
    }

    render() {
        const player: IPlayer = this.state;
        return (
            <li>{player.playerId} - {player.name}</li>
        );
    }
}