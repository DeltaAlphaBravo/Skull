import { createAsyncThunk, createSlice, SliceCaseReducers } from "@reduxjs/toolkit";
import { IGamePlayerView } from "../../api/skull-api";
import { RootState } from "../../app/store";
import { GameService } from "./game-service";

export interface GameState {
  phase: string;
  view: IGamePlayerView | null;
}

const initialGameState: GameState = {
  phase: "not started",
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
    .catch((err) => console.log("Failed to find game/player", err));
 }
)

export const gameSlice = createSlice<GameState, SliceCaseReducers<GameState>>({
  name: 'game',
  reducers: {
    join: (state) => {
      console.log("Hey");
    }
  },
  extraReducers: (builder) => {
    builder
      .addCase(createGameAsync.fulfilled, (state, action) => {
        state.phase = "placement";
      })
      .addCase(getGameAsync.fulfilled, (state, action) => {
        state.view = action.payload!;
      });
  },
  initialState: initialGameState
});

export const { } = gameSlice.actions;


// The function below is called a selector and allows us to select a value from
// the state. Selectors can also be defined inline where they're used instead of
// in the slice file. For example: `useSelector((state: RootState) => state.counter.value)`
// export const selectName = (state: RootState) => state.game.phase;
export const selectPhase = (state: RootState) => state.game.phase;

export const selectView = (state:RootState) => state.game.view;

export const gameReducer = gameSlice.reducer;