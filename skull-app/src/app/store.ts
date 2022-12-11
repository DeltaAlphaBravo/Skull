import { configureStore, ThunkAction, Action, createListenerMiddleware, isAnyOf, getDefaultMiddleware } from '@reduxjs/toolkit';
import { localPlayerReducer, setId, setName } from '../features/localPlayer/local-player-slice';
import { signalRReducer } from '../features/signalr/signalr-slice';
import { gameReducer } from '../features/skull/game-slice';
import { tableReducer } from '../features/table/table-slice';

const listenerMiddleware = createListenerMiddleware();
listenerMiddleware.startListening({
  matcher: isAnyOf(setName, setId),
  effect: (action, listenerApi) =>
    localStorage.setItem("playerIdentity", JSON.stringify((listenerApi.getState() as RootState).localPlayer))
});

const playerState = JSON.parse(localStorage.getItem('playerIdentity') || "null");
export const store = configureStore({
  preloadedState: {
    localPlayer: playerState === null ? { value: 0 } : playerState
  },
  reducer: {
    localPlayer: localPlayerReducer,
    table: tableReducer,
    game: gameReducer,
    signalr: signalRReducer,
  },
  middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(listenerMiddleware.middleware)
});

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
export type AppThunk<ReturnType = void> = ThunkAction<
  ReturnType,
  RootState,
  unknown,
  Action<string>
>;
