import { Client, ITableView } from "../../api/skull-api";

export class TableService {
  createTable(): Promise<string> {
    var client = new Client("https://localhost:55009")
    return client.tablePOST();
  }
  getTable(tableName: string): Promise<ITableView> {
    var client = new Client("https://localhost:55009")
    return client.tableGET(tableName);
  }
  joinTable(tableName: string, username: string): Promise<number> {
    var client = new Client("https://localhost:55009")
    return client.players(tableName, username);
  }
}