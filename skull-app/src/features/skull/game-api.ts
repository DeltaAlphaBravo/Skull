import { Client } from "../../api/skull-api";

export function createGame(tableName: string) {
  var client = new Client("https://localhost:55009")
  return client.game(tableName);
}