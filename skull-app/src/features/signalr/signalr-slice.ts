import { createAsyncThunk, createSlice, SliceCaseReducers } from '@reduxjs/toolkit';
import { RootState } from '../../app/store';
import { SignalRService } from './signalr-service';

export interface SignalRStatus {
  isConnected: boolean,
  joinedGame: string | null,
}

const initialSignalRStatus: SignalRStatus = {
  isConnected: false,
  joinedGame: null,
};

export const ensureSignalRConnectionAsync = createAsyncThunk(
  'signalr/start',
  async (signalRService: SignalRService) => {
    console.log("Attempting to start signalr");
    return await signalRService.StartConnection();
  }
)

export const subscribeToTableAsync = createAsyncThunk(
  'signalr/join',
  async ({table, signalRService}: {table: string, signalRService: SignalRService}) => {
    console.log("Attempting to join signalr");
    return await signalRService.subscribeTo(table);
  }
)

export const signalRSlice = createSlice<SignalRStatus, SliceCaseReducers<SignalRStatus>>({
  name: 'signalr',
  reducers: { },
  extraReducers: (builder) => {
    builder
      .addCase(ensureSignalRConnectionAsync.fulfilled, (state, action) => {
        state.isConnected = action.payload;
      });
  },
  initialState: initialSignalRStatus
});

export const {  } = signalRSlice.actions;

export const selectIsConnected = (state: RootState) => state.signalr.isConnected;

export const signalRReducer = signalRSlice.reducer;