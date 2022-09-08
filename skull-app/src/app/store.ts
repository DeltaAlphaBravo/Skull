import { configureStore, ThunkAction, Action } from '@reduxjs/toolkit';
import { signalRReducer } from '../features/signalr/signalr-slice';
import { gameReducer } from '../features/skull/game-slice';
import { tableReducer } from '../features/table/table-slice';

export const store = configureStore({
  reducer: {
    table: tableReducer,
    game: gameReducer,
    signalr: signalRReducer,
  },
});

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
export type AppThunk<ReturnType = void> = ThunkAction<
  ReturnType,
  RootState,
  unknown,
  Action<string>
>;
