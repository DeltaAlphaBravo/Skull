import { createAsyncThunk, createSlice, SliceCaseReducers } from "@reduxjs/toolkit";
import { ITableView } from "../../api/skull-api";
import { RootState } from "../../app/store";
import { TableService } from "./table-service";

const initalTableState: ITableView = {
  name: null,
  players: null,
};

export const leaveTableAsync = createAsyncThunk(
  'table/leave',
  async () => {
    //would need to remove the player from the api also.
    return;
  }
)

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
    console.log("get table ", tableName);
    var table = await new TableService().getTable(tableName)
      .catch((err) => console.log("Failed to find table", err));
    return table
  }
)

export const joinTableAsync = createAsyncThunk(
  'table/join',
  async ({ tableName, playerName }: { tableName: string, playerName: string }) => {
    return await (new TableService()).joinTable(tableName, playerName)
      .catch((err) => console.log("Failed to join table", err));
  }
)

export const tableSlice = createSlice<ITableView, SliceCaseReducers<ITableView>>({
  name: 'table',
  reducers: {
    setTableName: (state, action) => {
      state.name = action.payload;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(leaveTableAsync.fulfilled, (state, action) => {
        state.name = null;
        state.players = null;
      })
      .addCase(createTableAsync.fulfilled, (state, action) => {
        state.name = action.payload ?? null;
      })
      .addCase(getTableAsync.fulfilled, (state, action) => {
        state.players = action.payload?.players;
        state.name = state.name ?? action.payload?.name;
      })
      .addCase(joinTableAsync.fulfilled, (state, action) => {
      });
  },
  initialState: initalTableState
});

export const { setTableName } = tableSlice.actions;


// The function below is called a selector and allows us to select a value from
// the state. Selectors can also be defined inline where they're used instead of
// in the slice file. For example: `useSelector((state: RootState) => state.counter.value)`
export const selectTableName = (state: RootState) => state.table?.name as string | null;

export const selectPlayers = (state: RootState) => state.table.players;

export const tableReducer = tableSlice.reducer;