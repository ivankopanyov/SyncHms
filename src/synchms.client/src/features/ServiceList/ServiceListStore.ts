import { PayloadAction, createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import { ServiceList as ServiceList, ServiceInfo, SimpleServiceInfo } from './data';
import { api } from '../../utils/api';
import { object, params } from '../../utils/mapper';

export interface Service {
    name: string;
    state?: {
        isActive: boolean;
        error?: string;
        stackTrace?: string;
        info?: string;
    },
    jsonOptions?: string;
}

const defaultServiceList: ServiceList = {
    services: [],
    serviceCount: 0,
    activeServiceCount: 0,
    loading: false,
    auth: true
};

const initialState: ServiceList = defaultServiceList;

export const getService = createAsyncThunk('serviceList/getService', async (service: ServiceInfo) => 
    await api.get(`/services/${service.name}`));

export const reloadService = createAsyncThunk('serviceList/reloadService', async (service: ServiceInfo) =>
    await api.get(`/services/reload/${service.name}`));

export const updateService = createAsyncThunk('serviceList/updateService', async (service: SimpleServiceInfo) =>
    await api.put(`/services/${service.name}`, {
        jsonOptions: JSON.stringify(object(service.parameters))
    }));

export const deleteService = createAsyncThunk('serviceList/deleteService', async (service: ServiceInfo) =>
    await api.delete(`/services/${service.name}`));

const setService = (state: ServiceList, payload: Service) => {
    const { name, jsonOptions } = payload;
    const isActive = payload.state?.isActive;

    let service = state.services.find(s => s.name === name);
    let index = -1;

    if (!service) {
        state.serviceCount += 1;
        if (isActive) {
            state.activeServiceCount += 1;
        }
    } else {
        if (service.timeoutId) {
            clearTimeout(service.timeoutId);
            service.timeoutId = undefined;
        }

        if (isActive && !service.state?.isActive) {
            state.activeServiceCount += 1;
        } else if (!isActive && service.state?.isActive) {
            state.activeServiceCount -= 1;
        }

        index = state.services.indexOf(service);
    }

    service = {
        name: name,
        state: !payload.state ? undefined : {
            isActive: payload.state.isActive,
            error: payload.state.error,
            stackTrace: payload.state.stackTrace
        },
        loading: false,
        updateError: payload.state?.info,
        parameters: {
            booleanParameters: [],
            stringParameters: [],
            listParameters: [],
            booleanMapParameters: [],
            stringMapParameters: []
        }
    };

    if (jsonOptions)
        service.parameters = params(JSON.parse(jsonOptions));

    index >= 0
        ? state.services[index] = service
        : state.services.push(service);
};

const remove = (state: ServiceList, name: string) => {
    const service = state.services.find(s => s.name === name);
    if (service) {
        const index = state.services.indexOf(service);
        if (index >= 0) {
            state.serviceCount -= 1;
            if (service.state?.isActive) {
                state.activeServiceCount -= 1;
            }

            state.services.splice(index, 1);
        }
    }
};

const pending = (state: ServiceList, action: any) => {
    const { name } = action.meta.arg;
    const service = state.services.find(s => s.name === name);
    if (service) {
        service.loading = true;
        service.updateError = undefined;
    }
};

const rejected = (state: ServiceList, action: any) => {
    const { name } = action.meta.arg;
    const service = state.services.find(s => s.name === name);
    if (service) {
        if (service.timeoutId) {
            clearTimeout(service.timeoutId);
            service.timeoutId = undefined;
        }
        service.loading = false;
        service.updateError = action.error.message;
    }

    if (action.error.code === '401')
        state.auth = false;
};

const hostListSlice = createSlice({
    name: 'serviceList',
    initialState,
    reducers: {
        update(state, action: PayloadAction<Service>) {
            setService(state, action.payload)
        },
        updateServiceRange(state, action: PayloadAction<Service[]>) {
            action.payload.forEach(service => setService(state, service));
            state.loading = false;
            state.error = undefined;
        },
        removeService(state, action: PayloadAction<string>) {
            remove(state, action.payload);
        },
        setTimeoutService(state, action: PayloadAction<{
            name: string;
            timeoutId: number;
        }>) {
            const { name, timeoutId } = action.payload;
            const service = state.services.find(s => s.name === name);
            if (service) {
                service.timeoutId = timeoutId;
                service.loading = true;
            }
        },
        setLoading(state, action: PayloadAction<boolean>) {
            state.loading = action.payload;
        },
        setError(state, action: PayloadAction<string | undefined>) {
            state.error = action.payload;
        },
        authorized(state) {
            state.auth = true;
        }
    },
    extraReducers: (builder) => {
        builder.addCase(getService.fulfilled, (state, action) => setService(state, action.payload));
        builder.addCase(getService.rejected, (state, action) => rejected(state, action));
        builder.addCase(reloadService.pending, (state, action) => pending(state, action));
        builder.addCase(reloadService.rejected, (state, action) => rejected(state, action));
        builder.addCase(updateService.pending, (state, action) => pending(state, action));
        builder.addCase(updateService.rejected, (state, action) => rejected(state, action));
        builder.addCase(deleteService.pending, (state, action) => pending(state, action));
        builder.addCase(deleteService.rejected, (state, action) => rejected(state, action));
    }
});

export const {
    update,
    updateServiceRange,
    removeService,
    setTimeoutService,
    setLoading,
    setError,
    authorized
} = hostListSlice.actions;

export default hostListSlice.reducer;
