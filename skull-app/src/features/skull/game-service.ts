import { Client, IGamePlayerView } from "../../api/skull-api";
import Config from "../../config.json";

export class GameService {

  private _client: Client = new Client(Config.api);

  constructor() {}

  createGame(tableName: string): Promise<void> {
    return this._client.game(tableName);
  }

  getGame(tableName: string, playerNumber: number): Promise<IGamePlayerView> {
    return this._client.view(tableName, playerNumber);
  }

  playCard(tableName: string, playerNumber: number, card: boolean): Promise<IGamePlayerView> {
    return this._client.stack(tableName, playerNumber, card);
  }
}