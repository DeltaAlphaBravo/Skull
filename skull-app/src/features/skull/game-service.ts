import { Client } from "../../api/skull-api";
import Config from "../../config.json";

export class GameService {

  private _client: Client = new Client(Config.api);

  constructor() {}

  createGame(tableName: string): Promise<void> {
    return this._client.game(tableName);
  }
}