import { configureStore } from '@reduxjs/toolkit';
import appReducer from '../App/AppStore';
import serviceListReducer from '../features/ServiceList/ServiceListStore';
import logListReducer from '../features/LogList/LogListStore';
import environmentReducer from '../features/Environment/EnvironmentStore';

export const store = configureStore({
    reducer: {
        app: appReducer,
        serviceList: serviceListReducer,
        logList: logListReducer,
        environment: environmentReducer
    }
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;