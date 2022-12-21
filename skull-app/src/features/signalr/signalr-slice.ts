import { createAsyncThunk, createSlice, SliceCaseReducers } from '@reduxjs/toolkit';
import { RootState } from '../../app/store';
import { SignalRService } from './signalr-service';

export interface SignalRStatus {
  isConnected: boolean,
}

const initialSignalRStatus: SignalRStatus = {
  isConnected: false,
};

export const ensureSignalRConnectionAsync = createAsyncThunk(
  'signalr/start',
  async (signalRService: SignalRService) => {
    if(signalRService.isConnected) return true;
    console.log("Attempting to start signalr");
    return await signalRService.startConnection();
  }
)

export const signalRSlice = createSlice<SignalRStatus, SliceCaseReducers<SignalRStatus>>({
  name: 'signalr',
  reducers: { },
  extraReducers: (builder) => {
    builder
      .addCase(ensureSignalRConnectionAsync.fulfilled, (state, action) => {
        state.isConnected = action.payload;
      })
  },
  initialState: initialSignalRStatus
});

export const {  } = signalRSlice.actions;

export const selectIsConnected = (state: RootState) => state.signalr.isConnected;

export const signalRReducer = signalRSlice.reducer;