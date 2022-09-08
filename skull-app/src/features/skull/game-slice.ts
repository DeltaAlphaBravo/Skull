import { createAsyncThunk, createSlice, SliceCaseReducers } from "@reduxjs/toolkit";
import { RootState } from "../../app/store";
import { createGame } from "./game-api";

export interface GameState {
  phase: string;
}

const initialGameState: GameState = {
  phase: "not started",
};

export const createGameAsync = createAsyncThunk(
  'game/start',
  async (tableName: string) => {
    await createGame(tableName)
      .catch((err) => console.log("Failed to create game", err));
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
      });
  },
  initialState: initialGameState
});

export const { } = gameSlice.actions;


// The function below is called a selector and allows us to select a value from
// the state. Selectors can also be defined inline where they're used instead of
// in the slice file. For example: `useSelector((state: RootState) => state.counter.value)`
// export const selectName = (state: RootState) => state.game.phase;

export const gameReducer = gameSlice.reducer;