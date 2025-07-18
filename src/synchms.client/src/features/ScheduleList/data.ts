import { Parameters } from "../ParameterList/data";

export interface ScheduleInfo {
    name: string;
    description?: string;
    parameters: Parameters;
    isRunning: boolean;
    loading: boolean;
    error?: string;
    stackTrace?: string;
    updateError?: string;
    lastSuccess?: string;
}

export interface ScheduleList {
    schedules: ScheduleInfo[];
    loading: boolean;
    error?: string;
    auth: boolean;
}
