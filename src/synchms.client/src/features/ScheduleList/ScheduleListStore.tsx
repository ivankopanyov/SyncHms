import { PayloadAction, createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import { ScheduleList } from './data';
import { api } from '../../utils/api';
import { object, params } from '../../utils/mapper';
import { Parameters } from '../ParameterList/data';

export interface Schedule {
    name: string;
    description?: string;
    intervalSeconds: number;
    last: string;
    message?: string;
    stackTrace?: string;
}

const defaultScheduleList: ScheduleList = {
    schedules: [],
    loading: false,
    auth: true
};

const initialState: ScheduleList = defaultScheduleList;

export const updateSchedule = createAsyncThunk('scheduleList/updateSchedule', async (schedule: {
    name: string,
    parameters: Parameters
}) => await api.put(`/scheduler/${schedule.name}`, object(schedule.parameters)));

const setSchedule = (state: ScheduleList, payload: Schedule) => {
    const { name, description, intervalSeconds, last, message, stackTrace } = payload;
    const isRunning = intervalSeconds > 0;
    const schedule = state.schedules.find(s => s.name === name);

    const parameters = params({
        intervalSeconds: intervalSeconds,
        last: last
    });

    if (!schedule) {
        state.schedules.push({
            name: name,
            description: description,
            parameters: parameters,
            isRunning: isRunning,
            loading: false,
            error: message,
            stackTrace: stackTrace
        });
    } else {
        schedule.description = description;
        schedule.parameters = parameters;
        schedule.isRunning = isRunning;
        schedule.loading = false;
        schedule.error = message;
        schedule.stackTrace = stackTrace;
        schedule.updateError = undefined;
    }
};

const scheduleListSlice = createSlice({
    name: 'scheduleList',
    initialState,
    reducers: {
        update(state, action: PayloadAction<Schedule>) {
            setSchedule(state, action.payload)
        },
        updateScheduleRange(state, action: PayloadAction<Schedule[]>) {
            const schedules = action.payload;
            schedules.forEach(schedule => setSchedule(state, schedule));
        },
        remove(state, action: PayloadAction<string>) {
            const name = action.payload;
            const schedule = state.schedules.find(s => s.name === name);
            if (schedule) {
                const index = state.schedules.indexOf(schedule);
                if (index >= 0) {
                    state.schedules.splice(index, 1);
                }
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
        builder.addCase(updateSchedule.fulfilled, (state, action) => setSchedule(state, action.payload));
        builder.addCase(updateSchedule.rejected, (state, action) => {
            const { name } = action.meta.arg;
            const schedule = state.schedules.find(s => s.name === name);
            if (schedule) {
                if (action.error.code === '404')
                {
                    const index = state.schedules.indexOf(schedule);
                    if (index >= 0) {
                        state.schedules.splice(index, 1);
                    }

                    return;
                }
                else
                {
                    schedule.loading = false;
                    schedule.updateError = action.error.message;
                }
            }

            if (action.error.code === '401')
                state.auth = false;
        });
        builder.addCase(updateSchedule.pending, (state, action) => {
            const { name } = action.meta.arg;
            const schedule = state.schedules.find(s => s.name === name);
            if (schedule) {
                schedule.loading = true;
                schedule.updateError = undefined;
            }
        });
    }
});

export const {
    update,
    updateScheduleRange,
    remove,
    setLoading,
    setError,
    authorized
} = scheduleListSlice.actions;

export default scheduleListSlice.reducer;
