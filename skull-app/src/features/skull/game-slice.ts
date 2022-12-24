import { createAsyncThunk, createSlice, SliceCaseReducers } from "@reduxjs/toolkit";
import { IGamePlayerView } from "../../api/skull-api";
import { RootState } from "../../app/store";
import { GameService } from "./game-service";

export interface GameState {
  view: IGamePlayerView | null;
}

const initialGameState: GameState = {
  view: null
};

export const createGameAsync = createAsyncThunk(
  'game/start',
  async (tableName: string) => {
    const gameService = new GameService();
    await gameService.createGame(tableName)
      .catch((err) => console.log("Failed to create game", err));
  }
)

export const getGameAsync = createAsyncThunk(
  'game/retrieve',
 async ({tableName, playerNumber}: { tableName: string, playerNumber: number }) => {
  const gameService = new GameService();
  return await gameService.getGame(tableName, playerNumber)
    .catch((err) => console.log("Failed to find game/player", tableName, playerNumber, err));
 }
)

export const playCardAsync = createAsyncThunk(
  'game/play-card',
  async({tableName, playerNumber, card}: {tableName: string, playerNumber: number, card: boolean}) => {
    const gameService = new GameService();
    return await gameService.playCard(tableName, playerNumber, card)
      .catch((err) => console.log("Failed to play card", tableName, playerNumber, card, err));
  }
)

export const makeBidAsync = createAsyncThunk(
  'game/make-bid',
 async ({tableName, playerNumber, bid}: {tableName: string, playerNumber: number, bid: number | null}) => {
  const gameService = new GameService();
    return await gameService.makeBid(tableName, playerNumber, bid)
      .catch((err) => console.log("Failed to make bid", tableName, playerNumber, bid, err));
 }
)

export const gameSlice = createSlice<GameState, SliceCaseReducers<GameState>>({
  name: 'game',
  reducers: {
    join: (state) => {
      console.log("Hey");
    },
    showCardPlayed: (state, action) => {
      const opponent = state.view?.opponentStates?.find((o) => o.playerId === action.payload);
      opponent!.hand!.cardCount!-=1;
      opponent!.stackCount!+=1;
      console.log("Played", opponent?.playerId)
    }
  },
  extraReducers: (builder) => {
    builder
      .addCase(getGameAsync.fulfilled, (state, action) => {
        state.view = action.payload!;
      })
      .addCase(playCardAsync.fulfilled, (state, action) => {
        state.view = action.payload!;
      })
  },
  initialState: initialGameState
});

export const { showCardPlayed } = gameSlice.actions;


// The function below is called a selector and allows us to select a value from
// the state. Selectors can also be defined inline where they're used instead of
// in the slice file. For example: `useSelector((state: RootState) => state.counter.value)`
// export const selectName = (state: RootState) => state.game.phase;
export const selectPhase = (state: RootState) => state.game.view?.phase;

export const selectView = (state:RootState) => state.game.view;

export const gameReducer = gameSlice.reducer;