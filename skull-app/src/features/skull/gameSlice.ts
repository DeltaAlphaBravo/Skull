import { createAsyncThunk, createSlice, SliceCaseReducers } from "@reduxjs/toolkit";
import { RootState } from "../../app/store";
import { createGame } from "./gameApi";

export interface GameState {
  name: string;
}

const initialGameState: GameState = {
  name: "initial",
};

export const createGameAsync = createAsyncThunk(
  'game/start',
  async () => {
    const response = await createGame();
    return response.name;
  }
)

export const gameSlice = createSlice<GameState, SliceCaseReducers<GameState>>({
  name: 'game',
  reducers: { },
  extraReducers: (builder) => {
    builder
      .addCase(createGameAsync.fulfilled, (state, action) => {
        state.name = action.payload || "";
      });
  },
  initialState: initialGameState
});

export const { start } = gameSlice.actions;


// The function below is called a selector and allows us to select a value from
// the state. Selectors can also be defined inline where they're used instead of
// in the slice file. For example: `useSelector((state: RootState) => state.counter.value)`
export const selectName = (state: RootState) => state.game.name;

export const gameReducer = gameSlice.reducer;