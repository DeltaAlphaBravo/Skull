import { Client, ITableView } from "../../api/skull-api";
import Config from "../../config.json";

export class TableService {

  private _client: Client = new Client(Config.api);

  constructor() {

  }

  createTable(): Promise<string> {
    return this._client.tablePOST();
  }
  getTable(tableName: string): Promise<ITableView> {
    return this._client.tableGET(tableName);
  }
  joinTable(tableName: string, username: string): Promise<number> {
    return this._client.players(tableName, username);
  }
}