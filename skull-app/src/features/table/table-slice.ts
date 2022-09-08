import { createAsyncThunk, createSlice, SliceCaseReducers } from "@reduxjs/toolkit";
import { ITableView } from "../../api/skull-api";
import { RootState } from "../../app/store";
import { TableService } from "./table-service";

const initalTableState: ITableView = {
    name: null,
    players: null,
};

export const createTableAsync = createAsyncThunk(
  'table/create',
  async () => {
    return await (new TableService()).createTable()
      .catch((err) => console.log("Failed to create table", err));
  }
)

export const getTableAsync = createAsyncThunk(
    'table/retrieve',
    async (tableName: string) => {
      return await (new TableService()).getTable(tableName)
        .catch((err) => console.log("Failed to get table", err));
    }
  )

  export const joinTableAsync = createAsyncThunk(
    'table/join',
    async ({tableName, playerName}: {tableName: string, playerName: string}) => {
      return await (new TableService()).joinTable(tableName, playerName)
        .catch((err) => console.log("Failed to join table", err));
    }
  )

export const tableSlice = createSlice<ITableView, SliceCaseReducers<ITableView>>({
  name: 'table',
  reducers: {
    join: () => {
      
    }
  },
  extraReducers: (builder) => {
    builder
      .addCase(createTableAsync.fulfilled, (state, action) => {
        state.name = action.payload || undefined;
      })
      .addCase(getTableAsync.fulfilled, (state, action) => {
        console.log(action.payload?.players);
        state.players = action.payload?.players;
      })
      .addCase(joinTableAsync.fulfilled, (state, action) => {
      });
  },
  initialState: initalTableState
});

export const { } = tableSlice.actions;


// The function below is called a selector and allows us to select a value from
// the state. Selectors can also be defined inline where they're used instead of
// in the slice file. For example: `useSelector((state: RootState) => state.counter.value)`
export const selectName = (state: RootState) => state.table.name;

export const selectPlayers = (state: RootState) => state.table.players;

export const tableReducer = tableSlice.reducer;