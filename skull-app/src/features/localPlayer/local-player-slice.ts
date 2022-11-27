import { createSlice, SliceCaseReducers } from "@reduxjs/toolkit";
import { RootState } from "../../app/store";

export interface ILocalPlayer {
  name: string | null,
  id: number | null
}

const initalTableState: ILocalPlayer = {
    name: null,
    id: null,
};

export const localPlayerSlice = createSlice<ILocalPlayer, SliceCaseReducers<ILocalPlayer>>({
  name: 'local-player',
  reducers: {
    setName: (state, action) => {
      state.name = action.payload;
    },
    setId: (state, action) => {
      state.id = action.payload;
    }
  },
  initialState: initalTableState
});

export const { setName, setId } = localPlayerSlice.actions;


// The function below is called a selector and allows us to select a value from
// the state. Selectors can also be defined inline where they're used instead of
// in the slice file. For example: `useSelector((state: RootState) => state.counter.value)`
export const selectName = (state: RootState) => state.localPlayer.name;

export const selectId = (state: RootState) => state.localPlayer.id;

export const localPlayerReducer = localPlayerSlice.reducer;